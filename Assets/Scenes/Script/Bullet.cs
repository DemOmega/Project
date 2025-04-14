using System;
using UnityEngine;

namespace Scenes.Script
{
    public class Bullet : MonoBehaviour
    {
        public float bulletSpeed, lifeTime;

        public Rigidbody theRigidbody;

        public int damage;

        public bool damageEnemy, damagePlayer;

        void Update()
        {
           theRigidbody.linearVelocity = transform.forward * bulletSpeed;

            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && damageEnemy)
            {
                other.gameObject.GetComponent<EnnemyHealt>()?.DamageEnemy(damage);
            }

            if (other.gameObject.CompareTag("Player") && damagePlayer)
            { 
                PlayerHealt.instance?.DamagePlayer(damage);
            }

            Destroy(gameObject);
        }
    }
}