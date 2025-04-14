using System;
using UnityEngine;

namespace Scenes.Script
{
    public class HelthPickup : MonoBehaviour
    {
        public int heal;
        public int healAmount = 10;
        public float cooldown = 60f;
        private bool isAvailable = true;

        private void OnTriggerEnter(Collider other)
        {
            if (!isAvailable) return;

            PlayerHealt player = other.GetComponent<PlayerHealt>();
            if (player != null)
            {
                if (player.Heal(healAmount))
                {
                    isAvailable = false;
                    StartCoroutine(RespawnCooldown());
                    gameObject.SetActive(false); // Désactive visuellement
                }
            }
        }

        private System.Collections.IEnumerator RespawnCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            isAvailable = true;
            gameObject.SetActive(true);
        }
    }
}
