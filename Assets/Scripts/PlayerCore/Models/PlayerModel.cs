using System;
using EnemyCore;
using UnityEngine;
using UnityEngine.AI;
using WeaponsCore;

namespace PlayerCore
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        public event Action LevelUp;
        public event Action ChaneWeapon;

        private int _level;
        private Weapon _weapon;
        
        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public void AttackEnemy(EnemyModel enemy)
        {
            if (!CheckAttackConditions(enemy)) return;
            
            if(_level > enemy.Level) KillEnemy(enemy);
            else DieFromEnemy(enemy);
        }

        public void PickUpLevel(int levelAmount)
        {
            
        }

        public void PickUpWeapon(Weapon weapon)
        {
             
        }

        private void KillEnemy(EnemyModel enemy)
        {
            
        }

        private void DieFromEnemy(EnemyModel enemy)
        {
            
        }

        private void ChangeLevel(int newLevel)
        {
            _level = newLevel;
            LevelUp?.Invoke();
        }

        private bool CheckAttackConditions(EnemyModel enemy)
        {
            if(enemy.HasSpecificAttackType)
                if (_weapon.AttackType != enemy.AttackType)
                    return false;

            var distance = DistanceToEnemy(enemy);
            if (distance > _weapon.AttackDistance)
                return false;

            return true;
        }

        private float DistanceToEnemy(EnemyModel enemy) => Vector3.Distance(_agent.transform.position, enemy.transform.position);
    }
}