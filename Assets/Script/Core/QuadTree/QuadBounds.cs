using System;
using Unity.Mathematics;

[Serializable]
public struct QuadBounds
{
    public float2 center; // Vị trí trung tâm của hình chữ nhật
    public float2 extents; // đại diện cho nửa chiều rộng và chiều aco
    public float2 Size => extents * 2f; // trả về kích thước của hình chữ nhật
    public float2 Haft => extents * 0.5f; // trả về 1 nửa kích thước của extents
    public float2 Min => center - extents; // tính tọa độ góc dưới trái của hình chữ nhật
    public float2 Max => center + extents; // tính tọa độ góc trên phải của hình chữ nhật
    public float Radius => math.length(extents); // bán kính của hình

    public QuadBounds(float2 center, float2 extents) => (this.center, this.extents) = (center, extents);

    public bool Contains(float point) => math.all(point <= Min) && math.all(point <= Max);
    public bool Contains(QuadBounds bounds) => math.all(bounds.Min >= Min) && math.all(bounds.Max <= Max);

    public bool Intersects(QuadBounds bounds) =>
        math.abs(center.x - bounds.center.x) <= (extents.x + bounds.extents.x) &&
        math.abs(center.y - bounds.center.y) <= (extents.y + bounds.extents.y);

    public bool ContainsCircle(float2 point) => math.lengthsq(point - center) <= math.lengthsq(Radius);

    public bool ContainsCircle(QuadBounds bounds) =>
        ContainsCircle(bounds.GetCorner(0)) && ContainsCircle(bounds.GetCorner(1)) &&
        ContainsCircle(bounds.GetCorner(2)) && ContainsCircle(bounds.GetCorner(3));

    public bool IntersectsCircle(QuadBounds bounds)
    {
        float2 closesPoint = math.clamp(center, bounds.Min, bounds.Max);
        float distanceSquared = math.lengthsq(center - closesPoint);
        return distanceSquared <= math.lengthsq(Radius);
    }

    public float2 GetCorner(int zIndexChild)
    {
        return zIndexChild switch
        {
            0 => new float2(Min.x, Max.y), // Bottom Left
            1 => Max, // Bottom Right
            2 => Min, // Top Right
            3 => new float2(Max.x, Min.y), // Top Left
            _ => throw new System.ArgumentOutOfRangeException(nameof(zIndexChild), "Invalid zIndexChild value")
        };
    }

    public QuadBounds GetBoundsChildint(int zIndexChild)
    {
        return zIndexChild switch
        {
            0 => new QuadBounds(new float2(center.x - Haft.x, center.y + Haft.y), Haft), // Bottom Left
            1 => new QuadBounds(center + Haft, Haft), // Bottom Right
            2 => new QuadBounds(center - Haft, Haft), // Top Right
            3 => new QuadBounds(new float2(center.x + Haft.x, center.y - Haft.y), Haft), // Top Left
            _ => throw new System.ArgumentOutOfRangeException(nameof(zIndexChild), "Invalid zIndexChild value")
        };
    }
}


