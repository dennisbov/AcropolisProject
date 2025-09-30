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
            _currentTile.gameObject.SetActive(false);
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _planetLayer))
        {
                _currentTile = hit.transform.GetChild(0);
                _currentTile.gameObject.SetActive(true);
        }
    }
}
