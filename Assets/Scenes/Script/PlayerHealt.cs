using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Script
{
    public class PlayerHealt : MonoBehaviour
    {
        public static PlayerHealt instance;
        
        public int maxHealt, currentHealt;
        
        public AudioSource audioSource;
        public AudioClip hurtClip;

        public float timeUntilFinalScreen = 1f;

        public string finalScreenScene;

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

            UI.instance.ShowDamage();
            
            if (hurtClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(hurtClip);
            }

            if (currentHealt <= 0)
            {
                currentHealt = 0;
                StartCoroutine(WaitingForFinalScreen());
            }
            
            UI.instance.healthSlider.value = currentHealt;
            UI.instance.healthText.text = currentHealt + "/" + maxHealt;
        }
        
        public bool Heal(int amount)
        {
            if (currentHealt >= maxHealt) return false;

            currentHealt = Mathf.Min(currentHealt + amount, maxHealt);
            UI.instance.healthSlider.value = currentHealt;
            UI.instance.healthText.text = currentHealt + "/" + maxHealt;
            return true;
            
        }

        public IEnumerator WaitingForFinalScreen()
        {
            yield return new WaitForSeconds(timeUntilFinalScreen);
            
            SceneManager.LoadScene(finalScreenScene);

            Cursor.lockState = CursorLockMode.None;
        }
    }
}
