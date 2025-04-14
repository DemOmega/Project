using UnityEngine;

namespace Scenes.Script
{
    public class Rifle : BaseWeapon
    {
        
        public override int GetCurrentAmmo() => currentAmmo;
        public override int GetMaxAmmo() => maxAmmo;
        public GameObject bullet;
        public Transform firePoint;
        public float fireRate = 0.1f;
        private float fireCooldown;

        public int maxAmmo = 30;
        private int currentAmmo;
        private bool isReloading = false;
        public float reloadTime = 2f;

        public float sprayAmount = 2f;
        public float sprayIncreasePerShot = 0.5f;
        public float sprayRecovery = 1f;
        public float maxSpray = 5f;
        private float currentSpray = 0f;

        public AudioSource audioSource;
        public AudioClip shootSound;
        public AudioClip reloadSound;

        private void Start()
        {
            currentAmmo = maxAmmo;
        }

        private void Update()
        {
            fireCooldown -= Time.deltaTime;
            currentSpray = Mathf.MoveTowards(currentSpray, 0f, sprayRecovery * Time.deltaTime);

            if (!isReloading)
            {
                if (Input.GetButton("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
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
            direction = Quaternion.Euler(
                Random.Range(-currentSpray, currentSpray),
                Random.Range(-currentSpray, currentSpray),
                0f
            ) * direction;

            Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));

            if (shootSound && audioSource)
                audioSource.PlayOneShot(shootSound);

            currentSpray = Mathf.Clamp(currentSpray + sprayIncreasePerShot, 0f, maxSpray);
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
    }
}
