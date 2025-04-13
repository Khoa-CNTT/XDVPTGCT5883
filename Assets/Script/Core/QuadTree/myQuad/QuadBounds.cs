using Unity.Mathematics;
using UnityEngine;

public class QuadBounds
{
    public float2 center;
    public float2 extents;
    public float2 Size => extents * 2f;
    public float2 Haft => extents * 0.5f;
    public float2 Min => center - extents;
    public float2 Max => center + extents;
    public float Radius => math.length(extents);

    public QuadBounds(float2 center, float2 extents) => (this.center, this.extents) = (center, extents);

    public bool Contains(float point) => math.all(point <= Min) && math.all(point <= Max);

}
