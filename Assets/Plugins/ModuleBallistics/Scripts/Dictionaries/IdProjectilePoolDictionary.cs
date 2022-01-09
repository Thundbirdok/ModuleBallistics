using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ModuleBallistics
{
	[Serializable]
	public class IdProjectilePoolDictionary : UnitySerializedDictionary<string, ProjectilePool>
	{
		/// <summary>
		/// Check if no missings
		/// If found - Clear all
		/// </summary>
		public void CheckDictionary()
		{
			foreach (KeyValuePair<string, ProjectilePool> pool in this)
			{
				if (pool.Value.ProjectilesParent != false)
				{
					continue;
				}

				ClearDictionary("Projectile pool dictionary has missing object");

				break;
			}
		}

		/// <summary>
		/// Clear dictionary
		/// </summary>
		/// <param name="message"></param>
		public void ClearDictionary(string message = null)
		{
			foreach (ProjectilePool pool in Values)
			{
				if (pool.Pool != null)
				{
					pool.Pool.Dispose();
				}
			}

			Clear();

#if UNITY_EDITOR

			Debug.Log(message ?? "Dictionary of pools cleared");

#endif
		}
	}

	[Serializable]
	public class ProjectilePool
	{
		[SerializeField]
		private Transform projectilesParent = default;
		public Transform ProjectilesParent => projectilesParent;

		[SerializeField]
		private ObjectPool<AbstractProjectile> pool = default;
		public ObjectPool<AbstractProjectile> Pool => pool;

		public ProjectilePool(
			Transform projectilesParent,
			ObjectPool<AbstractProjectile> pool)
		{
			this.projectilesParent = projectilesParent;
			this.pool = pool;
		}
	}
}
