using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDespawn : DeSpawnByTime
{
    [SerializeField] private bool isDespawnParent = false;
    protected override void Start()
    {
        base.Start();
        timeDespawn = 1.75f;
    }
    public override void DeSpawnObj()
    {
        base.DeSpawnObj();
        if (isDespawnParent == false)
        {
            EffectSpawner.Instance.Despawm(transform);
        }
        else 
            EffectSpawner.Instance.Despawm(transform.parent);
    }
}
