using UnityEngine;
using System.Collections;

namespace Scenes.Script
{
    public class Revolver : BaseWeapon
    {
        public GameObject bullet;
        public Transform firePoint;
        public float fireRate = 0.5f;
        private float fireCooldown;

        public int maxAmmo = 6;
        private int currentAmmo;
        private bool isReloading = false;
        public float reloadTime = 1.5f;
        private Coroutine reloadCoroutine;

        public AudioSource audioSource;
        public AudioClip shootSound;
        public AudioClip reloadSound;

        public GameObject muzzleFlash;

        private void OnEnable()
        {
            // Initialisation des munitions si l'arme est activée
            if (currentAmmo <= 0)
                currentAmmo = maxAmmo;

            canShoot = true;
            isReloading = false;
        }

        private void OnDisable()
        {
            CancelReload();
            canShoot = false;
        }

        private void Update()
        {
            if (!canShoot || isReloading) return;

            fireCooldown -= Time.deltaTime;

            if (!isReloading)
            {
                if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
                    Shoot();

                if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading)
                    reloadCoroutine = StartCoroutine(Reload());
            }
        }

        void Shoot()
        {
            fireCooldown = fireRate;
            currentAmmo--;

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 targetPoint = ray.GetPoint(100f);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f)) targetPoint = hit.point;

            Vector3 direction = (targetPoint - firePoint.position).normalized;

            Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));

            
            if (shootSound && audioSource)
                audioSource.PlayOneShot(shootSound);

            
            if (muzzleFlash != null)
            {
                StartCoroutine(ShowMuzzleFlash());
            }
        }

        IEnumerator Reload()
        {
            isReloading = true;
            canShoot = false;

            if (reloadSound && audioSource)
                audioSource.PlayOneShot(reloadSound);

            yield return new WaitForSeconds(reloadTime);

            currentAmmo = maxAmmo;
            isReloading = false;
            canShoot = true;
        }

        IEnumerator ShowMuzzleFlash()
        {
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            muzzleFlash.SetActive(false);
        }

        public override void CancelReload()
        {
            if (isReloading && reloadCoroutine != null)
            {
                StopCoroutine(reloadCoroutine);
                reloadCoroutine = null;
                isReloading = false;
                canShoot = true;
            }
        }

        public override int GetCurrentAmmo() => currentAmmo;
        public override int GetMaxAmmo() => maxAmmo;
    }
}
