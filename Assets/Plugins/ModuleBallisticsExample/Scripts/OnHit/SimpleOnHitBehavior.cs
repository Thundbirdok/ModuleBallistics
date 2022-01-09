using UnityEngine;

namespace ModuleBallistics
{
	/// <summary>
	/// Write log in console and deactivate object
	/// </summary>
	public class SimpleOnHitBehavior : AbstractOnHitBehaviour
	{
		protected override void OnTriggerEnter(Collider collider)
		{
			OnEnvironmentHit(collider);

			base.OnTriggerEnter(collider);
		}

		/// <summary>
		/// Specify which object was hit
		/// </summary>
		public void OnHit(Collider collider)
		{
			OnEnvironmentHit(collider);

			OnHit();
		}

		/// <summary>
		/// Write log in console and deactivate object
		/// </summary>
		private void OnEnvironmentHit(Collider collider)
		{
			Debug.Log(gameObject.name + " hit environment " + collider.gameObject.name + " and been destroyed");
		}
	}
}
