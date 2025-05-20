using System;
using dang;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

public unsafe partial struct NativeQuadTree<T> where T : unmanaged
{
    public struct QuadtreeRangeQuery
    {
        NativeQuadTree<T> tree;
        UnsafeList<T>* fastResults;
        QuadBounds bounds;
        int count;

        public QuadtreeRangeQuery(NativeQuadTree<T> tree, QuadBounds bounds, NativeList<QuadElement<T>> results)
        {
            this.tree = tree;
            this.bounds = bounds;
            this.count = 0;

            fastResults = (UnsafeList<T>*)NativeListUnsafeUtility.GetInternalListDataPtrUnchecked(ref results);
            RecursiveRangeQuery(tree.bounds);
            fastResults->Length = count;
        }

        public void RecursiveRangeQuery(QuadBounds boundsParent = default, bool containedParent = false, int previoutsOffset = 1, int depth = 1)
        {
            var totalNodesCount = count + 4 * tree.maxDepth;
            if (totalNodesCount > fastResults->Capacity)
            {
                fastResults->Resize(math.max(fastResults->Capacity * 2, totalNodesCount));
            }

            var depthSize = LookUpTable.DepthSizeLookup[tree.maxDepth - depth + 1];
            for (int i = 0; i < 4; i++)
            {
                var boundsChild = boundsParent.GetBoundsChildint(i);
                var contained = containedParent;
                if (!contained)
                {
                    if (bounds.Contains(boundsChild)) contained = true;
                    else if (!bounds.Intersects(boundsChild)) continue;
                }

                var atIndex = previoutsOffset + i * depthSize;
                var elementsCount = UnsafeUtility.ReadArrayElement<int>(tree.lookup->Ptr, atIndex);
                if (elementsCount > tree.maxLeafElements && depth < tree.maxDepth)
                {
                    RecursiveRangeQuery(boundsChild, contained, atIndex + 1, depth + 1);
                }
                else if (elementsCount != 0)
                {
                    var node = UnsafeUtility.ReadArrayElement<QuadNode>(tree.nodes->Ptr, atIndex);
                    if (contained)
                    {
                        var index = (void*)((IntPtr)tree.elements->Ptr + node.firstChildIndex * UnsafeUtility.SizeOf<QuadElement<T>>());
                        UnsafeUtility.MemCpy((void*)((IntPtr)fastResults->Ptr + count * UnsafeUtility.SizeOf<T>()),
                        index, node.count * UnsafeUtility.SizeOf<QuadElement<T>>());
                        count += node.count;
                    }
                    else
                    {
                        for (int j = 0; j < node.count; j++)
                        {
                            var element = UnsafeUtility.ReadArrayElement<QuadElement<T>>(tree.elements->Ptr, node.firstChildIndex + j);
                            if (bounds.ContainsCircle(element.position))
                            {
                                UnsafeUtility.WriteArrayElement(fastResults->Ptr, count++, element);
                            }
                        }
                    }
                }
            }
        }
    }
}