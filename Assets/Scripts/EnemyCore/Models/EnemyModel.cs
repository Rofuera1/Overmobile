using UnityEngine;
using WeaponsCore;

namespace EnemyCore
{
    public class EnemyModel : MonoBehaviour
    {
        public AttackType AttackType => _attackType;
        public bool HasSpecificAttackType => _hasSpecificAttackType;
        public int Level => _level;
        public bool Attackable => _attackable;

        [SerializeField] private AttackType _attackType;
        [SerializeField] private bool _hasSpecificAttackType;
        [SerializeField] private int _level;
        [SerializeField] private bool _attackable;

        public void Die()
        {
            
        }

        public void Kill()
        {
            
        }
    }
}