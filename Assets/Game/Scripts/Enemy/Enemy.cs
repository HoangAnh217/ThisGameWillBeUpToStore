using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Color currentColor;
    public Color Color => currentColor;

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
}
