using UnityEngine;

namespace Scenes.Script
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Transform[] spawnPoints;
        public float spawnInterval = 5f;
        private float timer = 0f;
        private int level = 1;

        private void Update()
        {
            timer += Time.deltaTime;

            // Augmenter le niveau toutes les 5 minutes
            int newLevel = Mathf.FloorToInt(Time.time / 300f) + 1;
            if (newLevel != level)
            {
                level = newLevel;
                Debug.Log($"Niveau augmenté ! Nouveau niveau : {level}");
            }

            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnEnemies(level); // Plus d'ennemis selon le niveau
            }
        }

        void SpawnEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            }
        }
    }
}