using TMPro;
using UnityEngine;

namespace Scenes.Script
{
    public class FPSCounter : MonoBehaviour
    {
        public TextMeshProUGUI fpsText;
        public float refreshRate = 0.5f; // Rafraîchissement en secondes

        private float timer;

        void Update()
        {
            timer += Time.unscaledDeltaTime;
            if (timer >= refreshRate)
            {
                int fps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime);
                fpsText.text = fps + " FPS";
                timer = 0f;
            }
        }
    }
}