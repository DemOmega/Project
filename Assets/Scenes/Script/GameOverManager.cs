using UnityEngine;
using TMPro;

namespace Scenes.Script
{
    public class GameOverUI : MonoBehaviour
    {
        public TextMeshProUGUI timeText;

        void Start()
        {
            timeText.text = "Temps : " + GameManager.finalTime.ToString("F1") + " sec";
        }
    }
}