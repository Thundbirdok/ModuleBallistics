using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SimpleProjectileData", menuName = "Projectiles/SimpleProjectile")]
public class SimpleProjectileData : AbstractProjectileData
{
    [SerializeField]
    private float speed;

    /// <summary>
    /// Speed
    /// </summary>
    public float Speed { get => speed; }
}
