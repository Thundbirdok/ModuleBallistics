using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Simple gun
    /// </summary>
    public class SimpleGun : AbstractGun
    {
        [SerializeField]
        private Caster caster = default;

        [SerializeField]
        private AbstractTeamMark team = default;

        [SerializeField]
        private AbstractProjectileData projectileData = default;

        private ShootData shootData = default;

        private void Awake()
        {
            shootData = new ShootData();
        }

        public override void StartFire()
        {
            shootData.position = transform.position;
            shootData.rotation = transform.rotation;

            caster.Cast(shootData, projectileData);
        }
    }
}
