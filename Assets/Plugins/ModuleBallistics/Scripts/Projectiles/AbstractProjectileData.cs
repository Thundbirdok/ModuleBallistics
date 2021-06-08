using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Base projectile data
    /// </summary>
    public abstract class AbstractProjectileData : ScriptableObject
    {
        [SerializeField]
        private string id = "ID";

        /// <summary>
        /// Projectile ID
        /// </summary>
        public string Id { get => id; }

        [SerializeField]
        private GameObject prefab = default;

        /// <summary>
        /// Projectile Prefab
        /// </summary>
        public GameObject Prefab { get => prefab; }
    }
}
