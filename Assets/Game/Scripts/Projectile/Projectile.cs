using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dame = 50;
    private ProjectileDespawner projectileDespawner;

    [SerializeField] private int currentColorIndex;
    /* [SerializeField] private List<Sprite> sps;
     [SerializeField] private SpriteRenderer spr;*/
    [SerializeField] private CharacterStatsSO statsSO;
    private void Start()
    {
        projectileDespawner = GetComponent<ProjectileDespawner>();
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
       
    }
    public void SetColor(int index)
    {
        currentColorIndex = index;
    }
}
