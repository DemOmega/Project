using UnityEngine;
using UnityEngine.SceneManagement;
namespace Scenes.Script

{
    public class Pause : MonoBehaviour
    {
        
        public string mainMenu;
        
        public void Resume()
        {
            GameManager.instance.PauseUnpause();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenu);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
