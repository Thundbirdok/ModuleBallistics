using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Gun with auto fire
    /// </summary>
    public class AutoGun : AbstractGun
    {
        [SerializeField]
        private Caster caster = default;

        [SerializeField]
        private AbstractTeamMark team = default;

        [SerializeField]
        private AbstractProjectileData projectileData = default;

        [SerializeField]
        private float cooldown = 0.5f;

        private bool isFireing = false;
        private float lastShootTime = 0;

        public override void StartFire()
        {
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
                caster.Cast(transform.position, transform.rotation, projectileData, team);
                lastShootTime = Time.time;
            }
        }
    }
}
