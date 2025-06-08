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
    public static string PurpleBullet = "Purple";
    public static string AquaBullet = "Aqua";
    public List<string> listColor = new List<string>() { RedBullet, BlueBullet, GreenBullet, YellowBullet,PurpleBullet, AquaBullet };
    protected override void Awake()
    {
     
        Instance = this;
    }
   // public void Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation,dloa)
}
