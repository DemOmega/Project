using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Scenes.Script
{
    public class PadControll : MonoBehaviour
    {
        public float moveSpeed = 5f, gravityForce = 3f, jumpForce = 7f, sprintMultiplier = 8f;
        public float rotationSpeed = 100f;
        public GameObject head;
        public GameObject bulletPrefab; 
        public Transform firePoint;

        private CharacterController _controller;
        private float _verticalRotation = 0f;
        private float _horizontalRotation = 0f;
        public float minYRotation = -50f;
        public float maxYRotation = 50f;
        private Vector3 _velocity;
        private bool _isGrounded;
        public Transform groundCheckPoint;
        public LayerMask groundLayer;
        
        
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool _isJumping;
        private bool _isSprinting;
        private bool _isShooting;

        private void Start()
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _isGrounded = Physics.CheckSphere(groundCheckPoint.position, 0.3f, groundLayer);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            float speed = _isSprinting ? sprintMultiplier : moveSpeed;

            Vector3 move = (transform.right * _moveInput.x + transform.forward * _moveInput.y) * speed;
            if (_moveInput.magnitude < 0.1f)
            {
                move = Vector3.zero;
            }
            _controller.Move(move * Time.deltaTime);

            if (_isGrounded && _isJumping)
            {
                _velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
                _isJumping = false;
            }

            _velocity.y += Physics.gravity.y * gravityForce * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);

            // Rotation avec le stick droit
            _horizontalRotation += _lookInput.x * rotationSpeed * Time.deltaTime;
            _verticalRotation -= _lookInput.y * rotationSpeed * Time.deltaTime;
            _verticalRotation = Mathf.Clamp(_verticalRotation, minYRotation, maxYRotation);

            transform.localRotation = Quaternion.Euler(0, _horizontalRotation, 0);
            head.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);

            if (_isShooting)
            {
                Fire();
            }
        }

        private void Fire()
        {

            if (bulletPrefab != null && firePoint != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }

        // Fonctions appelées par Player Input
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isJumping = true;
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            _isSprinting = context.performed;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            _isShooting = context.performed;
        }
    }
}