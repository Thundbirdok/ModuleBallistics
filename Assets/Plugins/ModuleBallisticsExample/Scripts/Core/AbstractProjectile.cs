using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    private bool isActive;

    /// <summary>
    /// Is projectile moving
    /// </summary>
    public bool IsActive 
    {
        get
        {
            return isActive;
        }

        set
        {
            isActive = value;
            gameObject.SetActive(value);            
        }
    }

    protected void FixedUpdate()
    {
        if (IsActive)
        {
            Move();
        }
    }

    /// <summary>
    /// Init projectile
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="direction">Direction</param>
    /// <param name="data">Data</param>
    public abstract void Init(Vector3 position, Quaternion direction, AbstractProjectileData data);

    /// <summary>
    /// Move projectile while it active
    /// Called in <see cref="FixedUpdate"/>
    /// </summary>
    public abstract void Move();
}
