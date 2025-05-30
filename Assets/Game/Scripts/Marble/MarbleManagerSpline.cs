using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class MarbleManagerSpline : MonoBehaviour
{
    /*public SplineContainer spline;
    public GameObject marblePrefab;
    public float spacing = 0.02f;
    public float speed = 0.1f;

    private List<MarbleMover2D> marbles = new List<MarbleMover2D>();

    void Update()
    {
        if (marbles.Count == 0) return;

        // Cập nhật bi đầu tiên
        marbles[0].targetT += speed * Time.deltaTime;

        // Các bi sau bám theo bi trước
        for (int i = 1; i < marbles.Count; i++)
        {
            float prevT = marbles[i - 1].targetT;
            float t = FindTAtSpacing(prevT, spacing);
            marbles[i].targetT = t;
        }
    }

    float FindTAtSpacing(float fromT, float backDistance)
    {
        float t = fromT;
        float step = 0.001f;
        float accDist = 0f;

        float3 lastPos, nextPos, tangent, up;
        spline.Spline.Evaluate(t, out lastPos, out tangent, out up);

        while (t > 0)
        {
            t -= step;
            spline.Spline.Evaluate(t, out nextPos, out tangent, out up);
            accDist += Vector3.Distance(lastPos, nextPos);
            if (accDist >= backDistance)
                break;
            lastPos = nextPos;
        }

        return Mathf.Clamp01(t);
    }

    public void SpawnMarble()
    {
        GameObject go = Instantiate(marblePrefab);
        MarbleMover2D mover = go.GetComponent<MarbleMover2D>();
        mover.splineContainer = spline;
        mover.currentT = 0;
        mover.targetT = marbles.Count == 0 ? 0f : marbles[marbles.Count - 1].targetT;
        marbles.Add(mover);
    }

    public void RemoveMarble(MarbleMover2D marble)
    {
        if (!marbles.Contains(marble)) return;

        float deletedT = marble.targetT;

        // Xóa
        marbles.Remove(marble);
        Destroy(marble.gameObject);

        // Tụt lùi các viên ở phía trước
        foreach (var m in marbles)
        {
            if (m.targetT < deletedT)
            {
                m.targetT += spacing;
                m.targetT = Mathf.Clamp01(m.targetT);
            }
        }
    }*/

}
