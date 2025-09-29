using ProjectAcropolis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TestMovement _testObject;
    [SerializeField] private LayerMask _planetLayer;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        { 
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _planetLayer))
            {
                _testObject.AddTarget(hit.point);
            }
        }
    }
}
