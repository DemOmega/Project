using System;
using UnityEditor.Search;
using UnityEngine;

namespace Scenes.Script
{
    public class GunController : MonoBehaviour
    {
        public GameObject projectiletPrefab;
        public float power;
        
        
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject projectile = Instantiate(projectiletPrefab, transform.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                
                rb.AddForce(transform.forward * power, ForceMode.Impulse);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * 1000);
        }
    }
}