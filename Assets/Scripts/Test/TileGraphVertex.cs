using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGraphVertex
{
    public int id;
    public List<TileGraphVertex> neighbourVertices;

    public TileGraphVertex(int id)
    {
        this.id = id;
        neighbourVertices = new List<TileGraphVertex>();
    }
}
