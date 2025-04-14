using System;
using UnityEngine;

namespace Scenes.Script
{
    public class PlayerHealt : MonoBehaviour
    {
        public static PlayerHealt instance;
        
        public int maxHealt, currentHealt;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            currentHealt = maxHealt;
            UI.instance.healthSlider.maxValue = maxHealt;
            UI.instance.healthSlider.value = currentHealt;
            UI.instance.healthText.text = "HEALTH"+ currentHealt + "/" + maxHealt;
        }

        private void Update()
        {
        }

        public void DamagePlayer(int damage)
        {
            currentHealt -= damage;

            if (currentHealt <= 0)
            {
                gameObject.SetActive(false);
                GameManager.instance.GameOver();
            }
            
            UI.instance.healthSlider.value = currentHealt;
            UI.instance.healthText.text = currentHealt + "/" + maxHealt;
        }
        
        public bool Heal(int amount)
        {
            if (currentHealt >= maxHealt) return false;

            currentHealt = Mathf.Min(currentHealt + amount, maxHealt);
            return true;
        }
    }
}
