using System.Collections;
using UnityEngine;

public class WinPopup : MonoBehaviour
{
    public GameObject obj1; // GameObject có Animator gắn sẵn
    public GameObject obj2; // GameObject thứ 2

    void OnEnable()
    {
        StartCoroutine(ShowObjectsInSequence());
    }

    IEnumerator ShowObjectsInSequence()
    {
        if (obj1 != null)
        {
            obj1.SetActive(true); // Bật obj1, animation sẽ tự chạy
        }

        yield return new WaitForSeconds(0.23f); // Chờ 0.3 giây

        if (obj2 != null)
        {
            obj2.SetActive(true); // Bật obj2, animation cũng sẽ tự chạy
        }
    }
}
