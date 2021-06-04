using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Data of projectile with simple ballistic and raycast hit check
    /// </summary>
    [CreateAssetMenu(fileName = "RigidbodyProjectileData", menuName = "Projectiles/RigidbodyProjectile")]
    public class RigidbodyProjectileData : AbstractProjectileData
    {
        [SerializeField]
        protected float startForce = default;

        /// <summary>
        /// Speed
        /// </summary>
        public float StartForce { get => startForce; }
    }
}
