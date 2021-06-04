using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Move object in direction
    /// </summary>
    public class SimpleProjectile : AbstractProjectile
    {
        private float speed;

        public override void Init(Vector3 position, Quaternion direction, AbstractProjectileData data)
        {
            SimpleProjectileData downCastedData = data as SimpleProjectileData;

            base.Init(position, direction, data);

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

        protected override void Move()
        {
            transform.position = transform.position + transform.forward * speed;
        }
    }
}
