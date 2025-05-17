using UnityEngine;

public class EnemySpawner : Spawner
{
    public static EnemySpawner Instance;

    public static string Slime = "Slime";

    public float xLimit = 10f; // Giới hạn spawn theo trục x
    public float yMaxLimit = 5f;  // Giới hạn spawn theo trục y
    public float yMinLimit = 5f;  // Giới hạn spawn theo trục y
    public float spawnInterval = 0.2f;
    private float time = 0f;
    // controller spawn
    protected override void Awake()
    {
        base.Awake(); // Gọi Awake từ class cha nếu cần
        Instance = this;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnInterval)
        {
            time = 0f;
            SpawnEnemySimple(Slime);
        }
    }
    public void SpawnEnemySimple(string enemyType)
    {
        /*GameObject enemyPrefab = Resources.Load<GameObject>(enemyType);
        if (enemyPrefab == null)
        {
            Debug.LogError($"Enemy prefab '{enemyType}' not found in Resources folder.");
            return;
        }*/

        float xPos = Random.Range(-xLimit, xLimit);
        float yPos = Random.Range(yMinLimit, yMaxLimit);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);

        int randomColor = Random.Range(0,4);
        SpawnEnemy(Slime, spawnPos, Quaternion.identity, randomColor);
    }
    public void SpawnEnemy(string enemyType, Vector3 spawnPos, Quaternion rotation,int colorIndex)
    {
        Transform enemy = base.Spawn(enemyType, spawnPos, rotation);
        enemy.GetComponent<Enemy>().SetColor(colorIndex);
        /*if (colorIndex == 0)
            enemy.gameObject.layer = LayerMask.NameToLayer("Red");
        else if (colorIndex == 1)
            enemy.gameObject.layer = LayerMask.NameToLayer("Blue");
        else if (colorIndex == 2)
            enemy.gameObject.layer = LayerMask.NameToLayer("Green");
        else if (colorIndex == 3)
            enemy.gameObject.layer = LayerMask.NameToLayer("Yellow");*/
    }
}
