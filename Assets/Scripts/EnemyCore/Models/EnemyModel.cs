using UnityEngine;
using WeaponsCore;

namespace EnemyCore
{
    public class EnemyModel : MonoBehaviour
    {
        public AttackType AttackType => _attackType;
        public bool HasSpecificAttackType => _hasSpecificAttackType;
        public int Level => _level;

        private AttackType _attackType;
        private bool _hasSpecificAttackType;
        private int _level;
    }
}