using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Base projectile data
    /// </summary>
    public abstract class AbstractProjectileData : ScriptableObject
    {
        [Tooltip("Projectile id")]
        [SerializeField] private string id = "ID";

        /// <summary>
        /// Projectile ID
        /// </summary>
        public string Id => id;

        [Tooltip("Projectile prefab")]
        [SerializeField] private GameObject prefab = default;

        /// <summary>
        /// Projectile prefab
        /// </summary>
        public GameObject Prefab => prefab;

        [Tooltip("Minimal pool size")]
        [SerializeField] private int minPoolSize = 10;

        /// <summary>
        /// Minimal pool size
        /// </summary>
        public int MinPoolSize => minPoolSize;

        [Tooltip("Maximal pool size")]
        [SerializeField] private int maxPoolSize = 10;

        /// <summary>
        /// Maximal pool size
        /// </summary>
        public int MaxPoolSize => maxPoolSize;
    }
}
