using ProjectAcropolis;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TestMovement _testObject;
    [SerializeField] private LayerMask _planetLayer;
    [SerializeField] private TileDistancesRepository _tileDistancesRepository;
    [SerializeField] private CreateTileGraph _graphGenerator;
    private List<int> obstacleList = new List<int>();
    [SerializeField] private GameObject _obstalePrefab;
    [SerializeField] private int _range;
    [SerializeField] private Transform _planet;

    private List<TileGraphVertex> tileGraph;
    private Transform _currentTile;
    private int _selectedTileIndex = -1;
    private List<Transform> selectedTiles = new List<Transform>();
    private PathFinder _pathFinder;

    private void Start()
    {
        tileGraph = _graphGenerator.GenerateTileGraph();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(_pathFinder != null)
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _planetLayer))
                {
                    int _currentTileId = hit.transform.GetComponent<TileGeometry>().id;
                    if(_pathFinder.GetPathTo(_currentTileId, out List<int> path))
                    {
                        List<Vector3> targets = new List<Vector3>();
                        foreach(int id in path)
                        {
                            targets.Add(_planet.GetChild(id).GetComponent<TileGeometry>().globalCenter);
                        }
                        _testObject.AddTarget(targets);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _planetLayer))
            {
                TileGeometry _currentTile = hit.transform.GetComponent<TileGeometry>();
                if (obstacleList.Contains(_currentTile.id))
                {
                    Destroy(hit.transform.GetChild(1).gameObject);
                    obstacleList.Remove(_currentTile.id);
                    return;
                }
                GameObject obstacle = Instantiate(_obstalePrefab, _currentTile.globalCenter, Quaternion.identity);
                obstacle.transform.SetParent(hit.transform);
                obstacleList.Add(_currentTile.id);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(
                _testObject.transform.position.normalized * 10,
                -_testObject.transform.position,
                out RaycastHit objectHit,
                100,
                _planetLayer)) 
            {
                if (objectHit.transform.TryGetComponent<TileGeometry>(out TileGeometry geometry)) {
                    foreach(Transform tile in selectedTiles)
                    {
                        tile.gameObject.SetActive(false);
                    }
                    selectedTiles.Clear();
                    
                    _pathFinder = new PathFinder(tileGraph, obstacleList, geometry.id, _range);
                    foreach(int id in _pathFinder.GetAvailableTiles())
                    {
                        Transform tile = _planet.GetChild(id).GetChild(0);
                        tile.gameObject.SetActive(true);
                        selectedTiles.Add(tile);
                    }
                }
            }
        }
    }
}
