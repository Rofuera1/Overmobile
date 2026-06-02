using UnityEngine;

namespace PlayerCore
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public void StartMoving() => _animator.SetBool("Running", true);

        public void StopMoving() => _animator.SetBool("Running", false);

        public void Attack()
        {
            
        }

        public void Die()
        {
            
        }
    }
}