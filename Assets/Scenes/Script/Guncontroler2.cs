using UnityEngine;

namespace Scenes.Script
{
    public class Guncontroler2 : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public GameObject particulePrefab;
        public Transform raycastOrigin;
        public float power = 10f;
        public int bulletCount = 6; 
        public float spreadAngle = 10f; 

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootShotgun();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out var hitInfo))
                {
                    Instantiate(particulePrefab, hitInfo.point, Quaternion.Euler(hitInfo.transform.forward));
                }
            }
        }

        void ShootShotgun()
        {
            for (int i = 0; i < bulletCount; i++)
            {
                
                float randomX = Random.Range(-spreadAngle, spreadAngle);
                float randomY = Random.Range(-spreadAngle, spreadAngle);
                
                Quaternion spreadRotation = Quaternion.Euler(randomX, randomY, 0);
                Vector3 shootDirection = spreadRotation * transform.forward; 
                
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.AddForce(shootDirection * power, ForceMode.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(transform.position, transform.forward * 1000);
        }
    }
}
