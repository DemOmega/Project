using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Script
{
    public class WeaponControllerPad : MonoBehaviour
    {
        public GameObject[] weapons; // Liste des armes
        private int currentWeaponIndex = 0;

        private void Start()
        {
            UpdateWeapon();
        }

        public void OnSwitchWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                // Changer d'arme
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
                UpdateWeapon();
            }
        }

        private void UpdateWeapon()
        {
            // Désactiver toutes les armes sauf celle sélectionnée
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(i == currentWeaponIndex);
            }
        }
    }
}