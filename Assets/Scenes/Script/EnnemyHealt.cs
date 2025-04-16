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
            
            if (GameManager.instance != null)
            {
                GameManager.instance.AddKill(10); // Gagne 10 points ici !
            }

            Destroy(gameObject);
        }
    }
}

