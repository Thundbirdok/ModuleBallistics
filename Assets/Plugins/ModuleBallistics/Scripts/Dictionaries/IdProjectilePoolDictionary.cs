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
                    ClearDictionary("Projectile pool dictionary has missing object");

                    return;
                }

                foreach (AbstractProjectile projectile in pool.Value.List)
                {
                    if (projectile == false)
                    {
                        ClearDictionary("Projectile pool has missing object");

                        return;
                    }
                }
            }
        }

        private void ClearDictionary(string message = null)
        {
            Clear();

#if UNITY_EDITOR

            Debug.Log(message ?? "Dictionary of pools cleared");

#endif
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
