using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Cast projectile
    /// </summary>
    public class Caster : MonoBehaviour
    {
        private const string ENVIRONMENT_NAME = "Environment";
        private const string DYNAMIC_NAME = "Dynamic";
        private const string PROJECTILE_POOL_NAME = "ProjectilePool";

        [Tooltip("Better setup in editor, not in runtime")]
        [SerializeField] private ProjectilePool projectilePool;

        private void OnEnable()
        {
            CheckProjectilePool();
        }

        /// <summary>
        /// Cast projectile
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="direction">Direction</param>
        /// <param name="data">Data</param>
        public void Cast(Vector3 position, Quaternion direction, AbstractProjectileData data)
        {
            AbstractProjectile projectile = projectilePool.GetProjectile(data);

            if (projectile == null)
            {
                Debug.LogError("projectile == null");

                return;
            }

            projectile.Init(position, direction, data);
        }

        /// <summary>
        /// Check if pool seted and if not setup it
        /// </summary>
        /// <returns>Is already linked?</returns>
        public bool CheckProjectilePool()
        {
            if (projectilePool != null)
            {
                return true;
            }

            GameObject environment = FindOrInstantiate(ENVIRONMENT_NAME);

            GameObject dynamic = FindOrInstantiate(DYNAMIC_NAME, environment.transform);

            projectilePool = FindOrInstantiate(PROJECTILE_POOL_NAME, dynamic.transform).AddComponent<ProjectilePool>();

            return false;
        }

        private GameObject FindOrInstantiate(string name, Transform target = null)
        {
            GameObject hierarchyObject = GameObject.Find(name);

            if (hierarchyObject != null)
            {
                return hierarchyObject;
            }

            if (target == null)
            {
                return new GameObject(name);
            }

            GameObject child = new GameObject(name);
            child.transform.parent = target;

            return child;
        }
    }
}
