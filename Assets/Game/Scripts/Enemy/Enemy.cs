using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour, IDamageable
{
    private Color currentColor;
    public Color Color => currentColor;

    public float health = 100f;

    // components
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.Find("Model").GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        Movement();
    }
    public void SetColor(Color color)
    {
        currentColor = color;
        foreach (var spr in GetComponentsInChildren<SpriteRenderer>())
        {
            spr.color = color;
        }
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
    public int GetColorIndexByColor()
    {
        switch (currentColor)
        {
            case var c when c == Color.red:
                return 0;
            case var c when c == Color.blue:
                return 1;
            case var c when c == Color.green:
                return 2;
            case var c when c == Color.yellow:
                return 3;
            default:
                return -1; // Không khớp màu nào
        }
    }
    void Die()
    {
        GetComponent<EnemyDespawn>().DeSpawnObj();
    }
}
