using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Gun with auto fire
    /// </summary>
    public class AutoGun : AbstractGun
    {
        [SerializeField]
        private Caster caster;

        [SerializeField]
        private AbstractProjectileData projectileData;

        [SerializeField]
        private float cooldown = 0.5f;

        private bool isFireing;
        private float lastShootTime;

        public override void StartFire()
        {
            caster.Cast(transform.position, transform.rotation, projectileData);
            isFireing = true;
        }

        public override void StopFire()
        {
            isFireing = false;            
        }

        private void FixedUpdate()
        {
            if (isFireing == false)
            {
                return;
            }

            if (Time.time >= lastShootTime + cooldown)
            {
                caster.Cast(transform.position, transform.rotation, projectileData);
                lastShootTime = Time.time;
            }
        }
    }
}
