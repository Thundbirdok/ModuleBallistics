using UnityEngine;

/// <summary>
/// Write log in console and deactivate object
/// </summary>
public class SimpleOnHitBehavior : MonoBehaviour
{
    [SerializeField]
    private AbstractProjectile projectile;

    /// <summary>
    /// Write log in console and deactivate object
    /// </summary>
    public void OnHit()
    {
        Debug.Log(gameObject.name + " Hit!");

        projectile.IsActive = false;
    }

    /// <summary>
    /// Write log in console and deactivate object
    /// </summary>
    public void OnHit(Collision collision)
    {
        Debug.Log(gameObject.name + " Hit " + collision.gameObject.name);

        projectile.IsActive = false;
    }

    /// <summary>
    /// Write log in console and deactivate object
    /// </summary>
    public void OnHit(Collider collider)
    {
        Debug.Log(gameObject.name + " Hit " + collider.gameObject.name);

        projectile.IsActive = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
    }
}
