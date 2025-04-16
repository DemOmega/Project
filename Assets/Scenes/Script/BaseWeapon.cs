using UnityEngine;

namespace Scenes.Script
{
    public class BaseWeapon : MonoBehaviour
    {
        public bool canShoot=true;
        public virtual int GetCurrentAmmo() => 0;
        public virtual int GetMaxAmmo() => 0;
    }
}