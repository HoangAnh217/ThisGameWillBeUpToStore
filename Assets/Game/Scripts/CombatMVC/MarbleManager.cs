using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Splines;
using Unity.Mathematics;
using Unity.VisualScripting;

public class MarbleManager : MonoBehaviour
{   
    public static MarbleManager Instance { get; private set; }
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private List<MarbleMover2D> marbles = new List<MarbleMover2D>();
    [SerializeField] private float marbleSpacing; // khoảng cách t giữa 2 viên
    [SerializeField] private MarbleSpawnData marbleSpawnData;
    private Vector2 pointSpawn;
    float t;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        float3 localPos, tangent, up;
        splineContainer.Spline.Evaluate(0f, out localPos, out tangent, out up);

        // nếu bạn muốn vị trí world (không phải local của container)
        pointSpawn = splineContainer.transform.TransformPoint((Vector3)localPos);

        StartCoroutine(SpawnMarblesCoroutine());
        t = 2 * 0.5f/ splineContainer.Spline.GetLength();
    }

    private void Update()
    {
       // UpdateMarblePositions();
    }

    private IEnumerator SpawnMarblesCoroutine()
    {
        for (int i = 0; i < marbleSpawnData.spawnList.Count; i++)
        {
            var batch = marbleSpawnData.spawnList[i];
            for (int j = 0; j < batch.quantity; j++)
            {
                Transform marbleTrans = MarbleSpawner.Instance.Spawn(
                    batch.color,
                    "Marble",
                    pointSpawn,
                    Quaternion.identity
                );

                MarbleMover2D mover = marbleTrans.GetComponent<MarbleMover2D>();
                if (mover != null)
                {
                    // Thiết lập splineContainer nếu cần
                    //mover.splineContainer = splineContainer;
                    // Khởi đặt vị trí t ngay chính xác sau viên trước
                    /*float tStart = 0f;
                    if (marbles.Count > 0)
                    {
                        tStart = Mathf.Clamp01(marbles[marbles.Count - 1].GetT() - marbleSpacing);
                    }
                    mover.SetT(tStart);*/

                    marbles.Add(mover);
                }
                else
                {
                    Debug.LogWarning("Spawned marble missing MarbleMover2D component!");
                }

                // Đợi 0.1s trước khi spawn viên kế tiếp (có thể điều chỉnh)
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void UpdateMarblePositions(int index,float distancT)
    {
        for (int i = index-1; i >= 0; i--)
        {
            MarbleMover2D frontMarble = marbles[i];

            frontMarble.PullTo(marbles[marbles.Count-1].GetT() + distancT*(marbles.Count-i-2));
            //frontMarble.PullTo(marbles[i+1].GetT());
        }
    }
    public void RemoveMarble(MarbleMover2D marble)
    {
        if (marbles.Contains(marble))
        {   
            int id = marbles.IndexOf(marble);
            UpdateMarblePositions(id, marbles[0].GetT() - marbles[1].GetT());
            marbles.Remove(marble);
        }
    }
}
