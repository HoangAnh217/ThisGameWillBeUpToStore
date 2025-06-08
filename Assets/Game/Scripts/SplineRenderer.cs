using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class SplineRenderer : MonoBehaviour
{
    private SplineContainer splineContainer;
    [SerializeField] private int resolution = 50; // Số điểm chia nhỏ spline

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {   
        splineContainer = GetComponent<SplineContainer>();
        if (splineContainer == null)
        {
            Debug.LogWarning("SplineContainer not assigned.");
            return;
        }

        DrawSpline();
    }

    void DrawSpline()
    {
        var spline = splineContainer.Spline;
        if (spline == null || spline.Count < 2)
        {
            Debug.LogWarning("Spline is invalid or too short.");
            return;
        }

        List<Vector3> sampledPoints = new List<Vector3>();

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 position = spline.EvaluatePosition(t);
            sampledPoints.Add(position);
        }

        lineRenderer.positionCount = sampledPoints.Count;
        lineRenderer.SetPositions(sampledPoints.ToArray());
    }
}
