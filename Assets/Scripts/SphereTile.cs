using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SphereTile : MonoBehaviour
{
    public Vector2 SphericalPosition;
    public List<SphereTile> AdjacentTiles = new List<SphereTile>();
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
}
