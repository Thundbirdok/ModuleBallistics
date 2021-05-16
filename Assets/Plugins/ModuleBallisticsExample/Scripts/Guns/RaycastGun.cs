using UnityEngine;

/// <summary>
/// Gun with raycast projectiles
/// </summary>
public class RaycastGun : MonoBehaviour, IGun
{
    [SerializeField]
    private Caster caster;

    [SerializeField]
    private RaycastProjectileData projectileData;

    /// <summary>
    /// Fire projectile
    /// </summary>
    public void Fire()
    {
        caster.Cast(transform.position, transform.rotation, projectileData);
    }
}
