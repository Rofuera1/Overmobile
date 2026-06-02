using System;
using EnemyCore;
using LootableCore;
using UnityEngine;

namespace PlayerCore
{
    public class PlayerWorldInteractor : MonoBehaviour
    {
        [SerializeField] private PlayerModel _model;

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<EnemyModel>();
            if (enemy)
                _model.TryAttackEnemy(enemy);
            
            var lootable = other.GetComponent<Lootable>();
            if (lootable)
                _model.CollectLootable(lootable);
        }
    }
}