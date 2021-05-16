using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cast projectile
/// </summary>
public class Caster : MonoBehaviour
{    
    private const string ENVIRONMENT_NAME = "/Environment";
    private const string DYNAMIC_NAME = "/Environment/Dynamic";
    private const string PROJECTILE_POOL_NAME = "/Environment/Dynamic/ProjectilePool";
    
    [Tooltip("Better setup in editor, not in runtime")]
    [SerializeField] 
    private ProjectilePool projectilePool;

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
    public void CheckProjectilePool()
    {
        if (projectilePool != null)
        {
            return;
        }

        GameObject environment = GameObject.Find(ENVIRONMENT_NAME);
        GameObject dynamic;

        if (environment == null)
        {
            environment = Instantiate(new GameObject());
            dynamic = Instantiate(new GameObject(), environment.transform);
            projectilePool = Instantiate(new GameObject(), dynamic.transform).AddComponent<ProjectilePool>();

            return;
        }

        dynamic = GameObject.Find(DYNAMIC_NAME);

        if (dynamic == null)
        {
            dynamic = Instantiate(new GameObject(), environment.transform);
            projectilePool = Instantiate(new GameObject(), dynamic.transform).AddComponent<ProjectilePool>();

            return;
        }

        projectilePool = GameObject.Find(PROJECTILE_POOL_NAME).AddComponent<ProjectilePool>();

        if (projectilePool == null)
        {
            projectilePool = Instantiate(new GameObject(), dynamic.transform).AddComponent<ProjectilePool>();
        }
    }
}
