using UnityEngine;

namespace Scenes.Script
{
    public class Revolver : BaseWeapon
    {
        
        public override int GetCurrentAmmo() => currentAmmo;
        public override int GetMaxAmmo() => maxAmmo;
        public GameObject bullet;
        public Transform firePoint;
        public float fireRate = 0.5f;
        private float fireCooldown;

        public int maxAmmo = 6;
        private int currentAmmo;
        private bool isReloading = false;
        public float reloadTime = 1.5f;

        public AudioSource audioSource;
        public AudioClip shootSound;
        public AudioClip reloadSound;
        
        public GameObject muzzleFlash;
   

        private void Start()
        {
            currentAmmo = maxAmmo;
            canShoot = true;

        }

        private void Update()
        {
            if (!canShoot || isReloading) return;
            Debug.Log("canShoot: " + canShoot + ", isReloading: " + isReloading);


            fireCooldown -= Time.deltaTime;

            if (!isReloading)
            {
                if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
                    Shoot();

                if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
                    StartCoroutine(Reload());
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

            // 🔊 Son du tir
            if (shootSound && audioSource)
                audioSource.PlayOneShot(shootSound);

            // ✨ Affiche le muzzle flash
            if (muzzleFlash != null)
            {
                StartCoroutine(ShowMuzzleFlash());
            }
        }

        System.Collections.IEnumerator Reload()
        {
            isReloading = true;
            if (reloadSound && audioSource)
                audioSource.PlayOneShot(reloadSound);

            yield return new WaitForSeconds(reloadTime);

            currentAmmo = maxAmmo;
            isReloading = false;
        }
        
        System.Collections.IEnumerator ShowMuzzleFlash()
        {
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            muzzleFlash.SetActive(false);
        }
    }
}