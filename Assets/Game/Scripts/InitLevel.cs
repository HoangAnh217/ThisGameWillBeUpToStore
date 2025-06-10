using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Splines;

public class InitLevel : MonoBehaviour
{   
    public static InitLevel Instance { get; private set; }

    public static MarbleSpawnData spawnDatas;
    public static string mapName;
    public static SplineContainer currentSpline;
    private GameObject prefabsEnvironment; 
    private void Awake()
    {
        Instance = this;

        int currentLevel = SaveData.LoadLevel();
        //
        string path = $"GameLevelEdit/Level{currentLevel}";
        spawnDatas = Resources.Load<MarbleSpawnData>(path);
        if (spawnDatas ==null)
        {
            Debug.Log("SpawnDatas is null");    
        }
        //
        mapName = $"map{currentLevel}";
        //
        string splinePath = $"Splines/Level{currentLevel}";
        GameObject splinePrefab = Resources.Load<GameObject>(splinePath);
        if (splinePrefab == null)
        {
            Debug.LogError($"Không tìm thấy spline prefab tại {splinePath}");
            return;
        }
        GameObject splineInstance = Instantiate(splinePrefab);
        currentSpline = splineInstance.GetComponent<SplineContainer>();
    }
}