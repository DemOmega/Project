using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Script
{
    public class GunController2 : MonoBehaviour
    {
        public Transform firePoint; // L'endroit d'où part la balle
        public LineRenderer bulletTrail; // Ligne pour simuler la balle
        public Light muzzleFlash; // Flash au bout du canon
        public float range = 100f; // Portée de la balle
        public float fireRate = 0.2f; // Temps entre chaque tir
        public float bulletTrailTime = 0.05f; // Durée du bullet trail
        public GameObject impactFramePrefabs;
        public GameObject impactEffectPrefabs;

        private float nextFireTime = 0f;
        private bool isShooting = false;

        public void OnShoot(InputAction.CallbackContext context)
        {
            
            if (context.performed)
            {
                isShooting = true;
                Shoot();
            }
            else if (context.canceled)
            {
                isShooting = false;
            }
        }

        private void Shoot()
        {
            if (Time.time < nextFireTime) return; // Empêche de tirer trop vite

            nextFireTime = Time.time + fireRate;

            // Flash de tir
            if (muzzleFlash != null)
            {
                muzzleFlash.enabled = true;
                Invoke(nameof(HideMuzzleFlash), 0.05f);
            }

            // Raycast pour détecter l'impact
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
            {
                GameObject impactVFX = Instantiate(impactEffectPrefabs, hit.point, Quaternion.identity);
                GameObject impact = Instantiate(impactFramePrefabs, hit.point, Quaternion.LookRotation(hit.normal));

                impact.transform.localScale *= 0.5f;
                // Afficher l'impact
                if (impactEffectPrefabs != null)
                {
                    Instantiate(impactEffectPrefabs, hit.point, Quaternion.LookRotation(hit.normal));
                }

                // Simuler un bullet trail
                if (bulletTrail != null)
                {
                    StartCoroutine(ShowBulletTrail(hit.point));
                }
            }
        }

        private void HideMuzzleFlash()
        {
            if (muzzleFlash != null) muzzleFlash.enabled = false;
        }

        private System.Collections.IEnumerator ShowBulletTrail(Vector3 hitPoint)
        {
            if (bulletTrail != null)
            {
                bulletTrail.enabled = true;
                bulletTrail.SetPosition(0, firePoint.position);
                bulletTrail.SetPosition(1, hitPoint);
                yield return new WaitForSeconds(bulletTrailTime);
                bulletTrail.enabled = false;
            }
        }
    }
}