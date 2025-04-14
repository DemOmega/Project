using UnityEngine;

namespace Scenes.Script
{
    public class EnnemyHealt : MonoBehaviour
    {
        
        public int currentHealth, maxHealth;
        public AudioSource audioSource;
        public AudioClip deathSound;

        void Start()
        {
            currentHealth = maxHealth;
        }


        void Update()
        {
        
        }

        public void DamageEnemy(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        void Die()
        {
            if (deathSound && audioSource)
                audioSource.PlayOneShot(deathSound);
            
            Destroy(gameObject, deathSound.length);
        }
    }
}

