using UnityEngine;

namespace Scenes.Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int score = 0;
        public int kills = 0;
        private float startTime;
        public bool isGameOver = false;

        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Cursor.lockState =CursorLockMode.Locked; 
            Cursor.visible = false; 
            startTime = Time.time;
        }

        public void AddKill(int points)
        {
            score += points;
            kills++;
        }

        public void GameOver()
        {
            isGameOver = true;
            float timeSurvived = Time.time - startTime;
            Debug.Log($"Score: {score}, Kills: {kills}, Time: {timeSurvived:F1} sec");

            // Show UI panel
            // ShowStatsPanel(score, kills, timeSurvived);
        }
    }
}