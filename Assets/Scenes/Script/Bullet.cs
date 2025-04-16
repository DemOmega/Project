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
            if (damageEnemy && other.CompareTag("Enemy"))
            {
                EnnemyHealt enemy = other.GetComponent<EnnemyHealt>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);
                    if (HitmarkerManager.instance != null)
                        HitmarkerManager.instance.ShowHitmarker();
                }
            }

            Destroy(gameObject);
        }
    }
}
