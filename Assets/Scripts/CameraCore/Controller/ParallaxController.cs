using System;
using UnityEngine;

namespace CameraCore
{
    public class ParallaxController : MonoBehaviour
    {
        [Serializable]
        private class ParallaxLayer
        {
            [SerializeField] private Transform _layer;
            [SerializeField] private float _speedMultiplier = 1f;

            private Vector3 _startLocalPosition;

            public void CacheStartPosition(Transform referencePlane)
            {
                _startLocalPosition = referencePlane.InverseTransformPoint(_layer.position);
            }

            public void UpdatePosition(Transform referencePlane, Vector3 cameraLocalOffset)
            {
                var targetLocalPosition = _startLocalPosition + cameraLocalOffset * _speedMultiplier;
                _layer.position = referencePlane.TransformPoint(targetLocalPosition);
            }
        }

        [SerializeField] private Camera _camera;

        [SerializeField] private Transform _referencePlane;

        [SerializeField] private ParallaxLayer[] _layers;

        [SerializeField] private float _minMoveSqrMagnitude = 0.0001f;

        private Vector3 _startCameraLocalPosition;
        private Vector3 _lastCameraPosition;

        private void Awake()
        {
            _startCameraLocalPosition = _referencePlane.InverseTransformPoint(_camera.transform.position);
            _lastCameraPosition = _camera.transform.position;

            foreach (var layer in _layers)
                layer.CacheStartPosition(_referencePlane);
        }

        private void LateUpdate()
        {
            if ((_camera.transform.position - _lastCameraPosition).sqrMagnitude < _minMoveSqrMagnitude)
                return;

            _lastCameraPosition = _camera.transform.position;

            var currentCameraLocalPosition = _referencePlane.InverseTransformPoint(_camera.transform.position);
            var cameraLocalOffset = currentCameraLocalPosition - _startCameraLocalPosition;

            cameraLocalOffset.z = 0f;

            foreach (var layer in _layers)
                layer.UpdatePosition(_referencePlane, cameraLocalOffset);
        }
    }
}