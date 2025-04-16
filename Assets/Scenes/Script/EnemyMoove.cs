using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Script
{
    public class EnemyMoove : MonoBehaviour
    {
        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip deathSound;

        [Header("Déplacement")]
        public float distanceToStop = 10f;
        public float attackRange = 2f;         // Distance d'attaque
        public int attackDamage = 10;          // Dégâts infligés
        public float attackCooldown = 1f;      // Temps entre deux attaques

        private float attackTimer = 0f;
        private Transform target;
        private NavMeshAgent agent;
        private PlayerHealt playerHealth;      // Référence au script santé du joueur

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                playerHealth = player.GetComponent<PlayerHealt>();
                if (playerHealth == null)
                {
                    Debug.LogError("Le script 'PlayerHealt' est manquant sur le joueur !");
                }
            }
            else
            {
                Debug.LogError("Joueur non trouvé (tag 'Player')");
            }
        }

        private void Update()
        {
            if (target == null || agent == null) return;

            float distance = Vector3.Distance(transform.position, target.position);
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f;
            transform.forward = direction;

            // Attaque si dans la portée
            if (distance <= attackRange)
            {
                if (attackTimer <= 0f)
                {
                    Attack();
                    attackTimer = attackCooldown;
                }
            }
            else if (distance > distanceToStop)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }

            attackTimer -= Time.deltaTime;
        }

        private void Attack()
        {
            if (playerHealth != null && Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                playerHealth.DamagePlayer(attackDamage);

                if (audioSource && deathSound)
                {
                    audioSource.PlayOneShot(deathSound);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
