using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCoordinates
{
    public Vector3 forwardAxis;
    public Vector2 localPosition;

    public TileCoordinates(Vector3 forwardAxis, Vector2 localPosition)
    {
        this.forwardAxis = forwardAxis;
        this.localPosition = localPosition;
    }
}
