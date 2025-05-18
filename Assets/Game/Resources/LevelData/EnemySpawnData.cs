using UnityEngine;


[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Game Data/Enemy Spawn Data")]
public class EnemySpawnData : ScriptableObject
{
    public string NameEnemyPrefabs;
    public int AmountOfEnemy;
    public float SpawnInterval;
}
