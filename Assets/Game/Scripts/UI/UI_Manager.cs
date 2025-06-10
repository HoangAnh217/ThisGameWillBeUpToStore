using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{   
    public static UI_Manager Instance { get; private set; }

    public GameObject winPopup;
    private void Start()
    {
        Instance = this;
    }
    public void ShowPanelWin()
    {
        //StartCoroutine(ShowWinPanels());
        winPopup.SetActive(true);
    }

    
}
