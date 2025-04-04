using UnityEngine;

namespace Scenes.Script
{
    public class Controller : MonoBehaviour
    {
        public float moveSpeed = 10f, gravityforce = 3f, jumpForce = 7f, sprintMultiplier = 1.5f;
        public float rotationSpeed = 200f;
        public GameObject head;

        private CharacterController _controller;
        private float _verticalRotation = 0f;
        private float _horizontalRotation = 0f;
        public float minYRotation = -50f;
        public float maxYRotation = 50f;
        private Vector3 _velocity;
        private bool _isGrounded;
        public Transform groundCheckPoint;
        public LayerMask groundLayer;
        
        public Animator animator;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            // Vérification du sol
            _isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.3f, groundLayer);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            // Gestion du sprint
            float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                speed = sprintMultiplier; // Sprint activé si LeftShift est enfoncé
            }

            // Déplacement horizontal
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 move = (transform.right * moveX + transform.forward * moveZ)* speed;
            _controller.Move(move * Time.deltaTime);

            // Saut
            if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            }

            // Appliquer la gravité
            _velocity.y += Physics.gravity.y * gravityforce * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);

            // Rotation horizontale
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            _horizontalRotation += mouseX;
            transform.localRotation = Quaternion.Euler(0, _horizontalRotation, 0);

            // Rotation verticale
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, minYRotation, maxYRotation);
            head.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
            
            animator.SetFloat("movespeed", move.magnitude);
            animator.SetBool("onGround", _isGrounded);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, 0.3f);
        }
    }
}