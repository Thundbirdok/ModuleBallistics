using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    [SerializeField]
    private Caster caster;

    [SerializeField]
    private SimpleProjectileData projectileData;

    public void Fire()
    {
        caster.Cast(transform.position, transform.rotation, projectileData);
    }
}
