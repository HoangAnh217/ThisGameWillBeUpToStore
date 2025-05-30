using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Transform modelTank;
    [SerializeField] private Transform turret;               // Nòng súng (turret) xoay
    [SerializeField] private Transform firePoint;            // Vị trí bắn ra đạn
    [SerializeField] private float fireRate;            // Thời gian giữa mỗi lần bắn
    [SerializeField] private float detectionRange = 10f;     // Phạm vi phát hiện enemy
    private int amountBullet = 8;
    public int AmountBullet => amountBullet;
    private int colorIndex;  // Màu của trụ (để so với enemy)
    private float fireCooldown = 0f;
    [SerializeField] private LayerMask enemyLayer; // Layer của enemy

    //
    [Header("Tank Info")]
    [SerializeField] private List<Sprite> sps;
    [SerializeField] private SpriteRenderer spr;
    //private TankDespawner tankDespawner;
    private Tank tank;
    private CanvasInGameController canvasInGameController;
    [SerializeField] private CharacterStatsSO statsSO;
    private PointShootingController pointShooting;

    private ProjectileSpawner projectileSpawner;
    private void Start()
    {
        colorIndex = GetComponent<Tank>().GetColorIndex();
      //  tankDespawner = GetComponent<TankDespawner>();
        projectileSpawner = ProjectileSpawner.Instance;
        canvasInGameController = CanvasInGameController.Instance;
        pointShooting = PointShootingController.Instance;
        tank = GetComponent<Tank>();

        model.gameObject.SetActive(false);
        modelTank.gameObject.SetActive(true);
        //fireRate = statsSO.attackSpeed;
        InitStat();
        SetColor(colorIndex);
        amountBullet = 14;

    }
    private void InitStat()
    {
        fireRate = 0.5f;
    }
    /*private void OnEnable()
    {
    }*/
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Marble nearestEnemy = FindNearestEnemyWithSameColor();
        if (nearestEnemy != null)
        {
            RotateTurretTowards(nearestEnemy.transform.position);

            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = fireRate;
            }
        }
    }
    Marble FindNearestEnemyWithSameColor()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);
        Marble nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            Marble marble = hit.GetComponent<Marble>();
            if (marble != null && marble.GetCurrentColorIndex() == colorIndex)
            {
                float dist = Vector3.Distance(transform.position, marble.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = marble;
                }
            }
        }

        return nearest;
    }

    void RotateTurretTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        turret.rotation = Quaternion.Euler(0, 0, angle );
    }

    void Shoot()
    {
        projectileSpawner.Spawn(projectileSpawner.listColor[colorIndex], firePoint.position, turret.rotation );
        // Đạn tự xử lý bay và va chạm
        amountBullet--;
        canvasInGameController.amountBulletShowUI.UpdateTmp(pointShooting.IndexTank(tank),amountBullet);
        if (amountBullet <= 0)
        {

            Debug.Log("out of bullet");

            canvasInGameController.amountBulletShowUI.OutOfBullet(pointShooting.IndexTank(tank));

            tank.DeSpawn();
        }
    }
    private void SetColor(int index)
    {

        Debug.Log("set color");

        spr.sprite = sps[index];

    }
    public void Upgrade()
    {
        Debug.Log("Upgrade tank");
    }
}
