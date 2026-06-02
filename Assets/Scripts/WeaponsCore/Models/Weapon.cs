namespace WeaponsCore
{
    public class Weapon
    {
        public AttackType AttackType => _attackType;
        public float AttackDistance => _attackDistance; 

        private AttackType _attackType;
        private float _attackDistance;

        public Weapon(AttackType attackType, float attackDistance)
        {
            _attackType = attackType;
            _attackDistance = attackDistance;
        }
    }
}