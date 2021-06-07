using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Simple gun
    /// </summary>
    public class SimpleGun : AbstractGun
    {
        [SerializeField]
        private Caster caster;

        [SerializeField]
        private AbstractProjectileData projectileData;

        public override void StartFire()
        {           
            caster.Cast(transform.position, transform.rotation, projectileData);
        }

        public override void StopFire() { }
    }
}
