using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : Spawner
{   
    public static ProjectileSpawner Instance { get; private set; }
    public static string Bullet = "Bullet";
    protected override void Awake()
    {
        Instance = this;
    }
}
