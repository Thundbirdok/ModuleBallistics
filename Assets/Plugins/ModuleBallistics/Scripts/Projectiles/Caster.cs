using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Cast projectile
    /// </summary>
    public class Caster : MonoBehaviour
    {
        private const string DYNAMIC_PATH = "/" + ENVIRONMENT_NAME;
        private const string PROJECTILE_POOL_PATH = "/" + ENVIRONMENT_NAME + "/" + DYNAMIC_NAME;

        private const string ENVIRONMENT_NAME = "Environment";
        private const string DYNAMIC_NAME = "Dynamic";
        private const string PROJECTILE_POOL_NAME = "ProjectilePool";

        [Tooltip("Better setup in editor, not in runtime")]
        [SerializeField] private ProjectilePool projectilePool = null;

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
        public void Cast(ShootData shootData, AbstractProjectileData projectileData)
        {
            AbstractProjectile projectile = projectilePool.GetProjectile(projectileData);

            if (projectile == false)
            {
                Debug.LogError("projectile == null");

                return;
            }

            projectile.Init(shootData, projectileData);
        }

        /// <summary>
        /// Check if pool seted and if not setup it
        /// </summary>
        /// <returns>Is already linked?</returns>
        public bool CheckProjectilePool()
        {
            if (projectilePool)
            {
                return true;
            }

            GameObject environment = FindOrInstantiate(ENVIRONMENT_NAME);

            GameObject dynamic = FindOrInstantiate(DYNAMIC_NAME, DYNAMIC_PATH, environment.transform);

            projectilePool = FindOrInstantiate(PROJECTILE_POOL_NAME, PROJECTILE_POOL_PATH, dynamic.transform)
                .AddComponent<ProjectilePool>();

            return false;
        }

        private GameObject FindOrInstantiate(string name, string path = null, Transform target = null)
        {
            GameObject hierarchyObject = GameObject.Find($"{path ?? ""}/{name}");

            if (hierarchyObject)
            {
                return hierarchyObject;
            }

            if (target == false)
            {
                return new GameObject(name);
            }

            GameObject child = new GameObject(name);
            child.transform.parent = target;

            return child;
        }
    }
}
