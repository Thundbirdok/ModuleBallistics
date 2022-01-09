using ModuleBallistics;
using UnityEngine;
using UnityEngine.Pool;

public abstract class AbstractOnHitBehaviour : MonoBehaviour
{
	private ObjectPool<AbstractProjectile> pool = default;
	public ObjectPool<AbstractProjectile> Pool
	{
		get
		{
			return pool;
		}

		set
		{
			if (value != null)
			{
				pool = value;
			}
		}
	}

	[SerializeField]
	private AbstractProjectile projectile = default;
	public AbstractProjectile Projectile
	{
		get
		{
			return projectile;
		}

		set
		{
			if (value != null)
			{
				projectile = value;
			}
		}
	}

	protected virtual void OnTriggerEnter(Collider collider)
	{
		OnHit();
	}

	protected virtual void OnHit()
	{
		Pool.Release(projectile);
	}
}
