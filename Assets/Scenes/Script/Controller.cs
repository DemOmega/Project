using UnityEngine;

namespace Scenes.Script
{
    public class Controller : MonoBehaviour
    {
        public float moveSpeed = 15f;
        public float rotationSpeed = 20f;
        public GameObject head;
        
        private CharacterController _controller;
        private float _verticalRotation = 0f;
        private float _horizontalRotation = 0f;  // Variable pour gérer la rotation horizontale
        public float minYRotation = -50f;
        public float maxYRotation = 50f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'écran
            Cursor.visible = false; // Masque le curseur
        }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            // Déplacement horizontal et vertical
            float moveX = Input.GetAxis("Horizontal") * moveSpeed;
            float moveZ = Input.GetAxis("Vertical") * moveSpeed;
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            
            // Appliquer le mouvement
            _controller.Move(move * Time.deltaTime);

            // Rotation horizontale avec la souris (ou le joystick droit)
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;


            // Inverser la valeur du joystick X pour que la direction soit correcte


            // Ajouter la rotation horizontale avec la souris et le joystick
            _horizontalRotation += mouseX;  // Rotation horizontale combinée
            transform.localRotation = Quaternion.Euler(0, _horizontalRotation, 0);  // Appliquer la rotation horizontale

            // Rotation verticale avec la souris
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            _verticalRotation -= mouseY; // Appliquer la rotation verticale pour la souris
            _verticalRotation = Mathf.Clamp(_verticalRotation, minYRotation, maxYRotation);
            head.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(head.transform.position, head.transform.forward * 1000);
        }
    }
}
