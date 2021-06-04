using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Data of simple projectile
    /// </summary>
    [CreateAssetMenu(fileName = "SimpleProjectileData", menuName = "Projectiles/SimpleProjectile")]
    public class SimpleProjectileData : AbstractProjectileData
    {
        [SerializeField]
        private float speed = default;

        /// <summary>
        /// Speed
        /// </summary>
        public float Speed { get => speed; }
    }
}
