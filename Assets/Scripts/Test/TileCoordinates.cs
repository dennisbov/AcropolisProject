using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileCoordinates
{
    public Vector3 forwardAxis;
    public Vector2 localPosition;
    public int geometryId;

    public TileCoordinates(Vector3 forwardAxis, Vector2 localPosition, int geometryId)
    {
        this.forwardAxis = forwardAxis;
        this.localPosition = localPosition;
        this.geometryId = geometryId;
    }
}
