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

    private void OnCollisionEnter(Collision collision)
    {
        OnHit();
    }
}
