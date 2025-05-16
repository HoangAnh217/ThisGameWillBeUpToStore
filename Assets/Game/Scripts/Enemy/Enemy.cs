using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour, IDamageable
{
    private int currentColorIndex;
    public int CurrentColorIndex=> currentColorIndex;
   // [SerializeField] private List<Sprite> sps;
    private SpriteRenderer spr;

    public float health = 100f;

    // components
    private void Awake()
    {
        spr = transform.Find("Model").GetComponentInChildren<SpriteRenderer>();
    }
  
    private void Update()
    {
        Movement();
    }
    public void SetColor(int index)
    {
        Color[] colors =
                {
                    Color.red,
                    Color.blue,
                    Color.green,
                    Color.yellow,
                };
      
        currentColorIndex = index;
        spr.color = colors[index];
    }
    private void Movement()
    {   
        transform.Translate(Vector3.down * Time.deltaTime * 0.8f);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GetComponent<EnemyDespawn>().DeSpawnObj();
    }
}
