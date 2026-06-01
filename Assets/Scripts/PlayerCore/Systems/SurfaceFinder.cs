using UnityEngine;

namespace PlayerCore
{
    public class SurfaceFinder
    {
        private Camera _camera;

        public SurfaceFinder(Camera camera)
        {
            _camera = camera;
        }
        
        public bool TryGetNavMeshPosition(Vector2 screenPosition, LayerMask floorLayer, out Vector3 worldPosition)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, floorLayer))
            {
                worldPosition = hit.point;
                return true;
            }
            
            worldPosition = Vector3.zero;
            return false;
        }
    }
}