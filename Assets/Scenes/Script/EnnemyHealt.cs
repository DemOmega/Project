using UnityEngine;

namespace Scenes.Script
{
    public class EnnemyHealt : MonoBehaviour
    {
        
        public int currentHealth, maxHealth;

        void Start()
        {
            currentHealth = maxHealth;
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
            Destroy(gameObject);
        }
    }
}

