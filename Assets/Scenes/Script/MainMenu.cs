using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Script
{
    public class MainMenu : MonoBehaviour
    {

        public string firstLevel;
        public void PlayGame()
        {
            SceneManager.LoadScene(firstLevel);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
