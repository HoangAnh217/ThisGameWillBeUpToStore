using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
  //  [SerializeField] private float dame = 50;
    private ProjectileDespawner projectileDespawner;

    [SerializeField] private int currentColorIndex;
    /* [SerializeField] private List<Sprite> sps;
     [SerializeField] private SpriteRenderer spr;*/
   // [SerializeField] private CharacterStatsSO statsSO;
    private void Start()
    {
        projectileDespawner = GetComponent<ProjectileDespawner>();
    }
    void Update()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void MoveToTarget(Marble target)
    {
        if (target == null) return;
        Vector3 targetPos = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPos);
        float duration = distance / speed;
        transform.DOMove(target.transform.position, duration)
                 .SetEase(Ease.Linear)
                 .OnComplete(() =>
                 {
                     target.Die();
                     projectileDespawner.DeSpawnObj();
                 });
    }
  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Marble"))
        {
            Marble marble = collision.GetComponent<Marble>();
            if (marble != null && marble.GetCurrentColorIndex() == currentColorIndex)
            {
                marble.Die();
                // GetComponent<Proj>
                projectileDespawner.DeSpawnObj();
            }
        }
       
    }*/
    public void SetColor(int index)
    {
        currentColorIndex = index;
    }
}
