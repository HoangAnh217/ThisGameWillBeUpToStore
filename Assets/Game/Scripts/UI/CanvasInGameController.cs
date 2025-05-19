using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInGameController : MonoBehaviour
{
    private static CanvasInGameController instance;
    public static CanvasInGameController Instance => instance;

    [HideInInspector] public AmountBulletShowUI amountBulletShowUI;
    public Image health ;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        amountBulletShowUI = GetComponentInChildren<AmountBulletShowUI>();
    }
    public void UpdateHealthUI(float healthValue)
    {
        health.fillAmount = healthValue;
    }
}
