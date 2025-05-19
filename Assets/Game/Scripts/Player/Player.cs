using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float maxHealth = 100f;    
    [SerializeField] private float currentHealth = 100f;    
    private void Awake()
    {
        Instance = this;
    }
    public void TakeDame(float dame)
    {
        currentHealth -= dame;
        CanvasInGameController.Instance.UpdateHealthUI(currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Handle player death
        }
    }
    private void PlayerDie()
    {

    }
}
