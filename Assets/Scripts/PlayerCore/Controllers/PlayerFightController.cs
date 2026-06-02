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
        }

        private void DiedFromEnemy(EnemyModel enemy)
        {
            enemy.Kill();
        }
    }
}