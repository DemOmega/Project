using TMPro;
using UnityEngine;

namespace Scenes.Script
{
    public class UIAmmoDisplay : MonoBehaviour
    {
        public TextMeshProUGUI ammoText;
        public WeaponManager weaponManager;

        public Color normalColor = Color.white;
        public Color blinkColor = Color.red;
        public float blinkSpeed = 4f;

        private void Update()
        {
            var weapon = weaponManager.GetCurrentWeapon();
            if (weapon != null)
            {
                int currentAmmo = weapon.GetCurrentAmmo();
                int maxAmmo = weapon.GetMaxAmmo();

                ammoText.text = currentAmmo + " / " + maxAmmo;

                if (currentAmmo <= weapon.lowAmmoThreshold)
                {
                    float lerp = Mathf.PingPong(Time.time * blinkSpeed, 1f);
                    ammoText.color = Color.Lerp(normalColor, blinkColor, lerp);
                }
                else
                {
                    ammoText.color = normalColor;
                }
            }
            else
            {
                ammoText.text = "-- / --";
                ammoText.color = normalColor;
            }
        }
    }
}