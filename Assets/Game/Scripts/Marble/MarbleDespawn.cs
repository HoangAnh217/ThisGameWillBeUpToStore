using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleDespawn : DeSpawnByDistance
{   
    private MarbleSpawner marbleSpawner;
    
    protected override void Start()
    {
        base.Start();
        marbleSpawner = MarbleSpawner.Instance; // Get the instance of MarbleSpawner
        distanceLimit = 10f; // Set the distance limit for despawning marbles

    }
    public override void DeSpawnObj()
    {
        base.DeSpawnObj();
        marbleSpawner.Despawm(transform);
    }
}
