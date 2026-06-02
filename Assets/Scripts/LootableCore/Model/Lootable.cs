using System;
using UnityEngine;

namespace LootableCore
{
    public class Lootable : MonoBehaviour
    {
        public int WeaponId => _weaponId;
        public int Levels => _level;

        [SerializeField] private int _weaponId;
        [SerializeField] private int _level;

        public event Action OnCollected;
        
        public void Collect()
        {
            OnCollected?.Invoke();
        }
    }
}