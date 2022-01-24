using UnityEngine;

public static class HexMetrics
{
    public const float OuterRadius = 10f;

    public const float InnerRadius = OuterRadius * 0.866025404f;

    public static Vector3[] Corners =
    {
        new(0f, 0f, OuterRadius),
        new(InnerRadius, 0.0f, .5f * OuterRadius),
        new(InnerRadius, 0.0f, -.5f * OuterRadius),
        new(0f, 0f, -OuterRadius),
        new(-InnerRadius, 0.0f, -.5f * OuterRadius),
        new(-InnerRadius, 0.0f, .5f * OuterRadius),
        new(0f, 0f, OuterRadius),
    };
}