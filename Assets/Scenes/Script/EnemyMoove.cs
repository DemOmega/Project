using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Script
{
    public class EnemyMoove : MonoBehaviour
    {
        [Header("Mouvement")]
        public float distanceToStop = 50f;
        public float rotationSpeed = 5f;

        [Header("Tir")]
        public GameObject bulletPrefab;
        public Transform firePoint;
        public float maxShootingDistance = 150f;
        public float fireAngleLimit = 30f;
        public LayerMask visionMask;

        [Header("Cadence de tir")]
        public float minFireDelay = 0.1f;
        public float maxFireDelay = 0.4f;

        [Header("Spray")]
        public float sprayIncreasePerShot = 0.5f;
        public float sprayRecoverySpeed = 1f;
        public float maxSpray = 5f;

        [Header("Animation")]
        public Animator animator;

        private float fireTimer = 0f;
        private float nextFireTime = 0f;
        private float currentSpray = 0f;

        private NavMeshAgent agent;
        private Transform target;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            // Vérifier si le NavMeshAgent est attaché
            if (agent == null)
            {
                Debug.LogError("Le NavMeshAgent n'a pas été trouvé !");
                return;
            }

            target = Controller.instance.transform;
            nextFireTime = Random.Range(minFireDelay, maxFireDelay);
        }

        private void Update()
        {
            if (target == null) return;

            Vector3 playerPos = target.position;
            float distance = Vector3.Distance(transform.position, playerPos);

            // Déplacement : Toujours essayer de se déplacer vers le joueur
            agent.SetDestination(playerPos);

            // Debug: Vérifier si la destination est mise à jour
            Debug.Log("Destination actuelle de l'ennemi: " + playerPos);

            // Vérification du mouvement de l'agent
            if (agent.velocity.magnitude > 0)
            {
                Debug.Log("L'ennemi se déplace !");
            }
            else
            {
                Debug.Log("L'ennemi ne bouge pas !");
            }

            // Animation de déplacement
            if (animator)
                animator.SetBool("isMoving", distance > distanceToStop);

            // Rotation de l'ennemi vers le joueur
            Vector3 flatDir = new Vector3((playerPos - transform.position).x, 0, (playerPos - transform.position).z);
            if (flatDir.magnitude > 0.1f)
            {
                Quaternion lookRot = Quaternion.LookRotation(flatDir.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
            }

            firePoint.LookAt(playerPos);

            // Vision et tir
            Ray ray = new Ray(firePoint.position, (playerPos - firePoint.position).normalized);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, visionMask))
            {
                if (hit.collider.CompareTag("Player") && distance <= maxShootingDistance)
                {
                    fireTimer += Time.deltaTime;
                    currentSpray = Mathf.MoveTowards(currentSpray, 0f, sprayRecoverySpeed * Time.deltaTime);

                    if (fireTimer >= nextFireTime && Controller.instance.gameObject.activeInHierarchy)
                    {
                        Vector3 dir = (playerPos - firePoint.position).normalized;
                        dir = Quaternion.Euler(
                            Random.Range(-currentSpray, currentSpray),
                            Random.Range(-currentSpray, currentSpray),
                            0f
                        ) * dir;

                        float angle = Vector3.Angle(transform.forward, dir);
                        if (angle < fireAngleLimit)
                        {
                            Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));
                            currentSpray = Mathf.Clamp(currentSpray + sprayIncreasePerShot, 0f, maxSpray);
                            fireTimer = 0f;
                            nextFireTime = Random.Range(minFireDelay, maxFireDelay);

                            if (animator)
                                animator.SetTrigger("fire");
                        }
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            // Vérification visuelle de la direction de tir et de la portée maximale
            if (firePoint != null && target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(firePoint.position, (target.position - firePoint.position).normalized * maxShootingDistance);
            }
        }
    }
}
