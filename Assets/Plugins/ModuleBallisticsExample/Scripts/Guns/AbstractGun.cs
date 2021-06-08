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
        public virtual void StartFire() { }

        /// <summary>
        /// Stop fire projectile
        /// </summary>
        public virtual void StopFire() { }
    }
}
