using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLocalCoordinates : MonoBehaviour
{
    [SerializeField] private TileCoordinates tileCoordinates;
    
    public void Move(TileCoordinates newTileCoordinates)
    {
        transform.position = newTileCoordinates.TranslateToGlobal();
    }
}
