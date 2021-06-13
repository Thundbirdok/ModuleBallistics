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

        [Tooltip("Prefered projectile pool size")]
        [SerializeField] private uint preferedPoolSize = 10;

        /// <summary>
        /// Prefered projectile pool size
        /// </summary>
        public uint PreferedPoolSize => preferedPoolSize;
    }
}
