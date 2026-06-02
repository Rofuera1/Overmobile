using UnityEngine;

namespace PlayerCore
{
    public class PlayerSkinView : MonoBehaviour
    {
        [SerializeField] private GameObject _noWeapon;
        [SerializeField] private GameObject _weaponEquipped;

        public void SetWeapon(bool weaponActive)
        {
            _noWeapon.SetActive(!weaponActive);
            _weaponEquipped.SetActive(weaponActive);
        }
    }
}