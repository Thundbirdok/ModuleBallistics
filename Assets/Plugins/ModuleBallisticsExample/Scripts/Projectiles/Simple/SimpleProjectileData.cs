using UnityEngine;

[CreateAssetMenu(fileName = "SimpleProjectileData", menuName = "Projectiles/SimpleProjectile")]
public class SimpleProjectileData : AbstractProjectileData
{
    [SerializeField]
    private float speed = default;

    /// <summary>
    /// Speed
    /// </summary>
    public float Speed { get => speed; }
}
