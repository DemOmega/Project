using System.Collections;
using TMPro;
using UnityEngine;

namespace Scenes.Script
{
    public class CountdownManager : MonoBehaviour
    {
        public TextMeshProUGUI countdownText;
        public AudioSource audioSource;
        public AudioClip[] countdownClips;
        public float delayBetween = 1f;
        public GameObject gameplayElements;

        private PlayerMovement playerMovement;
        private BaseWeapon weapon;

        private void Start()
        {
            
            playerMovement = FindFirstObjectByType<PlayerMovement>();
            weapon = FindFirstObjectByType<BaseWeapon>();

            if (playerMovement != null) playerMovement.canMove = false;
            if (weapon != null) weapon.canShoot = true;

            StartCoroutine(StartCountdown());
        }

        IEnumerator StartCountdown()
        {
            string[] steps = { "4", "3", "2", "1", "MISSION START"};

            for (int i = 0; i < steps.Length; i++)
            {
                countdownText.text = steps[i];

                if (i < countdownClips.Length && countdownClips[i] != null)
                    audioSource.PlayOneShot(countdownClips[i]);

                yield return new WaitForSeconds(delayBetween);
                countdownText.text = "";
                yield return new WaitForSeconds(0.2f);
            }

            countdownText.gameObject.SetActive(false);

            if (gameplayElements != null)
                gameplayElements.SetActive(true);

            if (playerMovement != null) playerMovement.canMove = true;
            if (weapon != null) weapon.canShoot = true;
        }
    }
}