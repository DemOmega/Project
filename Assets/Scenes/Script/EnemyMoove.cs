using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Scenes.Script
{
    public class EnemyMoove : MonoBehaviour
    
    {
        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip shootSound;
        public AudioClip deathSound;
        
        [Header("Déplacement")]
        public float distanceToStop = 10f;

        [Header("Tir")]
        public GameObject bullet;
        public Transform firePoint;
        public float fireRate = 0.15f;
        public float shootingDistance = 30f;
        public LayerMask visionMask;
        public LayerMask obstacleMask;
        public int maxShotsBeforePause = 30;
        public float sprayAngle = 3f; // 🔸Ajout: angle de dispersion

        private int shotCount = 0;
        private float fireCooldown = 0f;
        private Transform target;
        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
            else
                Debug.LogError("Joueur non trouvé (tag 'Player')");

            if (firePoint == null)
                Debug.LogError("FirePoint non assigné !");
        }

        private void Update()
        {
            if (target == null || agent == null) return;

            float distance = Vector3.Distance(transform.position, target.position);

            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            transform.forward = direction;

            fireCooldown -= Time.deltaTime;

            Vector3 dirToPlayer = (target.position - firePoint.position).normalized;
            float distanceToPlayer = Vector3.Distance(firePoint.position, target.position);

            if (!Physics.Raycast(firePoint.position, dirToPlayer, distanceToPlayer, obstacleMask))
            {
                if (Physics.Raycast(firePoint.position, dirToPlayer, out RaycastHit hitInfo, shootingDistance, visionMask))
                {
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        if (fireCooldown <= 0f)
                        {
                            fireCooldown = fireRate;

                            if (shotCount < maxShotsBeforePause)
                            {
                                Shoot(hitInfo.point);
                                shotCount++;
                            }
                            else
                            {
                                StartCoroutine(PauseBeforeNextFire());
                            }
                        }

                        return;
                    }
                }
            }

            if (distance > distanceToStop)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
            GameManager.instance.AddKill(10);
        }

        private void Shoot(Vector3 targetPoint)
        {
            if (shootSound && audioSource)
                audioSource.PlayOneShot(shootSound);
            // 🔸 Ajout du spray (variation aléatoire de direction)
            Vector3 baseDirection = (targetPoint - firePoint.position).normalized;

            Quaternion sprayRotation = Quaternion.Euler(
                Random.Range(-sprayAngle, sprayAngle),
                Random.Range(-sprayAngle, sprayAngle),
                0f
            );

            Vector3 sprayedDirection = sprayRotation * baseDirection;

            Instantiate(bullet, firePoint.position, Quaternion.LookRotation(sprayedDirection));
        }

        private IEnumerator PauseBeforeNextFire()
        {
            yield return new WaitForSeconds(2f);
            shotCount = 0;
        }

        private void OnDrawGizmosSelected()
        {
            if (firePoint != null && target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(firePoint.position, target.position);
            }
        }
        
    }
}
