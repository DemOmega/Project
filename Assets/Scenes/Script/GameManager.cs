using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int score = 0;
        public int kills = 0;
        private float startTime;
        public bool isGameOver = false;

        // Variables statiques accessibles depuis la scène GameOver
        public static int finalScore;
        public static int finalKills;
        public static float finalTime;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Cursor.lockState =CursorLockMode.Locked; 
            startTime = Time.time;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseUnpause();
            }
        }

        public void AddKill(int points)
        {
            score += points;
            kills++;
        }

        public void GameOver()
        {
            isGameOver = true;

            // Sauvegarde des infos finales
            finalScore = score;
            finalKills = kills;
            finalTime = Time.time - startTime;

            // Chargement de la scène FinalScren (jai chié sur le "e")
            SceneManager.LoadScene("FinalScren");
            
        }

        public void PauseUnpause()
        {
            if (UI.instance.pauseScreen.activeInHierarchy)
            {
                UI.instance.pauseScreen.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
            else
            {
                UI.instance.pauseScreen.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                
                Time.timeScale = 0f;
            }
        }
    }
}