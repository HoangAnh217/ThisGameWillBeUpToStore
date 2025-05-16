using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawner : DeSpawnByDistance
{
    protected override void Start()
    {
        base.Start();
        distanceLimit = 20f; // Set the distance limit for despawning
    }
    public override void DeSpawnObj()
    {
        base.DeSpawnObj();
        ProjectileSpawner.Instance.Despawm(transform);
    }

}
