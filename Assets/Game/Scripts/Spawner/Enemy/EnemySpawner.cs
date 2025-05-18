using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public static EnemySpawner Instance;

    public static string Slime = "Slime";

    public LevelData levelData;
    private int currentWaveIndex = 0;
    private float timer = 0f;
    // private bool isSpawningWave = false;

    private int SumOfEnemyInLevel;
    // spawn infor
    [SerializeField] private float xLimit = 10f;
    [SerializeField] private float yMaxLimit = 5f;
    [SerializeField] private float yMinLimit = 5f;

    int _groupsRemaining;

    protected override void Awake()
    {
        base.Awake(); // Gọi Awake từ class cha nếu cần
        Instance = this;
    }
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < levelData.Waves.Count; i++)
        {
            for (int j = 0; j < levelData.Waves[i].EnemiesToSpawn.Count; j ++)
            {
                SumOfEnemyInLevel += levelData.Waves[i].EnemiesToSpawn[j].AmountOfEnemy ;
            }
        }
    }
    void Update()
    {
       
        timer += Time.deltaTime;
        if (currentWaveIndex >= levelData.Waves.Count)
        {
            return;
        }

        if (timer > levelData.Waves[currentWaveIndex].StartTime)
        {
            StartCoroutine(SpawnWave(levelData.Waves[currentWaveIndex], currentWaveIndex));
            currentWaveIndex++;
        }
        /*   if (spawnCoroutine != null)
           {
               StopCoroutine(spawnCoroutine);
               spawnCoroutine = null;
           }

           _mileStoneProgressBar.UpdateCompleteMileStone(currentWaveIndex);
           spawnCoroutine = StartCoroutine(SpawnWave(wave, currentWaveIndex));*/
    }

    IEnumerator SpawnWave(WaveData wave, int waveIndex)
    {
        _groupsRemaining = wave.EnemiesToSpawn.Count;

        foreach (var spawnData in wave.EnemiesToSpawn)
        {
            StartCoroutine(SpawnEnemyGroup(spawnData));
        }

        yield return new WaitUntil(() => _groupsRemaining == 0);

       /* if (waveIndex == levelData.Waves.Count - 1)
        {
            *//*_isSpawnAllEnemies = true;
            yield return new WaitUntil(() => _groupsRemaining == 0);
            Debug.Log("completed level.");*//*
            StartCoroutine(CheckComplete());
        }*/
    }
   /* private IEnumerator CheckComplete()
    {
        //WhitePoint\
        yield return new WaitUntil(() => SumOfEnemyInLevel == 0);
        Debug.Log("completed level.");
    }*/
    private IEnumerator SpawnEnemyGroup(EnemySpawnData spawnData)
    {
        for (int i = 0; i < spawnData.AmountOfEnemy; i++)
        {
            SpawnEnemy(spawnData.NameEnemyPrefabs);
            yield return new WaitForSeconds(spawnData.SpawnInterval);
        }

        // Khi group này xong, giảm counter
        _groupsRemaining--;
    }
    private void SpawnEnemy(string enemyName)
    {
        float xPos = Random.Range(-xLimit, xLimit);
        float yPos = Random.Range(yMinLimit, yMaxLimit);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);
        Transform enemy = Spawn(enemyName, spawnPos, Quaternion.identity);
        int randomColor = Random.Range(0, 4);
        enemy.GetComponent<Enemy>().SetColor(randomColor);
    }
    public override void Despawm(Transform obj)
    {   
        SumOfEnemyInLevel--;
        if (SumOfEnemyInLevel <=0)
        {
            Debug.Log("level compelete");
        }
        base.Despawm(obj);
    }
}
