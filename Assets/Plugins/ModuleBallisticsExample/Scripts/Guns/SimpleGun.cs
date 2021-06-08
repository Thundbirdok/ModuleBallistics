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
        private AbstractProjectileData projectileData = default;

        public override void StartFire()
        {
            caster.Cast(transform.position, transform.rotation, projectileData);
        }
    }
}
