using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SphericalTransform : MonoBehaviour
{
    [NonSerialized] public Vector3 Position;
    [SerializeField] private LayerMask _planetMask;
    [SerializeField] private Transform _planetCenter;
    [SerializeField] public float _maxGroundCheckDistanse;

    //not working properly
    public void Transform(Vector2 newPosition)
    {
        float y = Mathf.Sin(newPosition.x);
        float x = Mathf.Cos(newPosition.x) * Mathf.Cos(newPosition.y);
        float z = Mathf.Cos(newPosition.x) * Mathf.Sin(newPosition.y);
        Vector3 direction = new Vector3(x, y, z);
        Vector3 rayStart = direction * _maxGroundCheckDistanse;
        Position = newPosition;
        if (Physics.Raycast(rayStart, Vector3.zero-rayStart, out RaycastHit hit, _maxGroundCheckDistanse, _planetMask))
        {
            transform.position = hit.point;
            return;
        }
        transform.position = rayStart;
    }

    public void MoveTowards(Vector3 targetPosition, float maxDistanceDelta)
    {
        Vector3 planetCenter = _planetCenter.position;
        Vector3 resultVector = Vector3.RotateTowards(
            transform.position - planetCenter,
            targetPosition - planetCenter,
            maxDistanceDelta,
            0);
        transform.position = findRaycastHitOnPlanet(resultVector + planetCenter).point;
    }

    public void LookAt(Vector3 position)
    {
        Plane plane = new Plane(_planetCenter.position, transform.position, position);
        Vector3 normal = findRaycastHitOnPlanet(transform.position).normal;
        Quaternion resultRotation = new Quaternion();
        resultRotation.SetLookRotation(Vector3.Cross(plane.normal, normal), normal);
        transform.rotation = resultRotation;
    }

    public void LookTowards(Vector3 position, float speed)
    {
        Plane plane = new Plane(_planetCenter.position, transform.position, position);
        Vector3 normal = findRaycastHitOnPlanet(transform.position).normal;
        Quaternion resultRotation = new Quaternion();
        resultRotation.SetLookRotation(Vector3.Cross(plane.normal, normal), normal);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, resultRotation, speed);
    }
    
    public float LookAngle(Vector3 position)
    {
        Plane plane = new Plane(_planetCenter.position, transform.position, position);
        Vector3 normal = findRaycastHitOnPlanet(transform.position).normal;
        Quaternion resultRotation = new Quaternion();
        resultRotation.SetLookRotation(Vector3.Cross(plane.normal, normal), normal);

        return Quaternion.Angle(transform.rotation, resultRotation);
    }


    private RaycastHit findRaycastHitOnPlanet(Vector3 point)
    {
        Vector3 rayOrigin = (point - _planetCenter.position).normalized * _maxGroundCheckDistanse;
        if(Physics.Raycast(rayOrigin, (_planetCenter.position - point).normalized, out RaycastHit hit, float.PositiveInfinity, _planetMask))
        {
            return hit;
        }
        else
        {
            Debug.LogError("missing surface for object " + gameObject.name);
            return new RaycastHit();  
        }
    }
}
