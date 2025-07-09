using DG.Tweening;
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

    private PointShootingController pointShootingController;
    private void Start()
    {
        pointShootingController = PointShootingController.Instance;
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
        int indexCar;
        Vector2 posCollision;
        if (!MatrixGameController.Instance.HandleCarCollision(index, out posCollision, out indexCar))
        {
            Transform target = PointShootingController.Instance.GetPoint();
            //PointShootingController.Instance.SetObj();
            pointShootingController.AddList(this);

            transform.SetParent(transform.parent.parent.Find("OutOfHolder"));
            HandlerMovementTank.Instance.ControlMovement(transform, target);
        }
        else
        {
            Vector3 originalPos = transform.position;
            float distance = Vector2.Distance(transform.position, posCollision); 
            Vector3 targetPos = transform.position + transform.right * distance * 0.5f;

            transform.DOMove(targetPos, 0.2f)
                .OnComplete(() =>
                {
                    Transform otherCar = transform.parent.GetChild(indexCar);
                    otherCar.DOShakePosition(0.1f, strength: 0.1f, vibrato: 10, randomness: 90);

                    transform.DOShakePosition(0.1f, strength: 0.1f, vibrato: 10, randomness: 90)
                        .OnComplete(() =>
                        {
                            transform.DOMove(originalPos, 0.2f);
                        });
                });

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
        int indexCurrentTank = PointShootingController.Instance.IndexTank(GetComponent<Tank>());
        pointShootingController.RemoveAt(indexCurrentTank);

        Debug.Log("asdasd");

        //MergeSystem.Instance.MergeTwoTank()
        GetComponent<TankDespawner>().DeSpawnObj();
    }
    
}
