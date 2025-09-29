using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphericalTransform))]
public class BasicVehicle : MonoBehaviour
{
    [SerializeField]private SphereTile currentTile;
    private SphereTile targetTile = null;
    private Queue<SphereTile> Route = new Queue<SphereTile>();


    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve transformCurve;

    private SphericalTransform _Stransform;
    private float expiredTime = 0;
    private void Awake()
    {
        _Stransform = GetComponent<SphericalTransform>();
        _Stransform.Transform(currentTile.SphericalPosition);
    }
    private void Update()
    {
        if (targetTile == null)
            return;
    }
    public void AddRoutePoint(SphereTile tile) 
    {
        if(targetTile == null)
        {
            targetTile = tile;
        }
        else 
        {
            Route.Enqueue(tile);
        }
    }
}
