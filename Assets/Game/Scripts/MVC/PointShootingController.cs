using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointShootingController : MonoBehaviour
{   
    public static PointShootingController Instance { get; private set; }
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private List<bool> pointsBool = new List<bool>();


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
        pointsBool = new List<bool>(new bool[points.Count]);
    }
    public Transform GetPoint()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!pointsBool[i])
            {
                return points[i];
            }
        }

        Debug.Log("full slot");
        return null;
    }
    public void RemoveObj(int index)
    {
        pointsBool[index] = false;

    }
    public void SetObj()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (!pointsBool[i])
            {
                pointsBool[i] = true;
                return;
            }
        }
    }
    public int GetIndex(Transform trans)
    {
        return points.IndexOf(trans);
    }
}
