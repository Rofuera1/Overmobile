using System;
using EnemyCore;
using LootableCore;
using UnityEngine;
using UnityEngine.AI;
using WeaponsCore;

namespace PlayerCore
{
    public class PlayerModel : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private WeaponCollectionSystem _weaponCollectionSystem;
        [Space]
        [SerializeField] private int _startLevel;
        [SerializeField] private int _startWeaponId;

        public event Action LeveledUp;
        public event Action ChangedWeapon;
        public event Action<EnemyModel> KilledEnemy;
        public event Action<EnemyModel> DiedFromEnemy;

        private int _level;
        private Weapon _weapon;

        private void Awake()
        {
            _level = _startLevel;
            _weapon = _weaponCollectionSystem.GetWeapon(_startLevel);
        }

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public bool TryAttackEnemy(EnemyModel enemy)
        {
            if (!CheckAttackConditions(enemy)) return false;
            
            if(_level > enemy.Level) KillEnemy(enemy);
            else DieFromEnemy(enemy);

            return true;
        }

        public void CollectLootable(Lootable lootable)
        {
            lootable.Collect();
            
            if(lootable.Levels > 0) LevelUp(lootable.Levels);
            
            var weapon = _weaponCollectionSystem.GetWeapon(lootable.WeaponId);
            if(weapon != null) PickUpWeapon(weapon);
        }

        public void LevelUp(int levelAmount)
        {
            _level += levelAmount;
            LeveledUp?.Invoke();
        }

        public void PickUpWeapon(Weapon weapon)
        {
            _weapon = weapon; 
            ChangedWeapon?.Invoke();
        }

        private void KillEnemy(EnemyModel enemy)
        {
            KilledEnemy?.Invoke(enemy);
        }

        private void DieFromEnemy(EnemyModel enemy)
        {
            DiedFromEnemy?.Invoke(enemy);
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