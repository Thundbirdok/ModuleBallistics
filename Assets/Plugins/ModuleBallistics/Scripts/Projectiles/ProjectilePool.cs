using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModuleBallistics
{
    public class ProjectilePool : MonoBehaviour
    {
        [Tooltip("Do pre instantiation")]
        [SerializeField] private bool isInitPreferedPoolSize = true;

        [Tooltip("Projectile datas for pre instantiation")]
        [SerializeField] private List<AbstractProjectileData> projectileDatas = default;

        [SerializeField]
        private Dictionary<string, (Transform Pool, List<AbstractProjectile> List)> dictionary = default;

        private List<Coroutine> coroutines = default;

        private void OnDisable()
        {
            foreach (Coroutine coroutine in coroutines)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
        }

        /// <summary>
        /// Return projectile with sent data
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Projectile</returns>
        public AbstractProjectile GetProjectile(AbstractProjectileData data)
        {
            if (CheckData(data) == false)
            {
                AbstractProjectile projectile = CreateProjectile(data);

                if (isInitPreferedPoolSize)
                {
                    coroutines.Add(StartCoroutine(InitPoolCoroutine(data)));
                }

                return projectile;
            }

            foreach (AbstractProjectile projectile in dictionary[data.Id].List)
            {
                if (projectile.IsActive == false)
                {
                    return projectile;
                }
            }

            return CreateProjectile(data);
        }

        private bool CheckData(AbstractProjectileData data)
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, (Transform Pool, List<AbstractProjectile> List)>();
            }

            if (dictionary.ContainsKey(data.Id) == false)
            {
                GameObject pool = new GameObject(data.Id + " Pool");
                pool.transform.parent = transform;

                dictionary.Add(data.Id, (pool.transform, new List<AbstractProjectile>()));

                return false;
            }

            return true;
        }

        private AbstractProjectile CreateProjectile(AbstractProjectileData data)
        {
            AbstractProjectile createdProjectile
                = Instantiate(data.Prefab, dictionary[data.Id].Pool).GetComponent<AbstractProjectile>();

            createdProjectile.IsActive = false;

            dictionary[data.Id].List.Add(createdProjectile);

            return createdProjectile;
        }

        /// <summary>
        /// Instantiate pool with projectiles with sent data until reached minimal size
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
        public void InitPool(AbstractProjectileData data, uint customMinimalSize = 0)
        {
            uint size = customMinimalSize == 0 ? data.PreferedPoolSize : customMinimalSize;

            CheckData(data);

            while (dictionary[data.Id].List.Count < size)
            {
                CreateProjectile(data);
            }
        }

        /// <summary>
        /// Instantiate pool with projectiles with sent data until reached minimal size
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
        public IEnumerator InitPoolCoroutine(AbstractProjectileData data, uint customMinimalSize = 0)
        {
            uint size = customMinimalSize == 0 ? data.PreferedPoolSize : customMinimalSize;

            CheckData(data);

            while (dictionary[data.Id].List.Count < size)
            {
                CreateProjectile(data);

                yield return null;
            }
        }

        /// <summary>
        /// Instantiate pools with projectiles with datas until reached minimal size
        /// </summary>
        public void InitPools()
        {
            if (Application.isEditor)
            {
                ClearPools();
            }

            foreach (AbstractProjectileData data in projectileDatas)
            {
                InitPool(data);
            }
        }

        /// <summary>
        /// Instantiate pools with projectiles with datas until reached minimal size
        /// </summary>
        public IEnumerator InitPoolsCoroutine()
        {
            if (Application.isEditor)
            {
                ClearPools();
            }

            foreach (AbstractProjectileData data in projectileDatas)
            {
                coroutines.Add(StartCoroutine(InitPoolCoroutine(data)));

                yield return null;
            }
        }

        /// <summary>
        /// Delete inactive projectiles with sent data from pool until reached minimal size
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
        public void Shrink(AbstractProjectileData data, uint customMinimalSize = 0)
        {
            uint size = customMinimalSize == 0 ? data.PreferedPoolSize : customMinimalSize;

            CheckData(data);

            for (int i = 0; i < dictionary[data.Id].List.Count && dictionary[data.Id].List.Count > size;)
            {
                if (dictionary[data.Id].List[i].IsActive == false)
                {
                    dictionary[data.Id].List.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }

        /// <summary>
        /// Delete inactive projectiles from all pools until they reached minimal size
        /// </summary>
        /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
        public void Shrink(uint customMinimalSize)
        {
            foreach (var pool in dictionary)
            {
                for (int i = 0; i < pool.Value.List.Count && pool.Value.List.Count > customMinimalSize;)
                {
                    if (pool.Value.List[i].IsActive == false)
                    {
                        pool.Value.List.RemoveAt(i);
                    }
                    else
                    {
                        ++i;
                    }
                }
            }
        }

        /// <summary>
        /// Destroy all pools
        /// </summary>
        public void ClearPools()
        {
            if (dictionary == null)
            {
                return;
            }

            foreach (var pool in dictionary)
            {
                if (Application.isEditor)
                {
                    if (pool.Value.Pool != null)
                    {
                        DestroyImmediate(pool.Value.Pool.gameObject);
                    }

                    continue;
                }

                if (pool.Value.Pool != null)
                {
                    Destroy(pool.Value.Pool.gameObject);
                }
            }

            dictionary.Clear();
        }
    }
}
