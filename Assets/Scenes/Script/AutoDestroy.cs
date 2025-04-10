using UnityEngine;

namespace Scenes.Script
{
    public class AutoDestroy : MonoBehaviour
    {
        public float countdown;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Invoke(nameof(Destroy), countdown);
        }

        // Update is called once per frame
        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
