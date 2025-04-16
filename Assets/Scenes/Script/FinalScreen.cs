using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Script
{
    public class FinalScreen : MonoBehaviour
    {
        public string mainMenuScene = "MainMenu"; 
        public string levelScene = "Lvl1";        

        public void QuitGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenuScene);
        }

        public void Replay()
        {
            SceneManager.LoadScene(levelScene);
        }
    }
}