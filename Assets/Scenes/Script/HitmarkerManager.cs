using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Script
{
    public class HitmarkerManager : MonoBehaviour
    {
        public static HitmarkerManager instance;

        public Image hitmarkerImage;
        public float displayTime = 0.1f;
        public AudioSource audioSource;
        public AudioClip hitSound;

        private void Awake()
        {
            instance = this;
            hitmarkerImage.enabled = false;
        }

        public void ShowHitmarker()
        {
            if (hitSound && audioSource)
                audioSource.PlayOneShot(hitSound);

            StopAllCoroutines();
            StartCoroutine(ShowAndHide());
        }

        private System.Collections.IEnumerator ShowAndHide()
        {
            hitmarkerImage.enabled = true;
            yield return new WaitForSeconds(displayTime);
            hitmarkerImage.enabled = false;
        }
    }
}