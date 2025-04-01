using UnityEngine;

namespace Scenes.Script
{
    public class ImpactFrame : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Destroy(gameObject, 5f);
        }

       
    }
}
