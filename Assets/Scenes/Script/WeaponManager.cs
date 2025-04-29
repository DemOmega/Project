using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Script
{
    public class WeaponManager : MonoBehaviour
    {
        public List<GameObject> weapons = new();
        private int currentWeaponIndex = 0;
        public bool canMove = true;

        private BaseWeapon currentWeapon;

        private void Start()
        {
            if (weapons.Count > 0)
                UpdateWeapon();
        }

        private void Update()
        {
            if (!canMove || UI.instance.pauseScreen.activeInHierarchy || weapons.Count == 0)
                return;

            HandleWeaponSwitchInput();
        }

        private void HandleWeaponSwitchInput()
        {
            bool switched = false;
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0f)
            {
                currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
                switched = true;
            }
            else if (scroll < 0f)
            {
                currentWeaponIndex--;
                if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Count - 1;
                switched = true;
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    currentWeaponIndex = i;
                    switched = true;
                }
            }

            if (switched)
                UpdateWeapon();
        }

        private void UpdateWeapon()
        {
            if (currentWeapon != null)
                currentWeapon.CancelReload();

            for (int i = 0; i < weapons.Count; i++)
                weapons[i].SetActive(i == currentWeaponIndex);

            currentWeapon = weapons[currentWeaponIndex].GetComponent<BaseWeapon>();
            currentWeapon.canShoot = true;
        }

        public void AddWeapon(GameObject newWeapon)
        {
            weapons.Add(newWeapon);
            currentWeaponIndex = weapons.Count - 1;
            UpdateWeapon();
        }

        public BaseWeapon GetCurrentWeapon()
        {
            return currentWeapon;
        }
    }
}
