using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestLocalCoordinates : MonoBehaviour
{
    [SerializeField] private TileCoordinates tileCoordinates;
    
    public void Move(TileCoordinates newTileCoordinates)
    {
        transform.position = newTileCoordinates.TranslateToGlobal();
    }
    private Vector3 TranslateToGlobal()
    {
        Vector3 rightAxis = Vector3.Cross(forwardAxis, normal);
        Vector3 position = rightAxis.normalized * localPosition.x + forwardAxis.normalized * localPosition.y;
        return center + position * scale;
    }
}
