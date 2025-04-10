using UnityEngine;

namespace Scenes.Script
{
    public class PartyLight : MonoBehaviour
    {
        private Vector3 rotationSpeed;

        void Start()
        {
            // Génère une vitesse de rotation aléatoire
            rotationSpeed = new Vector3(
                Random.Range(10f, 50f), // Rotation aléatoire en X (modérée pour éviter les sauts brutaux)
                Random.Range(30f, 90f), // Rotation plus rapide en Y
                Random.Range(30f, 90f) // Rotation en Z
            );
        }

        void Update()
        {
            // Applique la rotation en Y et Z normalement
            transform.Rotate(0, rotationSpeed.y * Time.deltaTime, rotationSpeed.z * Time.deltaTime);

            // Gère la rotation en X avec une limite entre -180 et 180
            Vector3 currentRotation = transform.eulerAngles;

            // Convertir en plage [-180, 180]
            if (currentRotation.x > 180)
                currentRotation.x -= 360;

            // Ajoute une rotation aléatoire mais limitée en X
            float newX = Mathf.Clamp(currentRotation.x + (rotationSpeed.x * Time.deltaTime), -180f, 180f);

            // Applique la nouvelle rotation
            transform.eulerAngles = new Vector3(newX, currentRotation.y, currentRotation.z);
        }
    }
}