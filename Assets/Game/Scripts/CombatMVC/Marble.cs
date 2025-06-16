using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public enum MarbleColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    Aqua,

}

public class Marble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> marbleSprites;
    [SerializeField] private MarbleColor currentColorType;
    [SerializeField] private bool beTarget;
    public bool IsTarget
    {
        get => beTarget;
    }
    public void SetTarget(bool value)
    {
        beTarget = value;
        Debug.Log("SetTarget: " + value);
    }
    public int GetCurrentColorIndex()
    {
        switch (currentColorType)
        {
            case MarbleColor.Red: return 0;
            case MarbleColor.Blue: return 1;
            case MarbleColor.Green: return 2;
            case MarbleColor.Yellow: return 3;
            case MarbleColor.Purple: return 4;
            case MarbleColor.Aqua: return 5 ;
            default: return -1;
        }
    }

    private MarbleDespawn marbleDespawn;

    //
    private SplineContainer splineContainer;
    public static float speed = 1.4f;

    private float t = 0f;
    private float targetT;
    public float TargetT => targetT;
    [SerializeField] private bool isBeingPulled = false;
    public bool IsBeingPulled => isBeingPulled; 
    float3 pos, tangent, up;
    public float GetT() => t;

    public void SetT(float newT)
    {
        t = Mathf.Clamp01(newT);
    }

    private void OnEnable()
    {
        t = 0f; // Reset t when disabled
        isBeingPulled = false;
        beTarget = true;
    }
    private void Start()
    {
        marbleDespawn = GetComponent<MarbleDespawn>();
        splineContainer = InitLevel.currentSpline;
        beTarget = true;
    }
    public void SetColor(MarbleColor colorType)
    {
        currentColorType = colorType;

        int index = (int)colorType;

        if (index >= 0 && index < marbleSprites.Count)
        {
            spriteRenderer.sprite = marbleSprites[index];
        }
        else
        {
            Debug.LogWarning("Sprite index out of range for MarbleColor: " + colorType);
        }
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
        if (t == 1)
        {
            Debug.Log("Lose");
            //UI_Manager.Instance.ShowPanelLose();
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
   
    public void Die()
    {
        MarbleManager.Instance.RemoveMarble(this);
        marbleDespawn.DeSpawnObj();
    }
}
