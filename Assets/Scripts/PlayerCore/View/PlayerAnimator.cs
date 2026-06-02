using System;
using UnityEngine;

namespace PlayerCore
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _swordAnimator;
        [SerializeField] private Animator _noWeaponAnimator;

        private Animator _currentAnimator;

        private void Awake()
        {
            _currentAnimator = _noWeaponAnimator;
        }

        public void EquipWeapon() => _currentAnimator = _swordAnimator;
        
        public void StartMoving() => _currentAnimator.SetBool("Running", true);

        public void StopMoving() => _currentAnimator.SetBool("Running", false);

        public void Attack()
        {
            
        }

        public void Die()
        {
            
        }
    }
}