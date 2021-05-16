using Unity;
using UnityEngine;

/// <summary>
/// Base projectile data
/// </summary>
public abstract class AbstractProjectileData : ScriptableObject
{
    [SerializeField]
    private string id;

    /// <summary>
    /// Projectile ID
    /// </summary>
    public string Id { get => id; }

    [SerializeField]
    private GameObject prefab;
    
    /// <summary>
    /// Projectile Prefab
    /// </summary>
    public GameObject Prefab { get => prefab; }    
}