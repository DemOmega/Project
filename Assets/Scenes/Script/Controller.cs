using UnityEngine;

namespace Scenes.Script
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        
        public bool canMove = true;
        [Header("Déplacement")]
        public float moveSpeed = 10f, gravityForce = 3f, jumpForce = 7f, sprintMultiplier = 1.5f;
        public float rotationSpeed = 200f;
        public GameObject head;
        public Transform groundCheckPoint;
        public LayerMask groundLayer;
        public QueryTriggerInteraction obstacleLayer;

        [Header("Animation")]
        public Animator animator;

        private CharacterController _controller;
        private Vector3 _velocity;
        private bool _isGrounded;
        private float _verticalRotation = 0f;
        private float _horizontalRotation = 0f;
        public float minYRotation = -50f;
        public float maxYRotation = 50f;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            
            if (!canMove || UI.instance.pauseScreen.activeInHierarchy)
                return;
            
            if (!UI.instance.pauseScreen.activeInHierarchy)
            {
                _isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.3f, groundLayer, obstacleLayer);
                if (_isGrounded && _velocity.y < 0)
                    _velocity.y = -2f;

                float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
                float moveX = Input.GetAxis("Horizontal");
                float moveZ = Input.GetAxis("Vertical");
                Vector3 move = (transform.right * moveX + transform.forward * moveZ) * speed;
                _controller.Move(move * Time.deltaTime);

                if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
                    _velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);

                _velocity.y += Physics.gravity.y * gravityForce * Time.deltaTime;
                _controller.Move(_velocity * Time.deltaTime);

                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
                _horizontalRotation += mouseX;
                _verticalRotation -= mouseY;
                _verticalRotation = Mathf.Clamp(_verticalRotation, minYRotation, maxYRotation);
                transform.localRotation = Quaternion.Euler(0, _horizontalRotation, 0);
                head.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);

                animator.SetFloat("movespeed", move.magnitude);
                animator.SetBool("onGround", _isGrounded);
            }
        }

        private void OnDrawGizmos()
        {
            if (groundCheckPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheckPoint.position, 0.3f);
            }
        }
    }
}
