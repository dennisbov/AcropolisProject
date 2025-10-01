using ProjectAcropolis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TestMovement _testObject;
    [SerializeField] private LayerMask _planetLayer;

    private Transform _currentTile;
    private void Update()
    {
        if(_currentTile != null)
            _currentTile.GetChild(0).gameObject.SetActive(false);
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _planetLayer))
        {
                _currentTile = hit.transform;
                _currentTile.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            _testObject.AddTarget(_currentTile.GetComponent<TileGeometry>().globalCenter);
        }
    }
}
