using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDespawn : DeSpawnByTime
{
    protected override void Start()
    {
        base.Start();
        timeDespawn = 1.75f;
    }
    public override void DeSpawnObj()
    {
        base.DeSpawnObj();
        EffectSpawner.Instance.Despawm(transform);
    }
}
