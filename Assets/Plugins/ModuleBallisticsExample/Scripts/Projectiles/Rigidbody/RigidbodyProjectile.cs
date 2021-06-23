using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Simple rigidbody based ballistic projectile
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyProjectile : AbstractProjectile
    {
        [SerializeField]
        private Rigidbody body = default;

        private float startForce = 0;

        private Vector3 previousPosition = default;

        public override void Init(Vector3 position, Quaternion direction, AbstractProjectileData data, AbstractTeamMark team)
        {
            RigidbodyProjectileData downCastedData = data as RigidbodyProjectileData;

            ownerTeam = team;

            InitTransform(position, direction);

            startForce = downCastedData.StartForce;

            previousPosition = transform.position;

            IsActive = true;

            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.AddForce(transform.forward * startForce, ForceMode.Impulse);
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
            Vector3 direction = transform.position - previousPosition;
            transform.rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : transform.rotation;

            previousPosition = transform.position;
        }
    }
}
