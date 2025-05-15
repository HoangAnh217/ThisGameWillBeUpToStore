using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Tank Info")]
    [SerializeField] private List<SpriteRenderer> sprs;
    private Color currentColor;

    [Header("Hover Effect")]
    public float hoverScaleFactor = 1.1f; // Phóng to lên 10%
    private Vector3 originalScale;
    private bool isHovered = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isHovered)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * hoverScaleFactor, Time.deltaTime * 10f);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 10f);
        }
    }
    public Color GetColor()
    {
        return currentColor;
    }
    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }
    private void OnMouseDown()
    {
        // Xử lý sự kiện khi nhấn chuột vào Tank
        int index = transform.GetSiblingIndex();
        if (!MatrixGameController.Instance.HandleCarCollision(index))
        {
            Transform target = PointShootingController.Instance.GetPoint();
            if (target == null)
            {
                return;
            }
            if (HandlerMovementTank.Instance.GetIsBusy)
            {
                Debug.Log("Wating....");
                return;
            }
            PointShootingController.Instance.SetObj();
            transform.SetParent(transform.parent.parent.Find("OutOfHolder"));
            HandlerMovementTank.Instance.ControlMovement(transform, target);      
        }
    }
    public void SetColor(Color a)
    {
        foreach (var spr in sprs)
        {
            spr.color = a;
            currentColor = a;
        }
    }
    public void DeSpawn()
    {
        Debug.Log("despawn in " + (transform.GetSiblingIndex()));    
        PointShootingController.Instance.RemoveObj(transform.GetSiblingIndex());
        MergeSystem.Instance.RemoveAt(transform.GetSiblingIndex());
        GetComponent<TankDespawner>().DeSpawnObj();
    }
    public void Upgrade()
    {
        Debug.Log("Upgrade tank");
    }
}
