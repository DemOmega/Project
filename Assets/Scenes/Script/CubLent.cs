using UnityEngine;

namespace Scenes.Script
{
    public class CubLent : MonoBehaviour
    {
        private Vector3 rotationSpeed;

        void Start()
        {
            // Génère une vitesse de rotation aléatoire mais lente
            rotationSpeed = new Vector3(
                Random.Range(5f, 7f),  // Rotation lente sur X
                Random.Range(5f, 7f),  // Rotation lente sur Y
                Random.Range(5f, 7f)   // Rotation lente sur Z
            );
        }

        void Update()
        {
            // Applique la rotation douce
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
