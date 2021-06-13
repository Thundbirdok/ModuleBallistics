using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Move object in direction
    /// </summary>
    public class SimpleProjectile : AbstractProjectile
    {
        private float speed = 0;

        public override void Init(Vector3 position, Quaternion direction, AbstractProjectileData data)
        {
            SimpleProjectileData downCastedData = data as SimpleProjectileData;

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
            transform.position = transform.position + transform.forward * speed;
        }
    }
}
