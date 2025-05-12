using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Tank Info")]
    [SerializeField] private List<SpriteRenderer> sprs;

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
        MatrixGameController.Instance.HandleCarCollision(index);
    }
    public void SetColor(Color a)
    {
        foreach (var spr in sprs)
        {
            spr.color = a;
        }
    }
}
