using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public GameObject pauseMenuUI;
        private bool isPaused;
        
        private float startTime;
        public bool isGameOver = false;
        
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

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            startTime = Time.time;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;

            pauseMenuUI.SetActive(isPaused);
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isPaused;
            Time.timeScale = isPaused ? 0f : 1f;

            SetGameplayEnabled(!isPaused);
        }

        public void ResumeGame()
        {
            isPaused = false;
            pauseMenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;

            SetGameplayEnabled(true);
        }

        public void LoadMainMenu(string menuSceneName)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(menuSceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void GameOver()
        {
            if (isGameOver) return;

            isGameOver = true;
            finalTime = Time.time - startTime;

        
            Debug.Log("Final Time: " + finalTime);

            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("FinalScren");
        }

        public void SetGameplayEnabled(bool enabled)
        {
            // Bloque toutes les armes
            BaseWeapon[] weapons = FindObjectsOfType<BaseWeapon>();
            foreach (var weapon in weapons)
                weapon.canShoot = enabled;

            // Bloque les mouvements du joueur
            PlayerMovement movement = FindObjectOfType<PlayerMovement>();
            if (movement != null)
                movement.canMove = enabled;
        }
    }
}
