using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinates
{
    public Vector3 forwardAxis;
    public float angle;
    public float radius;
    public Vector3 normal;
    public float scale;
    public Vector3 center;

    public TileCoordinates(Vector3 forwardAxis, float angle, float radius, Vector3 normal, float scale, Vector3 center)
    {
        this.forwardAxis = forwardAxis;
        this.angle = angle;
        this.radius = radius;
        this.normal = normal;
        this.scale = scale;
        this.center = center;
    }

    public Vector3 TranslateToGlobal()
    {
        Vector3 rightAxis = Vector3.Cross(forwardAxis, normal);
        Vector2 localPosition = new Vector2(MathF.Cos(angle) * radius, MathF.Sin(angle) * radius);
        Vector3 position = rightAxis.normalized * localPosition.x + forwardAxis.normalized * localPosition.y;
        return center + position * scale;
    }
}
