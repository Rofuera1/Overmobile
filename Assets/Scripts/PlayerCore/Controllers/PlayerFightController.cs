using System;
using EnemyCore;
using UnityEngine;

namespace PlayerCore
{
    public class PlayerFightController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _model;

        private void Awake()
        {
            _model.KilledEnemy += KilledEnemy;
            _model.DiedFromEnemy += DiedFromEnemy;
        }

        private void KilledEnemy(EnemyModel enemy)
        {
            enemy.Die();
            
            _model.LevelUp(enemy.Level);
            _model.PickUpWeapon(enemy.RewardWeaponId);
        }

        private void DiedFromEnemy(EnemyModel enemy)
        {
            enemy.KillPlayer();
        }
    }
}