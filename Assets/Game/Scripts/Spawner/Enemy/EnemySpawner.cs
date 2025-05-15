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
    private Color[] colors =
               {
                    Color.red,
                    Color.blue,
                    Color.green,
                    Color.yellow,
                };
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
        SpawnEnemy(Slime, spawnPos, Quaternion.identity, colors[randomColor]);
    }
    public void SpawnEnemy(string enemyType, Vector3 spawnPos, Quaternion rotation,Color color)
    {
        Transform enemy = base.Spawn(enemyType, spawnPos, rotation);
        enemy.GetComponent<Enemy>().SetColor(color);
    }
}
