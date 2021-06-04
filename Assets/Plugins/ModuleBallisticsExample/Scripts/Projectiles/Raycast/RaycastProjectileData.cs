using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Data of projectile with raycast hit check
    /// </summary>
    [CreateAssetMenu(fileName = "RaycastProjectileData", menuName = "Projectiles/RaycastProjectile")]
    public class RaycastProjectileData : AbstractProjectileData
    {
        [SerializeField]
        protected float speed = default;

        /// <summary>
        /// Speed
        /// </summary>
        public float Speed { get => speed; }
    }
}
