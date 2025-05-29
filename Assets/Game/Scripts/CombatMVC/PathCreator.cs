// PathCreator.cs
using UnityEngine;
using System.Collections.Generic;

public class PathCreator : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    public Vector3 GetPositionAlongPath(float distance)
    {
        if (waypoints.Count < 2)
            return Vector3.zero;

        float totalLength = GetPathLength();
        float remaining = distance;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Vector3 start = waypoints[i].position;
            Vector3 end = waypoints[i + 1].position;
            float segmentLength = Vector3.Distance(start, end);

            if (remaining <= segmentLength)
                return Vector3.Lerp(start, end, remaining / segmentLength);

            remaining -= segmentLength;
        }

        return waypoints[waypoints.Count - 1].position;
    }

    public float GetPathLength()
    {
        float length = 0f;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            length += Vector3.Distance(waypoints[i].position, waypoints[i + 1].position);
        }
        return length;
    }
    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count < 2)
            return;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);
            }
        }
        Gizmos.DrawSphere(waypoints[waypoints.Count - 1].position, 0.2f);
    }

}
