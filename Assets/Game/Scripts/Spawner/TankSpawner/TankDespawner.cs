using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDespawner : DeSpawnByDistance
{
    private TankSpawner tankSpawner;

    protected override void Start()
    {
        base.Start();
        tankSpawner = TankSpawner.Instance;
        distanceLimit = 20f;
    }
    public override void DeSpawnObj()
    {
        base.DeSpawnObj();
        tankSpawner.Despawm(transform);    
    }
}
