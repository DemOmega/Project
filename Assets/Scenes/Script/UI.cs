using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

namespace Scenes.Script
{
    public class UI : MonoBehaviour
    {
        public static UI instance;
        public Slider healthSlider;
        public TextMeshProUGUI healthText;

        public GameObject pauseScreen;

        public Image damageEffect;
        
        public float damageAlpha = 0.3f, damageFadeSpeed = 3f;

        private void Awake()
        {
            instance = this;
        }

        void Update()
        {
            if (damageEffect.color.a != 0)
            {
                damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a,0f, damageFadeSpeed*Time.deltaTime));
            }
        }

        public void ShowDamage()
        {
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, 0.3f);
        }

    }
}
