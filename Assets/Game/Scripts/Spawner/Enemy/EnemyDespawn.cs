using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : DeSpawnByDistance
{
    protected override void Start()
    {
        base.Start();
        distanceLimit = 50f;
    }
    public override void DeSpawnObj()
    {
        base.DeSpawnObj();
        EnemySpawner.Instance.Despawm(transform);
    }
    protected override bool CanDespawn()
    {

        Debug.Log(distance + "  " + distanceLimit);
        return base.CanDespawn();
    }
}
