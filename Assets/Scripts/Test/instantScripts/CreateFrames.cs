using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class CreateFrames : MonoBehaviour
{
    [SerializeField] private bool _createFrames = false;
    [SerializeField] private bool _onlyDestroy;
    [SerializeField] private GameObject _hexagonFrame;
    [SerializeField] private GameObject _pentagonFrame;
    [SerializeField] private List<GameObject> _pentagons = new List<GameObject>();
    [SerializeField] private Transform _planet;
    [SerializeField] private float _frameScaleOffset;
    [SerializeField] private GameObject _testSphere;


    private void OnValidate()
    {
        if (_createFrames)
        {
            foreach (Transform _tile in _planet)
            {
                if (_tile.childCount > 0)
                    _tile.GetChild(0).gameObject.SetActive(false);
                
            }
            for(int i = 0; i < _planet.childCount; i++)
            {
                _planet.GetChild(i).GetComponent<TileGeometry>().id = i;
            }
            
            foreach (Transform _tile in _planet)
            {
                TileGeometry tile = _tile.GetComponent<TileGeometry>();
                Mesh mesh = _tile.GetComponent<MeshFilter>().sharedMesh;
                Vector3 center = new Vector3();
                foreach (Vector3 vertice in mesh.vertices)
                {
                    center += vertice;
                }
                center = center / mesh.vertices.Length;
                tile.globalCenter = center * _tile.lossyScale.x;
                tile.scale = Vector3.Distance(center, mesh.vertices[0])*_tile.lossyScale.x;
            }
            if (_onlyDestroy)
                return;
            foreach (Transform tile in _planet)
            {
                GameObject frame;
                if (_pentagons.Contains(tile.gameObject))
                    frame = Instantiate(_pentagonFrame);
                else
                    frame = Instantiate(_hexagonFrame);
                Mesh mesh = tile.GetComponent<MeshFilter>().mesh;

                Vector3 center = new Vector3();
                Vector3 thirdVertex = new Vector3();   
                foreach(Vector3 vertice in mesh.vertices)
                {
                    center += vertice;
                    if (vertice != mesh.vertices[0] && mesh.vertices[0]-center != -(vertice-center))
                        thirdVertex = vertice;
                }
                center = center / mesh.vertices.Length;
                float scale = (mesh.vertices[0] - center).magnitude * tile.lossyScale.x;
                frame.transform.position = center * tile.lossyScale.x;
                Plane plane = new Plane(
                    center * tile.lossyScale.x * 100f, 
                    mesh.vertices[0] * tile.lossyScale.x * 100f, 
                    thirdVertex * tile.lossyScale.x * 100f);
                Vector3 up = plane.normal.normalized;
                Vector3 forward = (mesh.vertices[0] * tile.lossyScale.x - center * tile.lossyScale.x).normalized;
                Quaternion frameRotation = new Quaternion();
                frameRotation.SetLookRotation(forward, -up);
                frame.transform.rotation = frameRotation;

                frame.transform.localScale = Vector3.one * scale;
                frame.transform.parent = tile;
            }
        }
    }
}

