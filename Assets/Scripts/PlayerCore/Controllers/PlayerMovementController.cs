using System;
using InputCore;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerCore
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _floorLayer;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private PlayerModel _model;
        
        private SurfaceFinder _surfaceFinder;
        private PlayerInputActions _actions;

        private void Awake()
        {
            _surfaceFinder = new(_camera);
            
            _actions = new();
            _actions.Enable();
        }

        private void Start()
        {
            _actions.Player.Click.performed += OnClick;
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if(_surfaceFinder.TryGetEnemy(Pointer.current.position.ReadValue(), _enemyLayer, out var enemy))
                if (_model.TryAttackEnemy(enemy))
                    return;
            
            if (_surfaceFinder.TryGetNavMeshPosition(Pointer.current.position.ReadValue(), _floorLayer, out var position))
                _model.SetDestination(position);
        }
    }
}