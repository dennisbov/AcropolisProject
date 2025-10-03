using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateTileGraph : MonoBehaviour
{
    [SerializeField] private bool generateTileGraph;
    [SerializeField] private bool testGraph;
    [SerializeField] private float overlapRadius = 0;
    [NonSerialized] public List<TileGraphVertex> tileGraphVertices;
    [SerializeField] private Transform _planet;
    [SerializeField] private LayerMask _planetLayer;

    private void OnValidate()
    {
        if (generateTileGraph)
        {
            tileGraphVertices = new List<TileGraphVertex>();
            foreach (Transform tile in _planet)
            {
                if (tile.TryGetComponent<TileGeometry>(out TileGeometry geometry))
                {
                    TileGraphVertex vertex = new TileGraphVertex(geometry.id);
                    tileGraphVertices.Add(vertex);
                }
            }
            for (int i = 0; i < _planet.childCount; i++)
            {
                TileGeometry geometry = _planet.GetChild(i).GetComponent<TileGeometry>();
                Collider[] neighbours = Physics.OverlapSphere(geometry.globalCenter, overlapRadius, _planetLayer);
                if (neighbours.Length < 6)
                {
                    Debug.Log(neighbours.Length);
                    Debug.LogError("overlapRadius is too small!");
                    return;
                }
                if (neighbours.Length > 7)
                {
                    Debug.Log(neighbours.Length);
                    Debug.LogError("overlapRadius is too big!");
                    return;
                }

                foreach (Collider neighbour in neighbours)
                {
                    int id = neighbour.GetComponent<TileGeometry>().id;

                    if (i == id)
                    {
                        continue;
                    }
                    tileGraphVertices[i].neighbourVertices.Add(tileGraphVertices[id]);
                }
            }
        }
        if (testGraph)
        {
            foreach(TileGraphVertex vertex in tileGraphVertices[0].neighbourVertices)
            {
                Debug.Log(vertex.id);
            }
        }
    }
}
