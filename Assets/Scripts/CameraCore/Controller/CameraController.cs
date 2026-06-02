using UnityEngine;

namespace CameraCore
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _trackable;

        [Space]
        [SerializeField] private SpriteRenderer _backgroundBorder;

        [Space]
        [SerializeField, Min(0.01f)] private float _lerpSpeed = 0.15f;

        private Vector3 _refLerp;
        private Vector3 _offset;

        private readonly Vector3[] _viewportCorners =
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(1f, 0f, 0f),
            new Vector3(1f, 1f, 0f)
        };

        private void Awake()
        {
            _offset = _camera.transform.position - _trackable.position;
        }

        private void Start()
        {
            var targetPosition = GetTargetPosition();
            _camera.transform.position = ClampPositionToBackground(targetPosition);
        }

        private void LateUpdate()
        {
            var targetPosition = GetTargetPosition();
            targetPosition = ClampPositionToBackground(targetPosition);

            _camera.transform.position = Vector3.SmoothDamp(
                _camera.transform.position,
                targetPosition,
                ref _refLerp,
                _lerpSpeed
            );
        }

        private Vector3 GetTargetPosition()
        {
            return _trackable.position + _offset;
        }

        private Vector3 ClampPositionToBackground(Vector3 position)
        {
            if (!TryGetCameraViewBoundsOnBackground(position, out var cameraViewBounds))
                return position;

            var backgroundBounds = _backgroundBorder.sprite.bounds;

            var correction = Vector3.zero;

            correction.x = GetAxisCorrection(
                cameraViewBounds.min.x,
                cameraViewBounds.max.x,
                backgroundBounds.min.x,
                backgroundBounds.max.x
            );

            correction.y = GetAxisCorrection(
                cameraViewBounds.min.y,
                cameraViewBounds.max.y,
                backgroundBounds.min.y,
                backgroundBounds.max.y
            );

            var worldCorrection = _backgroundBorder.transform.TransformVector(correction);

            return position + worldCorrection;
        }

        private bool TryGetCameraViewBoundsOnBackground(Vector3 cameraPosition, out Bounds viewBounds)
        {
            viewBounds = new Bounds();

            var backgroundPlane = new Plane(
                _backgroundBorder.transform.forward,
                _backgroundBorder.transform.position
            );

            var cameraOffset = cameraPosition - _camera.transform.position;

            for (int i = 0; i < _viewportCorners.Length; i++)
            {
                var ray = _camera.ViewportPointToRay(_viewportCorners[i]);

                ray.origin += cameraOffset;

                if (!backgroundPlane.Raycast(ray, out var distance))
                    return false;

                var worldPoint = ray.GetPoint(distance);
                var localPoint = _backgroundBorder.transform.InverseTransformPoint(worldPoint);

                localPoint.z = 0f;

                if (i == 0)
                    viewBounds = new Bounds(localPoint, Vector3.zero);
                else
                    viewBounds.Encapsulate(localPoint);
            }

            return true;
        }

        private float GetAxisCorrection(
            float viewMin,
            float viewMax,
            float borderMin,
            float borderMax)
        {
            var viewSize = viewMax - viewMin;
            var borderSize = borderMax - borderMin;

            if (viewSize >= borderSize)
            {
                var viewCenter = (viewMin + viewMax) * 0.5f;
                var borderCenter = (borderMin + borderMax) * 0.5f;

                return borderCenter - viewCenter;
            }

            if (viewMin < borderMin)
                return borderMin - viewMin;

            if (viewMax > borderMax)
                return borderMax - viewMax;

            return 0f;
        }
    }
}