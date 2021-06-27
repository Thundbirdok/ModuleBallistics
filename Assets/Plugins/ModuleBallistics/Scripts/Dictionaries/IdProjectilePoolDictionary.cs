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
                if (pool.Value.Pool == false)
                {
                    Clear();

#if UNITY_EDITOR

                    Debug.Log("Projectile pool dictionary has missing object");

#endif

                    return;
                }

                foreach (AbstractProjectile projectile in pool.Value.List)
                {
                    if (projectile == false)
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
