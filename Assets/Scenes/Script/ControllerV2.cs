using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Scenes.Script
{
    public class ControllerV2 : MonoBehaviour
    {
        public float moveSpeed, gravityFocre, jumpForce;
        public CharacterController characterController;

        private Vector3 moveInput;

        private bool canJump;
        public Transform groundCheck;
        public LayerMask ground;
        

        private void Start()
        {
        
        }

        private void Update()
        {
            float yVelocity = moveInput.y;
            Vector3 verticalMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal");
        
            moveInput = horizontalMove + verticalMove;
            moveInput.Normalize();
            moveInput = moveInput * moveSpeed;

            moveInput.y = yVelocity;
            
            moveInput.y +=Physics.gravity.y * gravityFocre * Time.deltaTime;

            if (characterController.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityFocre * Time.deltaTime;
            }
            
            characterController.Move(moveInput*Time.deltaTime);
            
            canJump=Physics.OverlapSphere(groundCheck.position, 0.2f, ground).Length>0;

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveInput.y = jumpForce;
            }
        }
    }
}
