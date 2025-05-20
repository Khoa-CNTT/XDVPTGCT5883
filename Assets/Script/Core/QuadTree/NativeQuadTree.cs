using System;
using dang;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public struct QuadElement<T> where T : unmanaged
{
    public float2 position;
    public T element;
}

public struct QuadNode
{
    public int firstChildIndex;
    public ushort count;
    public bool isLeaf;
}

[Serializable]
public unsafe partial struct NativeQuadTree<T> : IDisposable where T : unmanaged
{
    [NativeDisableUnsafePtrRestriction]
    UnsafeList<QuadElement<T>>* elements;

    [NativeDisableUnsafePtrRestriction]
    UnsafeList<int>* lookup;

    [NativeDisableUnsafePtrRestriction]
    UnsafeList<QuadNode>* nodes;

    QuadBounds bounds;
    [SerializeField] int elementsCount;
    [SerializeField] int maxDepth;
    [SerializeField] int maxLeafElements;

    public NativeQuadTree(QuadBounds bounds, Allocator allocator = Allocator.Temp,
        byte maxDepth = 6, ushort maxLeafElements = 32, int initialElementsCapacity = 256)
    {
        this.bounds = bounds;
        this.maxDepth = maxDepth;
        this.maxLeafElements = maxLeafElements;
        elementsCount = 0;

        if (maxDepth > 8 || maxDepth <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxDepth), "maxDepth must be between 0 and 8");

        var totalSize = LookUpTable.DepthSizeLookup[maxDepth + 1];

        lookup = UnsafeList<int>.Create(
            totalSize,
            allocator,
            NativeArrayOptions.ClearMemory
        );

        nodes = UnsafeList<QuadNode>.Create(
            totalSize,
            allocator,
            NativeArrayOptions.ClearMemory
        );

        elements = UnsafeList<QuadElement<T>>.Create(
            initialElementsCapacity,
            allocator
        );
    }

    public void ClearAndBilkIsert(NativeArray<QuadElement<T>> incomingElements)
    {
        Clear();

        if (elements->Capacity < incomingElements.Length)
        {
            elements->Resize(math.max(incomingElements.Length, elements->Capacity * 2));
        }

        var mortonCodes = new NativeArray<int>(incomingElements.Length, Allocator.Temp);
        var depthExtentsScalling = LookUpTable.DepthSizeLookup[maxDepth] / bounds.extents;
        for (int i = 0; i < mortonCodes.Length; i++)
        {
            var positionElement = incomingElements[i].position;
            positionElement -= bounds.center;
            positionElement.y = -positionElement.y;
            var position = (positionElement + bounds.extents) * 0.5f;
            position *= depthExtentsScalling;
            mortonCodes[i] = LookUpTable.mortonLookup[(int)position.x] | (LookUpTable.mortonLookup[(int)position.y] << 1);

            int atIndex = 0;
            for (int depth = maxDepth; depth >= 0; depth--)
            {
                (*(int*)((IntPtr)lookup->Ptr + atIndex * sizeof(int)))++;
                atIndex += IncrementIndex(mortonCodes[i], depth);
            }
        }

        for (int i = 0; i < incomingElements.Length; i++)
        {
            int atIndex = 0;
            for (int depth = maxDepth; depth >= 0; depth--)
            {
                var node = UnsafeUtility.ReadArrayElement<QuadNode>(nodes->Ptr, atIndex);
                if (node.isLeaf)
                {
                    UnsafeUtility.WriteArrayElement(elements->Ptr, node.firstChildIndex + node.count, incomingElements[i]);
                    node.count++;
                    UnsafeUtility.WriteArrayElement(nodes->Ptr, atIndex, node);
                    break;
                }
                atIndex += IncrementIndex(mortonCodes[i], depth);
            }
        }

        mortonCodes.Dispose();
    }

    private int IncrementIndex(int mortonCodes, int depth)
    {
        int shiftedMortonCode = (mortonCodes >> ((depth - 1) * 2)) & 0b11;
        return (LookUpTable.DepthSizeLookup[depth] * shiftedMortonCode) + 1;
    }

    private void RecursivePerpareLeaves(int previousOffset = 1, int depth = 1)
    {
        for (int i = 0; i < 4; i++)
        {
            var atIndex = previousOffset + i * LookUpTable.DepthSizeLookup[maxDepth - depth + 1];
            var elementsCount = UnsafeUtility.ReadArrayElement<int>(lookup->Ptr, atIndex);
            if (elementsCount > maxLeafElements && depth < maxDepth)
            {
                RecursivePerpareLeaves(atIndex + 1, depth + 1);
            }
            else if (elementsCount != 0)
            {
                var node = new QuadNode { firstChildIndex = elementsCount, count = 0, isLeaf = true };
                UnsafeUtility.WriteArrayElement(nodes->Ptr, atIndex, node);
                elementsCount += elementsCount;
            }
        }
    }

    public void RangeQuery(QuadBounds bounds, NativeList<QuadElement<T>> result)
    {
        new QuadtreeRangeQuery(this, bounds, result);
    }

    public void OnDrawGizmos(QuadBounds boundsParent = default)
    {
        Gizmos.DrawWireCube((Vector2)boundsParent.center, (Vector2)boundsParent.Size);
    }

    public void Clear()
    {
        UnsafeUtility.MemClear(lookup->Ptr, lookup->Capacity * UnsafeUtility.SizeOf<int>());
        UnsafeUtility.MemClear(nodes->Ptr, nodes->Capacity * UnsafeUtility.SizeOf<QuadBounds>());
        UnsafeUtility.MemClear(elements->Ptr, elements->Capacity * UnsafeUtility.SizeOf<QuadElement<T>>());
        elementsCount = 0;
    }

    public void Dispose()
    {
        UnsafeList<QuadElement<T>>.Destroy(elements);
        UnsafeList<int>.Destroy(lookup);
        UnsafeList<QuadNode>.Destroy(nodes);
        elements = null;
        lookup = null;
        nodes = null;
    }
}