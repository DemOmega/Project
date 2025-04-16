using UnityEngine;

namespace Scenes.Script
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyToSpawn;
        
        public float timeToSpawn;
        private float spawnCounter;

        void Start()
        {
            spawnCounter += Time.deltaTime;
        }

        void Update()
        {
            spawnCounter-=Time.deltaTime;
            
            if((spawnCounter<=0))
            {
                spawnCounter = timeToSpawn;
                
                Instantiate(enemyToSpawn, transform.position, transform.rotation);
            }
        }
    }
}