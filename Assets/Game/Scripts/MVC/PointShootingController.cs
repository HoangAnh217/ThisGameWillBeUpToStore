using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointShootingController : MonoBehaviour
{   
    public static PointShootingController Instance { get; private set; }
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private List<bool> HaveSlot = new List<bool>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        HaveSlot = new List<bool>(new bool[points.Count]);
    }
    public Transform GetPoint()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!HaveSlot[i])
            {
                HaveSlot[i] = true;
                return points[i];
            }
        }

        Debug.Log("full slot");
        return null;
    }
    public void RemoveObj(int index)
    {
        HaveSlot[index] = false;

    }
    public void SetObj()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!HaveSlot[i])
            {
                HaveSlot[i] = true;
                return;
            }
        }
    }
    public bool IsFullSlot()
    {
        for (int i = 0; i < HaveSlot.Count; i++)
        {
            if (!HaveSlot[i])
            {
                return false;
            }
        }
        return true;
    }
    public int GetIndex(Transform trans)
    {
        return points.IndexOf(trans);
    }
}
