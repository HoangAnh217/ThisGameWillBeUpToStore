using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInGameController : MonoBehaviour
{
    private static CanvasInGameController instance;
    public static CanvasInGameController Instance => instance;

    public AmountBulletShowUI amountBulletShowUI;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        amountBulletShowUI = GetComponentInChildren<AmountBulletShowUI>();
    }
}
