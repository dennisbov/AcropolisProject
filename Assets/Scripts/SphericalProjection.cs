using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SphericalProjection : MonoBehaviour
{
    [SerializeField] private Transform projectedObject;
    [SerializeField] private SphericalTransform projection;
    [SerializeField] private GameObject tile;

    private void Start()
    {
        projectedObject.position = tile.GetComponent<Renderer>().bounds.center;
    }
    private Vector3 FindMeshCenter(Vector3[] vertex)
    {
        Vector3 sum = new Vector3();
        foreach (Vector3 v in vertex) 
        { 
            sum += v;
        }
        return sum/vertex.Length;
    }
    private void Update()
    {
        //Vector3 positionOnSphere = projectedObject.position.normalized;
        //projection.latitude = Mathf.Asin(positionOnSphere.y);
        //Vector2 positionOnSircle = (new Vector2(positionOnSphere.x, positionOnSphere.z)).normalized;
        //if (positionOnSircle.x >= 0)
        //{
        //    projection.longtitude = Mathf.Atan(positionOnSircle.y / positionOnSircle.x);
        //} 
        //else
        //{
        //    projection.longtitude = Mathf.Atan(positionOnSircle.y / positionOnSircle.x) + Mathf.PI;
        //}
    }
}
