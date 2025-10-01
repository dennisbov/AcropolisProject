using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinates
{
    public Vector3 forwardAxis;
    public Vector2 localPosition;
    public Vector3 normal;
    public float scale;
    public Vector3 center;

    public TileCoordinates(Vector3 forwardAxis, Vector2 localPosition, Vector3 normal, float scale, Vector3 center)
    {
        this.forwardAxis = forwardAxis;
        this.localPosition = localPosition;
        this.normal = normal;
        this.scale = scale;
        this.center = center;
    }

    public Vector3 TranslateToGlobal()
    {
        Vector3 rightAxis = Vector3.Cross(forwardAxis, normal);
        Vector3 position = rightAxis.normalized * localPosition.x + forwardAxis.normalized * localPosition.y;
        return center + position * scale;
    }
}
