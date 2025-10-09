using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeometryRepository : MonoBehaviour
{
    public static TileGeometryRepository Instance;
    
    private List<TileGeometry> _tiles;

    public void AddTileGeometry(TileGeometry tileGeometry)
    {
        foreach(TileGeometry tile in _tiles)
        {
            if (tileGeometry.id == tile.id) 
            {
                Debug.LogError("Attempted to add tile geometry with existing index");
                return;
            }
        }
        _tiles.Add(tileGeometry);
    }

    public TileGeometry getTileGeometry(int id)
    {
        TileGeometry result = null;
        foreach(TileGeometry tile in _tiles)
        {
            if (tile.id == id)
            {
                result = tile;
            }
        }
        return result;
    }
}
