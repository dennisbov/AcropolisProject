using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovementController : MonoBehaviour
{
    [SerializeField] private TestLocalCoordinates obj;
    [SerializeField] private LayerMask _planetLayer;

    private Vector3 forwardVector;
    [SerializeField] private float angle;
    [SerializeField] private float radius;

    private TileCoordinates currentCoordinates;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _planetLayer))
            {
                TileGeometry geometry = hit.transform.gameObject.GetComponent<TileGeometry>();
                if (forwardVector == Vector3.zero)
                {
                    Debug.LogError("forward vector is zero!");
                    return;
                }
                currentCoordinates = new TileCoordinates(forwardVector, 
                    angle, 
                    radius, 
                    geometry.normal, 
                    geometry.innerSphereRadius, 
                    geometry.globalCenter
                    );
                obj.Move(currentCoordinates);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _planetLayer))
            {
                TileGeometry geometry = hit.transform.gameObject.GetComponent<TileGeometry>();
                forwardVector = (hit.point - geometry.globalCenter).normalized;
            }
        }
    }
}
