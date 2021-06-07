using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Abstract gun
    /// </summary>
    public abstract class AbstractGun : MonoBehaviour
    {
        /// <summary>
        /// Start fire projectile
        /// </summary>
        public abstract void StartFire();

        /// <summary>
        /// Stop fire projectile
        /// </summary>
        public abstract void StopFire();
    }
}
