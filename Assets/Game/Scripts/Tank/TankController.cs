using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private Transform turret;               // Nòng súng (turret) xoay
    [SerializeField] private GameObject bulletPrefab;        // Prefab đạn
    [SerializeField] private Transform firePoint;            // Vị trí bắn ra đạn
    [SerializeField] private float fireRate = 0.3f;            // Thời gian giữa mỗi lần bắn
    [SerializeField] private float detectionRange = 10f;     // Phạm vi phát hiện enemy
    private Color tankColor = Color.red;  // Màu của trụ (để so với enemy)

    private float fireCooldown = 0f;
    [SerializeField] private LayerMask enemyLayer; // Layer của enemy

    //
    private ProjectileSpawner projectileSpawner;
    private void Start()
    {
        tankColor = GetComponent<TankController>().tankColor;
        projectileSpawner = ProjectileSpawner.Instance;
    }
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Enemy nearestEnemy = FindNearestEnemyWithSameColor();
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
    Enemy FindNearestEnemyWithSameColor()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);
        Enemy nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null && enemy.Color == tankColor)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = enemy;
                }
            }
        }

        return nearest;
    }

    void RotateTurretTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        turret.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        projectileSpawner.Spawn(ProjectileSpawner.Bullet, firePoint.position, turret.rotation);
        // Đạn tự xử lý bay và va chạm
    }
}
