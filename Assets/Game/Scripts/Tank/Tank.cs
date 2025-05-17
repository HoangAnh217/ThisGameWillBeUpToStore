using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [Header("Tank Info")]
    [SerializeField] private List<Sprite> sps;
    [SerializeField] private SpriteRenderer spr;
    private int  currentColorIndex;

    [Header("Hover Effect")]
    public float hoverScaleFactor = 1.1f; // Phóng to lên 10%
    private Vector3 originalScale;
    private bool isHovered = false;
    private MergeSystem mergeSystem;
    private void Start()
    {   
        mergeSystem = MergeSystem.Instance;
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
    public int GetColorIndex()
    {
        return currentColorIndex;   
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
        if (HandlerMovementTank.Instance.GetIsBusy)
        {
            Debug.Log("Wating....");
            return;
        }
        if (PointShootingController.Instance.IsFullSlot())
        {
            return;
        }
        int index = transform.GetSiblingIndex();
        if (!MatrixGameController.Instance.HandleCarCollision(index))
        {
            Transform target = PointShootingController.Instance.GetPoint();
            //PointShootingController.Instance.SetObj();
            transform.SetParent(transform.parent.parent.Find("OutOfHolder"));
            HandlerMovementTank.Instance.ControlMovement(transform, target);      
        }
    }
    public void SetColor(int index)
    {
       /* Color[] colors =
                {
                    Color.red,
                    Color.blue,
                    Color.green,
                    Color.yellow,
                };*/
        spr.sprite = sps[index];
        currentColorIndex = index;
    }
    public void DeSpawn()
    {   
        int indexCurrentTank = mergeSystem.IndexTank(GetComponent<Tank>());
        PointShootingController.Instance.RemoveObj(indexCurrentTank);
        MergeSystem.Instance.RemoveAt(indexCurrentTank);
        //MergeSystem.Instance.MergeTwoTank()
        GetComponent<TankDespawner>().DeSpawnObj();
    }
    public void Upgrade()
    {
        Debug.Log("Upgrade tank");
    }
}
