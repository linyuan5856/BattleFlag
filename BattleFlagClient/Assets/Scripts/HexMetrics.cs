using UnityEngine;

public static class HexMetrics
{
    public const float OuterRadius = 10f;

    public const float InnerRadius = OuterRadius * 0.866025404f;

    public const float solidFactor = 0.75f;

    public const float blendFactor = 1f - solidFactor;

    public static readonly Vector3[] corners =
    {
        new(0f, 0f, OuterRadius),
        new(InnerRadius, 0.0f, .5f * OuterRadius),
        new(InnerRadius, 0.0f, -.5f * OuterRadius),
        new(0f, 0f, -OuterRadius),
        new(-InnerRadius, 0.0f, -.5f * OuterRadius),
        new(-InnerRadius, 0.0f, .5f * OuterRadius),
        new(0f, 0f, OuterRadius),
    };

    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return corners[(int)direction];
    }

    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return corners[(int)direction + 1];
    }

    public static Vector3 GetFirstSolidCorner(HexDirection direction)
    {
        return corners[(int)direction] * solidFactor;
    }

    public static Vector3 GetSecondSolidCorner(HexDirection direction)
    {
        return corners[(int)direction + 1] * solidFactor;
    }

    public static Vector3 GetBridge(HexDirection direction)
    {
        return (corners[(int)direction] + corners[(int)direction + 1])  * blendFactor;
    }
}