using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Move object in direction
    /// </summary>
    public class SimpleProjectile : AbstractProjectile
    {
        private float speed = 0;

        public override void Init(ShootData shootData, AbstractProjectileData projectileData)
        {
            SimpleProjectileData downCastedProjectileData = projectileData as SimpleProjectileData;

            InitTransform(shootData);

            speed = downCastedProjectileData.Speed;

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
            transform.position = transform.position + transform.forward * speed;
        }
    }
}
