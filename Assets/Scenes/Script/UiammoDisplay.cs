using TMPro;
using UnityEngine;

namespace Scenes.Script
{
    public class UIAmmoDisplay : MonoBehaviour
    {
        public TextMeshProUGUI ammoText;
        public WeaponManager weaponManager;

        private void Update()
        {
            var weapon = weaponManager.GetCurrentWeapon();
            if (weapon != null)
            {
                ammoText.text = weapon.GetCurrentAmmo() + " / " + weapon.GetMaxAmmo();
            }
            else
            {
                ammoText.text = "-- / --";
            }
        }
    }
}