using System;
using System.Collections;
using EnemyCore;
using UnityEngine;
using UnityEngine.AI;

namespace PlayerCore
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _model;
        [SerializeField] private NavMeshAgent _modelAgent;
        [SerializeField] private PlayerAnimator _animator;

        private IEnumerator _movementTracker;
        
        private void Awake()
        {
            _model.StartedMovement += StartMovement;
            _model.DiedFromEnemy += DiedFromEnemy;
            _model.KilledEnemy += KilledEnemy;
        }

        private void DiedFromEnemy(EnemyModel enemy)
        {
            _animator.Die();
        }

        private void KilledEnemy(EnemyModel enemy)
        {
            _animator.Attack();
        }

        private void StartMovement()
        {
            _animator.StartMoving();
            
            if(_movementTracker != null)
                StopCoroutine(_movementTracker);
            StartCoroutine(_movementTracker = WaitForMovementEnd());
        }

        private IEnumerator WaitForMovementEnd()
        {
            yield return null;
            
            while(_modelAgent.hasPath && _modelAgent.remainingDistance > 0.01f) yield return null; // magic numbers, i know
            
            _animator.StopMoving();
        }
    }
}