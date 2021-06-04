using UnityEngine;

namespace ModuleBallistics
{
    /// <summary>
    /// Simple gun
    /// </summary>
    public class SimpleGun : MonoBehaviour, IGun
    {
        [SerializeField]
        private Caster caster;

        [SerializeField]
        private AbstractProjectileData projectileData;

        public void Fire()
        {
            caster.Cast(transform.position, transform.rotation, projectileData);
        }
    }
}
