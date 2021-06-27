using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModuleBallistics
{
    public class ProjectilePool : MonoBehaviour
    {
        [Tooltip("Do pre instantiation")]
        [SerializeField] private bool isInitPreferedPoolSize = true;

        [Tooltip("Projectile datas for pre instantiation")]
        [SerializeField] private List<ProjectilePreInstantiateData> projectileDatas = default;

        [SerializeField, HideInInspector]
        private IdProjectilePoolDictionary dictionary = default;

        private List<Coroutine> coroutines = new List<Coroutine>();

        private void OnEnable()
        {
            dictionary?.CheckDictionary();
        }

        private void OnDisable()
        {
            if (coroutines is null)
            {
                return;
            }

            foreach (Coroutine coroutine in coroutines)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }

            coroutines.Clear();
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

            AbstractProjectile foundedProjectile = dictionary[data.Id].List.Where(p => p && p.IsActive == false).FirstOrDefault();
            if (foundedProjectile)
            {
                return foundedProjectile;
            }

            if (isInitPreferedPoolSize)
            {
                coroutines.Add(StartCoroutine(AddProjectilesCoroutine(data, data.PreferedPoolSize - 1)));
            }

            return CreateProjectile(data);
        }

        private bool CheckData(AbstractProjectileData data)
        {
            if (dictionary is null)
            {
                dictionary = new IdProjectilePoolDictionary();
            }

            if (dictionary.ContainsKey(data.Id) == false)
            {
                GameObject pool = new GameObject(data.Id + " Pool");
                pool.transform.parent = transform;

                dictionary.Add(data.Id, new SpecificProjectilePool(pool.transform, new List<AbstractProjectile>()));

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
        /// Add projectiles
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public IEnumerator AddProjectilesCoroutine(AbstractProjectileData data, uint amount = 0)
        {
            uint size = amount == 0 ? data.PreferedPoolSize : amount;

            CheckData(data);

            for (int i = 0; i < size; ++i)
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

            foreach (ProjectilePreInstantiateData data in projectileDatas)
            {
                InitPool(data.Data, data.Data.PreferedPoolSize * data.AmountOfGuns);
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

            foreach (ProjectilePreInstantiateData data in projectileDatas)
            {
                coroutines.Add(StartCoroutine(InitPoolCoroutine(data.Data, data.Data.PreferedPoolSize * data.AmountOfGuns)));

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

            RemoveInactiveProjectiles(dictionary[data.Id]);
        }

        /// <summary>
        /// Delete inactive projectiles from all pools until they reached minimal size
        /// </summary>
        /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
        public void Shrink(uint customMinimalSize)
        {
            foreach (var pool in dictionary)
            {
                RemoveInactiveProjectiles(pool.Value);
            }
        }

        private void RemoveInactiveProjectiles(SpecificProjectilePool projectilePool)
        {
            List<AbstractProjectile> shrinkProjectiles = projectilePool.List.Where(p => p == false || p.IsActive == false).ToList();

            foreach (var projectile in shrinkProjectiles)
            {
                projectilePool.List.Remove(projectile);
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
                if (pool.Value.Pool == false)
                {
                    continue;
                }

                if (Application.isEditor)
                {
                    DestroyImmediate(pool.Value.Pool.gameObject);

                    continue;
                }

                Destroy(pool.Value.Pool.gameObject);
            }

            dictionary.Clear();
        }

        [Serializable]
        private class ProjectilePreInstantiateData
        {
            public AbstractProjectileData Data = default;
            public uint AmountOfGuns = 1;
        }
    }
}
