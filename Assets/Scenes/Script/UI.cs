using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Script
{
    public class UI : MonoBehaviour
    {
        public static UI instance;
        public Slider healthSlider;
        public TextMeshProUGUI healthText;

        private void Awake()
        {
            instance = this;
        }

    }
}
