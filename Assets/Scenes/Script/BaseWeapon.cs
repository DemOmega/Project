using UnityEngine;

namespace Scenes.Script
{
    public class BaseWeapon : MonoBehaviour
    {
        public int lowAmmoThreshold = 5;
        public bool canShoot=true;
        public virtual int GetCurrentAmmo() => 0;
        public virtual int GetMaxAmmo() => 0;
    }
}