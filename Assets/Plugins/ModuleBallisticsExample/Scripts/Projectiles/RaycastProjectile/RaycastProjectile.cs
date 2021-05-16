using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Projectile with raycast hit check
/// </summary>
public class RaycastProjectile : AbstractProjectile
{
    [SerializeField]
    private UnityEvent OnHit;

    private float speed;

    public override void Init(Vector3 position, Quaternion direction, AbstractProjectileData data)
    {        
        RaycastProjectileData downCastedData = data as RaycastProjectileData;

        base.Init(position, direction, data);

        speed = downCastedData.Speed;

        IsActive = true;
    }

    private void FixedUpdate()
    {
        if (IsActive)
        {
            Move();
        }
    }

    public override void Move()
    {
        Vector3 previousPosition = transform.position;

        transform.position = transform.position + transform.forward * speed;

        if (RaycastHelper.CheckHitBetweenPoints(previousPosition, transform.position, out RaycastHit hit))
        {
            //hit happened
            OnHit?.Invoke();
        }
    }
}
