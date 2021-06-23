using UnityEngine;
using UnityEngine.Events;

namespace ModuleBallistics
{
    /// <summary>
    /// Projectile with raycast hit check
    /// </summary>
    public class RaycastProjectile : AbstractProjectile
    {
        [SerializeField]
        private UnityEvent<Collider> OnHit = default;

        private float speed = 0;

        public override void Init(Vector3 position, Quaternion direction, AbstractProjectileData data, AbstractTeamMark team)
        {
            RaycastProjectileData downCastedData = data as RaycastProjectileData;

            ownerTeam = team;

            InitTransform(position, direction);

            speed = downCastedData.Speed;

            IsActive = true;
        }

        private void FixedUpdate()
        {
            if (IsActive)
            {
                Move();
            }
        }

        protected void Move()
        {
            Vector3 previousPosition = transform.position;

            transform.position = transform.position + transform.forward * speed;

            if (RaycastHelper.CheckHitBetweenPoints(previousPosition, transform.position, out RaycastHit hit))
            {
                //NOTE: hit happened
                OnHit?.Invoke(hit.collider);
            }
        }
    }
}
