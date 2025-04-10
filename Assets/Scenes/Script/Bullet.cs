using System;
using UnityEngine;

namespace Scenes.Script
{
    public class Bullet: MonoBehaviour
    {
        public float bulletSpeed, lifeTime;
        
        public Rigidbody theRigidbody;
        
        public int damage;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            theRigidbody.linearVelocity = transform.forward * bulletSpeed;
            
            lifeTime -= Time.deltaTime;
            
            if(lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnnemyHealt>().DamageEnemy(damage);
            }
            Destroy(gameObject);
        }
    }
        
}
