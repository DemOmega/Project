using UnityEngine;

namespace Scenes.Script
{
    public class EnnemyHealt : MonoBehaviour
    {
        
        public int currentHealth;

        void Start()
        {
        
        }


        void Update()
        {
        
        }

        public void DamageEnemy(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
