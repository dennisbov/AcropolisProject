using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SphericalTransform))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxLatitude;
    [SerializeField] private Transform _rotationCenter;
    [SerializeField] private float _mouseScrollSpeed;
    [SerializeField] private float _nearCameraBound;
    [SerializeField] private float _farCameraBound;

    private SphericalTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<SphericalTransform>();
        transform.LookAt(transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float Vdir = -Input.GetAxis("Mouse Y") * _movementSpeed;
            float Hdir = -Input.GetAxis("Mouse X") * _movementSpeed;
            float latidude = Mathf.Clamp(_transform.Position.x + Vdir, -_maxLatitude, +_maxLatitude);
            _transform.Transform(new Vector2(latidude, _transform.Position.y + Hdir));
            transform.LookAt(_rotationCenter);
        }
        float scroll = -Input.mouseScrollDelta.y;
        
        _transform._maxGroundCheckDistanse += scroll * _mouseScrollSpeed;
        _transform._maxGroundCheckDistanse =
            Mathf.Clamp(_transform._maxGroundCheckDistanse, _nearCameraBound, _farCameraBound);
    }
}
