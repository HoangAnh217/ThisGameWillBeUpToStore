using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{   
    public static UI_Manager Instance { get; private set; }

    public GameObject winPopup;
    public GameObject losePopup;
    private void Start()
    {
        Instance = this;
    }
    public void ShowPanelWin()
    {
        //StartCoroutine(ShowWinPanels());
        winPopup.SetActive(true);
        SaveData.SaveLevel(SaveData.LoadLevel() + 1); // Tăng level khi thắng
    } 
    public void ShowPanelLose()
    {
        //StartCoroutine(ShowWinPanels());
        losePopup.SetActive(true);
    }

    
}
