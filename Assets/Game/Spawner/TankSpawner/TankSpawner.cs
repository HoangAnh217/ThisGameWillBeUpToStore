using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : Spawner
{
    public static TankSpawner Instance { get; private set; }

    public static string TankString = "Tank";
    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject GetHolderObj(int index)
    {
        return holder.GetChild(index).gameObject;
    }
}
