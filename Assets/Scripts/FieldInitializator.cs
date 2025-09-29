using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FieldInitializator : MonoBehaviour
{
    [SerializeField] private FIeldInfo field;
    [SerializeField] private Transform planet;
    [SerializeField] private LayerMask tileMask;
    [SerializeField] private float overlapRadius;

    private void OnValidate()
    {
        for (int i = 0; i < planet.childCount; i++)
        {
            if (planet.GetChild(i).TryGetComponent<SphereTile>(out SphereTile tile))
            {
                Vector3 center = planet.GetChild(i).GetComponent<Renderer>().bounds.center;
                Collider[] neibours = Physics.OverlapSphere(center, overlapRadius, tileMask);
                tile.AdjacentTiles.Clear();
                for (int j = 0; j < neibours.Length; j++) {
                    if (neibours[j] == planet.GetChild(i).GetComponent<Collider>())
                    {
                        continue;
                    }
                    tile.AdjacentTiles.Add(neibours[j].GetComponent<SphereTile>());
                }

                tile.SphericalPosition = SpherificalProjection(center);

                field.tiles.Add(tile);
            }
        }
    }

    private Vector2 SpherificalProjection(Vector3 projectedPosition)
    {
        Vector3 positionOnSphere = projectedPosition.normalized;
        Vector2 projection = new Vector2();
        projection.x = Mathf.Asin(positionOnSphere.y);
        Vector2 positionOnSircle = (new Vector2(positionOnSphere.x, positionOnSphere.z)).normalized;
        if (positionOnSircle.x >= 0)
        {
            projection.y = Mathf.Atan(positionOnSircle.y / positionOnSircle.x);
        }
        else
        {
            projection.y = Mathf.Atan(positionOnSircle.y / positionOnSircle.x) + Mathf.PI;
        }
        return projection;
    }
}
