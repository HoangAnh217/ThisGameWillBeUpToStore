using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : Spawner
{   
    public static ProjectileSpawner Instance { get; private set; }
    public static string RedBullet = "Red";
    public static string BlueBullet = "Blue";
    public static string GreenBullet = "Green";
    public static string YellowBullet = "Yellow";
    public List<string> listColor = new List<string>() { RedBullet, BlueBullet, GreenBullet, YellowBullet
};
protected override void Awake()
    {
        Instance = this;
    }
}
