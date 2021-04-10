using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move object in direction
/// </summary>
public class SimpleProjectile : AbstractProjectile
{
    private float speed;
    
    public override void Init(Vector3 position, Quaternion direction, AbstractProjectileData data)
    {
        SimpleProjectileData downCastedData = data as SimpleProjectileData;

        transform.position = position;
        transform.rotation = direction;

        speed = downCastedData.Speed;

        IsActive = true;
    }
    
    public override void Move()
    {
        transform.position = transform.position + transform.forward * speed;
    }
}
