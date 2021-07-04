using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Write log in console and deactivate object
    /// </summary>
    public class SimpleOnHitBehavior : MonoBehaviour
    {
        [SerializeField]
        private AbstractProjectile projectile = default;

        private void OnTriggerEnter(Collider collider)
        {
            OnEnvironmentHit(collider);
        }

        /// <summary>
        /// Specify which object was hit
        /// </summary>
        public void OnHit(Collider collider)
        {
            OnEnvironmentHit(collider);
        }

        /// <summary>
        /// Write log in console and deactivate object
        /// </summary>
        private void OnEnvironmentHit(Collider collider)
        {
            Debug.Log(gameObject.name + " hit environment " + collider.gameObject.name + " and been destroyed");

            projectile.IsActive = false;
        }
    }
}
