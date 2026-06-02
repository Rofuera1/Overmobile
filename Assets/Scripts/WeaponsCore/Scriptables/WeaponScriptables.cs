using UnityEngine;

namespace WeaponsCore
{
    [CreateAssetMenu(menuName = "Weapons")]
    public class WeaponScriptables : ScriptableObject
    {
        public WeaponInfo[] Weapons;
    }

    [System.Serializable]
    public class WeaponInfo
    {
        public int Id;
        public AttackType AttackType;
        public int AttackDistance;
    }
}