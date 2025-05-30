using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class MarbleMover2D : MonoBehaviour
{
    public SplineContainer splineContainer;
    [SerializeField] private float speed = 2f;

    private float t = 0f;
    private float targetT;
    [SerializeField] private bool isBeingPulled = false;
    float3 pos, tangent, up;

    public float GetT() => t;

    public void SetT(float newT)
    {
        t = Mathf.Clamp01(newT);
    }

    public void PullTo(float newTargetT)
    {
        if (isBeingPulled)
        {
            if (targetT > newTargetT)
            {
                targetT = newTargetT;
            }
        }
        targetT = Mathf.Clamp01(newTargetT);
        isBeingPulled = true;
    }
    private void OnEnable()
    {
        t = 0f; // Reset t when disabled
        isBeingPulled = false;

    }
    private void OnDisable()
    {
        MarbleManager.Instance.RemoveMarble(this);
    }
    void Update()
    {
        if (isBeingPulled)
        {
            targetT += speed * Time.deltaTime / splineContainer.Spline.GetLength();
            t = Mathf.MoveTowards(t, targetT, 0.4f * Time.deltaTime);
            if (Mathf.Approximately(t, targetT))
            {
                t = targetT;
                isBeingPulled = false;
            }
        }
        else
        {
            t += speed * Time.deltaTime / splineContainer.Spline.GetLength();
            t = Mathf.Clamp01(t);
        }

        splineContainer.Spline.Evaluate(t, out pos, out tangent, out up);
        Vector3 position = (Vector3)pos;
        Vector3 direction = (Vector3)tangent;

        position.z = 0;
        direction.z = 0;

        transform.position = position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
