using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModuleBallistics
{
    [Serializable]
    public class IdProjectilePoolDictionary : UnitySerializedDictionary<string, SpecificProjectilePool>
    {
        public void CheckDictionary()
        {
            foreach (KeyValuePair<string, SpecificProjectilePool> pool in this)
            {
                if (pool.Value.Pool == null || pool.Value.Pool is null || pool.Value.Pool.Equals(null))
                {
                    Clear();

#if UNITY_EDITOR

                    Debug.Log("Projectile pool dictionary has missing object");

#endif

                    return;
                }

                foreach (AbstractProjectile projectile in pool.Value.List)
                {
                    if (projectile == null || projectile is null || projectile.Equals(null))
                    {
                        Clear();

#if UNITY_EDITOR

                        Debug.Log("Projectile pool dictionary has missing object");

#endif

                        return;
                    }
                }
            }
        }
    }

    [Serializable]
    public class SpecificProjectilePool
    {
        public Transform Pool = default;
        public List<AbstractProjectile> List = default;

        public SpecificProjectilePool(
            Transform pool,
            List<AbstractProjectile> list)
        {
            Pool = pool;
            List = list;
        }
    }
}
