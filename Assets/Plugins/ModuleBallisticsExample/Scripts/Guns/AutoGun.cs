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

        private ShootData shootData = default;

        private void Awake()
        {
            shootData = new ShootData();
        }

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
                shootData.position = transform.position;
                shootData.rotation = transform.rotation;

                caster.Cast(shootData, projectileData);
                lastShootTime = Time.time;
            }
        }
    }
}
