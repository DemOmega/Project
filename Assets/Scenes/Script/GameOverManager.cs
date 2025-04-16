using UnityEngine;
using TMPro;

namespace Scenes.Script
{
    public class GameOverUI : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI killsText;
        public TextMeshProUGUI timeText;

        void Start()
        {
            scoreText.text = "Score: " + GameManager.finalScore;
            killsText.text = "Kills: " + GameManager.finalKills;
            timeText.text = "Temps: " + GameManager.finalTime.ToString("F1") + " sec";
        }
    }
}