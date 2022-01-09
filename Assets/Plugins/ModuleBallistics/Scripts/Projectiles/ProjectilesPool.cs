using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ModuleBallistics
{
	public class ProjectilesPool : MonoBehaviour
	{
		[Tooltip("Projectile datas for pre instantiation")]
		[SerializeField] private List<ProjectilePreInstantiateData> projectileDatas = default;

		[SerializeField, HideInInspector]
		private IdProjectilePoolDictionary dictionary = default;

		private void OnEnable()
		{
			dictionary?.CheckDictionary();
		}

		private void OnDestroy()
		{
			ClearPools();
		}

		public void InitPools()
		{
			ClearPools();

			foreach (ProjectilePreInstantiateData data in projectileDatas)
			{
				ObjectPool<AbstractProjectile> pool = CreatePool(data.Data,
					data.Data.MinPoolSize * data.AmountOfGuns,
					data.Data.MaxPoolSize * data.AmountOfGuns);

				AbstractProjectile[] array = new AbstractProjectile[data.Data.MinPoolSize * data.AmountOfGuns];

				for (int i = 0; i < data.Data.MinPoolSize * data.AmountOfGuns; ++i)
				{
					array[i] = pool.Get();
				}

				for (int i = 0; i < data.Data.MinPoolSize * data.AmountOfGuns; ++i)
				{
					pool.Release(array[i]);
				}
			}
		}

		/// <summary>
		/// Return projectile with sent data
		/// </summary>
		/// <param name="data">Data</param>
		/// <returns>Projectile</returns>
		public AbstractProjectile Get(AbstractProjectileData data)
		{
			CheckData(data);

			return dictionary[data.Id].Pool.Get();
		}

		private void CheckData(AbstractProjectileData data)
		{
			if (dictionary is null)
			{
				dictionary = new IdProjectilePoolDictionary();
			}

			if (dictionary.ContainsKey(data.Id) == false)
			{
				CreatePool(data);

				return;
			}

			return;
		}

		private ObjectPool<AbstractProjectile> CreatePool(AbstractProjectileData data, int customMinSize = 0, int customMaxSize = 0)
		{
			int minSize = customMinSize <= 0 ? data.MinPoolSize : customMinSize;
			int maxSize = customMaxSize <= 0 ? data.MaxPoolSize : customMaxSize;

			Transform projectilesParent = new GameObject(data.Id + " Pool").transform;
			projectilesParent.transform.parent = transform;

			ObjectPool<AbstractProjectile> pool = new ObjectPool<AbstractProjectile>(
				createFunc: () => ProjectileCreate(data),
				actionOnGet: null,
				actionOnRelease: ProjectileOnRelease,
				actionOnDestroy: ProjectileOnDestroy,
				collectionCheck: false,
				defaultCapacity: minSize,
				maxSize: maxSize
			);

			dictionary.Add(data.Id, new ProjectilePool(projectilesParent, pool));

			return pool;
		}

		private AbstractProjectile ProjectileCreate(AbstractProjectileData data)
		{
			AbstractProjectile projectile = Instantiate(data.Prefab, dictionary[data.Id].ProjectilesParent).GetComponent<AbstractProjectile>();

			AbstractOnHitBehaviour onHit = projectile.GetComponent<AbstractOnHitBehaviour>();
			onHit.Pool = GetPool(data);

			return projectile;
		}

		private void ProjectileOnRelease(AbstractProjectile projectile)
		{
			if (projectile == false)
			{
				return;
			}

			projectile.IsActive = false;
		}

		private void ProjectileOnDestroy(AbstractProjectile projectile)
		{
			if (projectile == false)
			{
				return;
			}

			Destroy(projectile.gameObject);
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

			foreach (KeyValuePair<string, ProjectilePool> pool in dictionary)
			{
				if (pool.Value.ProjectilesParent)
				{
					DestroyGameObject(pool.Value.ProjectilesParent.gameObject);
				}

				if (pool.Value.Pool != null)
				{
					pool.Value.Pool.Dispose();
				}
			}

			dictionary.Clear();
		}

		private void DestroyGameObject(GameObject target)
		{
			if (Application.isEditor)
			{
				DestroyImmediate(target);

				return;
			}

			Destroy(target);
		}

		private ObjectPool<AbstractProjectile> GetPool(AbstractProjectileData data)
		{
			return dictionary[data.Id].Pool;
		}

		[Serializable]
		private class ProjectilePreInstantiateData
		{
			public AbstractProjectileData Data = default;
			[Range(1, 100)]
			public int AmountOfGuns = 1;
		}
	}
}
