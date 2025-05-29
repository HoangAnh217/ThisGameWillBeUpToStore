using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HandlerMovementTank : MonoBehaviour
{       
    public static HandlerMovementTank Instance { get; private set; }    
    private EffectSpawner effectSpawner;
    private MergeSystem mergeSystem;   
    private CanvasInGameController canvasInGameController;
    private PointShootingController pointShootingController;

    [SerializeField] private float limitX, limitMaxY,limitMinY;
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();

    [Header("stage 1")]
    [SerializeField] private float speedStage1;
    private Transform currentTarget;

    private bool isBusy;
    public bool GetIsBusy => isBusy;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isBusy = false;
        effectSpawner = EffectSpawner.Instance;
        mergeSystem = MergeSystem.Instance;
        canvasInGameController = CanvasInGameController.Instance;
        pointShootingController = PointShootingController.Instance;
    }
    public void ControlMovement(Transform tank, Transform target)
    {
        isBusy = true;
        StartCoroutine(MoveForwardContinuously(tank, speedStage1,target));
    }
    private IEnumerator MoveForwardContinuously(Transform tank, float speed,Transform target)
    {
        Vector3 pos ;
        List<Vector2> paths = new List<Vector2>();
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        currentTarget = target;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        effectSpawner.Spawn(EffectSpawner.Smoke, tank.position - tank.right, Quaternion.identity);
        while (true)
        {

            pos = tank.position;
            Debug.Log("Pos: " + pos.y);
            tank.Translate(dir* speed * Time.deltaTime, Space.Self);
            yield return null; // Chờ 1 frame
            if (Mathf.Abs(pos.x) > limitX)
            {
                if (pos.x < 0)
                {
                    paths.Add(wayPoints[0].position);
                    break; // Dừng coroutine
                } 
                else
                {
                    paths.Add(wayPoints[1].position);
                    break; // Dừng coroutine

                }
            }
            else if (pos.y < limitMinY)
            {
                if (pos.x>=0)
                {
                    paths.Add(wayPoints[2].position);
                    paths.Add(wayPoints[1].position);
                }
                else
                {
                    paths.Add(wayPoints[3].position);
                    paths.Add(wayPoints[0].position);
                }
                break;
            }
            else if (pos.y >= limitMaxY)
            {
                break;
            }
        }
        paths.Add(target.position- new Vector3(0,1,0));
        paths.Add(target.position);
        StartTankMove(tank, paths,speedStage1);
    }
    public void StartTankMove(Transform tank, List<Vector2> paths, float speed)
    {
        if (paths == null || paths.Count == 0 || speed <= 0f) return;
        StartCoroutine(TankMoveRoutine(tank, paths, speed));
    }

    private IEnumerator TankMoveRoutine(Transform tank, List<Vector2> paths, float speed)
    {
        foreach (Vector2 target in paths)
        {
            Vector3 targetPos = new Vector3(target.x, target.y, tank.position.z);

            // Xoay hướng tank
            Vector2 dir = (target - (Vector2)tank.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            tank.rotation = Quaternion.Euler(0, 0, angle);

            float distance = Vector2.Distance(tank.position, target);
            float duration = distance / speed;
            float elapsed = 0f;

            // Bắt coroutine spawn smoke song song
            Coroutine smokeCoroutine = StartCoroutine(SpawnSmokeWhileMoving(tank));

            while (elapsed < duration)
            {
                float step = speed * Time.deltaTime;
                tank.position = Vector3.MoveTowards(tank.position, targetPos, step);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Đảm bảo đến đúng vị trí cuối
            tank.position = targetPos;
            // Ngừng spawn smoke
            StopCoroutine(smokeCoroutine);
        }
        MoveComplete(tank);
    }

    private void MoveComplete(Transform tank)
    {
        pointShootingController.MergeTwoTank();
        //canvasInGameController.amountBulletShowUI.ActiveTmp(pointShootingController.IndexTank(tank.GetComponent<Tank>()));
        isBusy = false;
        tank.GetComponent<TankController>().enabled = true;
        Debug.Log("Tank đã đến vị trí cuối!");
        currentTarget = null;
    }

    private IEnumerator SpawnSmokeWhileMoving(Transform tank)
    {
        while (true)
        {
            effectSpawner.Spawn(EffectSpawner.Smoke, tank.position, Quaternion.identity);
            yield return new WaitForSeconds(0.04f);
        }
    }
    

}
