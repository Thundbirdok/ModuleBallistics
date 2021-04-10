using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField]
    uint minimalSize = 10;

    [SerializeField]
    private Dictionary<string, (Transform Pool, List<AbstractProjectile> List)> dictionary;

    /// <summary>
    /// Return projectile with sent data
    /// </summary>
    /// <param name="data">Data</param>
    /// <returns>Projectile</returns>
    public AbstractProjectile GetProjectile(AbstractProjectileData data)
    {
        if (dictionary == null)
        {
            dictionary = new Dictionary<string, (Transform Pool, List<AbstractProjectile> List)>();
        }

        if (CheckData(data) == false)
        {
            return CreateProjectile(data);
        }

        foreach (AbstractProjectile projectile in dictionary[data.Id].List)
        {
            if (projectile.IsActive == false)
            {
                return projectile;
            }
        }

        return CreateProjectile(data);
    }

    private bool CheckData(AbstractProjectileData data)
    {
        if (dictionary.ContainsKey(data.Id) == false)
        {
            Transform pool = Instantiate(new GameObject(data.Id + " Pool"), transform).transform;

            dictionary.Add(data.Id, (pool, new List<AbstractProjectile>()));

            return false;
        }

        return true;
    }

    private AbstractProjectile CreateProjectile(AbstractProjectileData data)
    {
        AbstractProjectile createdProjectile
            = Instantiate(data.Prefab, dictionary[data.Id].Pool).GetComponent<AbstractProjectile>();

        dictionary[data.Id].List.Add(createdProjectile);

        return createdProjectile;
    }

    /// <summary>
    /// Instantiate pool with projectiles with sent data until reached minimal size
    /// </summary>
    /// <param name="data">Data</param>
    /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
    public void InitPool(AbstractProjectileData data, uint customMinimalSize = 0)
    {
        uint size = customMinimalSize == 0 ? minimalSize : customMinimalSize;

        CheckData(data);

        for (int i = 0; i < size && size < dictionary[data.Id].List.Count; ++i)
        {
            CreateProjectile(data);
        }
    }

    /// <summary>
    /// Delete inactive projectiles with sent data from pool until reached minimal size
    /// </summary>
    /// <param name="data">Data</param>
    /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
    public void Shrink(AbstractProjectileData data, uint customMinimalSize = 0)
    {
        uint size = customMinimalSize == 0 ? minimalSize : customMinimalSize;

        CheckData(data);

        for (int i = 0; i < dictionary[data.Id].List.Count && dictionary[data.Id].List.Count > size;)
        {
            if (dictionary[data.Id].List[i].IsActive == false)
            {
                dictionary[data.Id].List.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
    }

    /// <summary>
    /// Delete inactive projectiles from all pools until they reached minimal size
    /// </summary>
    /// <param name="customMinimalSize">Size of pool will be equal or bigger</param>
    public void Shrink(uint customMinimalSize = 0)
    {
        uint size = customMinimalSize == 0 ? minimalSize : customMinimalSize;

        foreach (var pair in dictionary)
        {
            for (int i = 0; i < pair.Value.List.Count && pair.Value.List.Count > size;)
            {
                if (pair.Value.List[i].IsActive == false)
                {
                    pair.Value.List.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }
    }
}
