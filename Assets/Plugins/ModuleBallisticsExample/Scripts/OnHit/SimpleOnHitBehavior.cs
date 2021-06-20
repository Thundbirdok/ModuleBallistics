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
            if (collider.gameObject.TryGetComponent(out AbstractTeamMark colliderTeam))
            {
                if (projectile.OwnerTeam == null || colliderTeam.GetType() != projectile.OwnerTeam.GetType())
                {
                    OnEnemyHit(collider);

                    return;
                }

                OnAllyHit(collider);

                return;
            }

            OnEnvironmentHit(collider);
        }

        /// <summary>
        /// Specify which object was hit
        /// </summary>
        public void OnHit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out AbstractTeamMark colliderTeam))
            {
                if (projectile.OwnerTeam == null || colliderTeam.GetType() != projectile.OwnerTeam.GetType())
                {
                    OnEnemyHit(collider);

                    return;
                }

                OnAllyHit(collider);

                return;
            }

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

        /// <summary>
        /// Write log in console and deactivate object
        /// </summary>
        private void OnEnemyHit(Collider collider)
        {
            Debug.Log(gameObject.name + " hit enemy " + collider.gameObject.name + " and deal damage");

            projectile.IsActive = false;
        }

        /// <summary>
        /// Write log in console and deactivate object
        /// </summary>
        private void OnAllyHit(Collider collider)
        {
            //Note: You can add some parameter for friendly fire behaviour

            Debug.Log(gameObject.name + " hit ally " + collider.gameObject.name + " do nothing and fly further");
        }
    }
}
