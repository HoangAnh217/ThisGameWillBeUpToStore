using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HandlerMovementTank : MonoBehaviour
{       
    public static HandlerMovementTank Instance { get; private set; }    
    private PointShootingController pointShootingController;

    [SerializeField] private float limitX, limitMaxY,limitMinY;
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();

    [Header("stage 1")]
    [SerializeField] private float speedStage1;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        pointShootingController = PointShootingController.Instance;
    }
    public void ControlMovement(Transform tank, Transform target)
    {
        StartCoroutine(MoveForwardContinuously(tank, speedStage1,target));
    }
    private IEnumerator MoveForwardContinuously(Transform tank, float speed,Transform target)
    {
        Vector3 pos ;
        List<Vector2> paths = new List<Vector2>();
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        while (true)
        {
            pos = tank.position;
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
                    break;
                }
            }
            else if (pos.y >= limitMaxY)
            {
                break;
            }
        }
        paths.Add(target.position- new Vector3(0,1,0));
        paths.Add(target.position);
        TankMoveStage2(tank, paths,speedStage1);
    }
    private void TankMoveStage2(Transform tank, List<Vector2> paths, float speed)
    {
        if (paths == null || paths.Count == 0 || speed <= 0f) return;

        Sequence seq = DOTween.Sequence();
        Vector3 currentPos = tank.position;

        foreach (Vector2 point in paths)
        {
            Vector2 target = point;
            Vector3 from = currentPos;
            // 1. Tính toán góc tại thời điểm sẽ move
            seq.AppendCallback(() =>
            {
                Vector2 dir = (target - (Vector2)tank.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                tank.rotation = Quaternion.Euler(0, 0, angle  );
            });

            float distance = Vector2.Distance(currentPos, target);
            float duration = distance / speed;

            seq.Append(tank.DOMove(target, duration).SetEase(Ease.Linear));

            currentPos = target;
        }

        seq.OnComplete(() =>
        {
            Debug.Log("Tank đã đến vị trí cuối!");
        });
    }



}
