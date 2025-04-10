using UnityEngine;

namespace Scenes.Script
{
    public class Controller : MonoBehaviour
    {
        public static Controller instance;

        [Header("Déplacement")]
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
        public QueryTriggerInteraction obstacleLayer;

        [Header("Animation")]
        public Animator animator;

        [Header("Tir")]
        public GameObject bullet;
        public Transform firePoint;
        public float fireRate = 0.1f;
        private float fireCooldown;
        public int maxAmmo = 30;
        private int currentAmmo;
        private bool isReloading = false;
        public float reloadTime = 2f;

        [Header("Spray")]
        public float sprayAmount = 2f;
        public float sprayIncreasePerShot = 0.5f;
        public float sprayRecovery = 1f;
        public float maxSpray = 5f;
        private float currentSpray = 0f;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip shootSound;
        public AudioClip reloadSound;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _controller = GetComponent<CharacterController>();
            currentAmmo = maxAmmo;
        }

        private void Update()
        {
            // --- Sol ---
            _isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.3f, groundLayer, obstacleLayer);
            if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;

            // --- Sprint & déplacement ---
            float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            Vector3 move = (transform.right * moveX + transform.forward * moveZ) * speed;
            _controller.Move(move * Time.deltaTime);

            // --- Saut ---
            if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
                _velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);

            _velocity.y += Physics.gravity.y * gravityforce * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);

            // --- Rotation souris ---
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            _horizontalRotation += mouseX;
            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, minYRotation, maxYRotation);
            transform.localRotation = Quaternion.Euler(0, _horizontalRotation, 0);
            head.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);

            // --- Animation ---
            animator.SetFloat("movespeed", move.magnitude);
            animator.SetBool("onGround", _isGrounded);

            // --- Tir & Rechargement ---
            fireCooldown -= Time.deltaTime;
            currentSpray = Mathf.MoveTowards(currentSpray, 0f, sprayRecovery * Time.deltaTime);

            if (!isReloading)
            {
                if (Input.GetButton("Fire1") && fireCooldown <= 0f && currentAmmo > 0)
                    Shoot();

                if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
                    StartCoroutine(Reload());
            }
        }

        void Shoot()
        {
            fireCooldown = fireRate;
            currentAmmo--;

            // Calcul direction visée
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Vector3 targetPoint = ray.GetPoint(100f);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f)) targetPoint = hit.point;

            Vector3 direction = (targetPoint - firePoint.position).normalized;

            // Ajoute le spray
            direction = Quaternion.Euler(
                Random.Range(-currentSpray, currentSpray),
                Random.Range(-currentSpray, currentSpray),
                0f
            ) * direction;

            // Instancie la balle
            Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));

            // Son de tir
            if (shootSound && audioSource)
                audioSource.PlayOneShot(shootSound);

            currentSpray = Mathf.Clamp(currentSpray + sprayIncreasePerShot, 0f, maxSpray);

            Debug.Log($"Ammo: {currentAmmo}");
        }

        System.Collections.IEnumerator Reload()
        {
            isReloading = true;

            if (reloadSound && audioSource)
                audioSource.PlayOneShot(reloadSound);

            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTime);

            currentAmmo = maxAmmo;
            isReloading = false;

            Debug.Log("Reloaded.");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, 0.3f);
        }
    }
}
