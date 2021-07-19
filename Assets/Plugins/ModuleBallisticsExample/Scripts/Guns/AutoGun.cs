using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Gun with auto fire
    /// </summary>
    public class AutoGun : AbstractGun
    {
        [SerializeField]
        protected Caster caster = default;

        [SerializeField]
        protected AbstractProjectileData projectileData = default;

        [SerializeField]
        protected float cooldown = 0.5f;

        protected bool isFireing = false;
        protected float lastShootTime = 0;

        protected ShootData shootData = new ShootData();

        protected void FixedUpdate()
        {
            if (isFireing == false)
            {
                return;
            }

            if (Time.time >= lastShootTime + cooldown)
            {
                Fire();
            }
        }

        public override void StartFire()
        {
            isFireing = true;
        }

        public override void StopFire()
        {
            isFireing = false;
        }

        protected virtual void Fire()
        {
            shootData.position = transform.position;
            shootData.rotation = transform.rotation;

            caster.Cast(shootData, projectileData);
            lastShootTime = Time.time;
        }
    }
}
