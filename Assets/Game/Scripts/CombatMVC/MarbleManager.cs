using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Splines;
using Unity.Mathematics;
using Unity.VisualScripting;

public class MarbleManager : MonoBehaviour
{   
    public static MarbleManager Instance { get; private set; }
    private SplineContainer splineContainer;
    [SerializeField] private List<Marble> marbles = new List<Marble>();
    [SerializeField] private float marbleSpacing; // khoảng cách t giữa 2 viên
    private MarbleSpawnData marbleSpawnData;
    private Vector2 pointSpawn;
    float t;
    private int amountOfMarbles;
    private float distanceT;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        splineContainer = InitLevel.currentSpline;
        Marble.speed = 2.1f;
        marbleSpacing = 0.3f; // khoảng cách giữa các viên bi
        distanceT = Marble.speed * marbleSpacing / splineContainer.Spline.GetLength();
        float3 localPos, tangent, up;
        splineContainer.Spline.Evaluate(0f, out localPos, out tangent, out up);

        marbleSpawnData = InitLevel.spawnDatas;
        // nếu bạn muốn vị trí world (không phải local của container)
        pointSpawn = splineContainer.transform.TransformPoint((Vector3)localPos);

        amountOfMarbles = marbleSpawnData.GetTotalMarbles();
        StartCoroutine(SpawnMarblesCoroutine());
        t = 2 * 0.5f/ splineContainer.Spline.GetLength();
    }
    float timer = 0f;
    private void Update()
    {
       // UpdateMarblePositions();
       timer += Time.deltaTime;
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

                Marble mover = marbleTrans.GetComponent<Marble>();
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
                if (timer >2.5f)
                {
                    Marble.speed = 0.7f;
                    marbleSpacing = 0.9f;
                }
                yield return new WaitForSeconds(marbleSpacing);
            }
        }
    }

    private void UpdateMarblePositions(int index,float distancT)
    {   
        float t = marbles[index].GetT();
        for (int i = index-1; i >= 0; i--)
        {
            Marble frontMarble = marbles[i];

           // frontMarble.PullTo(marbles[marbles.Count-1].GetT() + distancT*(marbles.Count-i-2));
            frontMarble.PullTo(t + distancT*(index-1-i));
            //frontMarble.PullTo(marbles[i+1].GetT());
        }
    }
    public void RemoveMarble(Marble marble)
    {
        amountOfMarbles--;
        if (amountOfMarbles<=0)
        {
            UI_Manager.Instance.ShowPanelWin();
            return;
        }
        if (marbles.Contains(marble))
        {   
            int id = marbles.IndexOf(marble);
            UpdateMarblePositions(id, distanceT);
            marbles.Remove(marble);
        }
    }
}
