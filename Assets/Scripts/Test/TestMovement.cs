using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectAcropolis
{
    [RequireComponent(typeof(SphericalTransform))]
    public class TestMovement : MonoBehaviour
    {
        private SphericalTransform _sphericalTransform;
        private Queue<TileCoordinates> _targetPositions;
        private TileCoordinates _currentTarget;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _rotationSpeedOnSpot;
        [SerializeField] private float _toleratedAngleToTarget;

        [SerializeField] private TMP_Text _debugAngle;

        private float _currentSpeed;
        private int _accelerationSing;
        private Vector3 _pointToFollow;
        private bool _isRotating = true;
        private void Awake()
        {
            _sphericalTransform = GetComponent<SphericalTransform>();
            _currentTarget = transform.position;
            _targetPositions = new Queue<TileCoordinates>();
            _pointToFollow = transform.position;
        }

        
        public void Update()
        {
            _debugAngle.text = Vector3.Angle(_pointToFollow, _currentTarget).ToString();
            if (Vector3.Angle(transform.position, _currentTarget) < _toleratedAngleToTarget && _targetPositions.Count == 0)
                return;

            if (_targetPositions.Count > 0 && Vector3.Angle(_pointToFollow, _currentTarget) < _toleratedAngleToTarget)
            {
                _currentTarget = _targetPositions.Dequeue();
            }

            if (_sphericalTransform.LookAngle(_currentTarget) > 0 && _isRotating)
            {
                _sphericalTransform.LookTowards(_currentTarget, _rotationSpeedOnSpot * Time.deltaTime);
                return;
            }

            _isRotating = false;
 

            _pointToFollow = Vector3.RotateTowards(_pointToFollow, _currentTarget, _maxSpeed * Time.deltaTime, 1);
            float delay = Vector3.Angle(transform.position, _pointToFollow);

            _sphericalTransform.LookTowards(_currentTarget, _rotationSpeed * Time.deltaTime);
            _sphericalTransform.MoveTowards(_pointToFollow, delay * _acceleration * Time.deltaTime);
        }

        public void AddTarget(Vector3 target)
        {
            if(_targetPositions.Count == 0)
            {
                _currentTarget = target;
                _isRotating = true;
            }
            _targetPositions.Enqueue(target);
        }

        public void AddTarget(List<Vector3> target)
        {
            foreach(Vector3 t in target)
            {
                AddTarget(t);
            }
        }
        
    }
}
