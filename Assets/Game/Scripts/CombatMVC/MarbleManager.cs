// MarbleManager.cs
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class MarbleManager : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints; // gán các điểm tạo đường đi
    [SerializeField] private float spacing = 1.5f;
    [SerializeField] private float moveDuration = 10f;
    [SerializeField] private MarbleSpawnData marbleSpawnData;
    private MarbleSpawner marbleSpawner;

    //private List<GameObject> marbles = new List<GameObject>();

    void Start()
    {
        marbleSpawner = MarbleSpawner.Instance;
        StartCoroutine(SpawnMarblesCoroutine());
    }

    IEnumerator SpawnMarblesCoroutine()
    {
        Vector3[] path = GetPath();
        for (int i = 0; i < marbleSpawnData.spawnList.Count; i++)
        {
            var batch = marbleSpawnData.spawnList[i];

            for (int j = 0; j < batch.quantity; j++)
            {
                Transform marble = marbleSpawner.Spawn(
                    batch.color,
                    "Marble",
                    pathPoints[0].position,
                    Quaternion.identity
                );

                marble.transform.position = pathPoints[0].position;

                marble.transform.DOPath(path, moveDuration, PathType.CatmullRom)
                    .SetEase(Ease.Linear);

                yield return new WaitForSeconds(spacing); // delay thực sự giữa các viên
            }
        }
    }

    private Vector3[] GetPath()
    {
        Vector3[] path = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            path[i] = pathPoints[i].position;
        }
        return path;
    }

    private float TotalPathLength()
    {
        float length = 0f;
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            length += Vector3.Distance(pathPoints[i].position, pathPoints[i + 1].position);
        }
        return length;
    }
}
