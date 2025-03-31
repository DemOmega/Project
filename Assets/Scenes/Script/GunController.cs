using System;
using UnityEditor.Search;
using UnityEngine;

namespace Scenes.Script
{
    public class GunController : MonoBehaviour
    {
        public GameObject projectiletPrefab;
        public GameObject particulePrefab;
        public Transform raycastOrigin;
        public float power;
        
        
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject projectile = Instantiate(projectiletPrefab, transform.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                
                rb.AddForce(transform.forward * power, ForceMode.Impulse);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out var hitInfo))
                {
                    Instantiate(particulePrefab, hitInfo.point, Quaternion.Euler(hitInfo.transform.forward));
                    //hitInfo.transform.gameObject.GetComponent<TargetController>().Damga(1);//
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * 1000);
        }
    }
}