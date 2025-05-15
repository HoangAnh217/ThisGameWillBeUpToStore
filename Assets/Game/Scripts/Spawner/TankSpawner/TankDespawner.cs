using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDespawner : DeSpawnByDistance
{
    protected override void Start()
    {
        base.Start();
        distanceLimit = 20f;
    }
    public override void DeSpawnObj()
    {
        base.DeSpawnObj();
        TankSpawner.Instance.Despawm(transform);    
    }
}
