using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmountBulletShowUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> tmp_List;
    private void Start()
    {
        // Lấy tất cả TextMeshProUGUI trong các con
       // tmp_List = new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>());
    }
    public void UpdateTmp(int idPoint,int amountBullet)
    {
      //  tmp_List[idPoint].text = amountBullet.ToString();
    }
    public void OutOfBullet(int idPoint)
    {
//tmp_List[idPoint].transform.parent.gameObject.SetActive(false);
    }
    public void ActiveTmp(int idPoint)
    {
       // tmp_List[idPoint].transform.parent.gameObject.SetActive(true);
//tmp_List[idPoint].text = "8";
    }
}
