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
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && enemy.CurrentColorIndex == currentColorIndex)
            {
                enemy.TakeDamage(dame);
                // GetComponent<Proj>
                projectileDespawner.DeSpawnObj();
            }
        }
        /*else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroy the projectile when it hits a wall
        }*/
    }
    public void SetColor(int index)
    {

        //spr.sprite = sps[index];
        currentColorIndex = index;
    }
}
