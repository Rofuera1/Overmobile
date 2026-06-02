using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponsCore
{
    public class WeaponCollectionSystem : MonoBehaviour
    {
        [SerializeField] private WeaponScriptables _scriptables;

        private Dictionary<int, Weapon> _weapons;

        private void Awake()
        {
            _weapons = new();
            foreach (var weapon in _scriptables.Weapons)
            {
                var weaponData = new Weapon(weapon.AttackType, weapon.AttackDistance);
                _weapons.Add(weapon.Id, weaponData); // if collision will occur, it will just throw an Exception. no need to adjust for this type of stuff yk
            }
        }

        public Weapon GetWeapon(int id)
        {
            if (_weapons.TryGetValue(id, out var weapon)) return weapon;

            return null;
        }
    }
}