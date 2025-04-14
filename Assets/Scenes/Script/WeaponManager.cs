using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Script
{
    public class WeaponManager : MonoBehaviour
    {
        public List<GameObject> weapons = new List<GameObject>(); // Liste dynamique des armes
        private int currentWeaponIndex = 0;

        private void Start()
        {
            UpdateWeapon();
        }

        private void Update()
        {
            // Changement d'arme avec touches 1, 2, etc.
            for (int i = 0; i < weapons.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    currentWeaponIndex = i;
                    UpdateWeapon();
                }
            }

            // Changement d'arme avec la molette de la souris
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f) // Molette vers le haut
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
                UpdateWeapon();
            }
            else if (scroll < 0f) // Molette vers le bas
            {
                currentWeaponIndex--;
                if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Count - 1;
                UpdateWeapon();
            }
        }

        private void UpdateWeapon()
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(i == currentWeaponIndex);
            }
        }

        // Fonction pour ajouter une arme dynamiquement
        public void AddWeapon(GameObject newWeapon)
        {
            weapons.Add(newWeapon);
            currentWeaponIndex = weapons.Count - 1;
            UpdateWeapon();
        }
        
        public BaseWeapon GetCurrentWeapon()
        {
            return weapons[currentWeaponIndex].GetComponent<BaseWeapon>();
        }
    }
}