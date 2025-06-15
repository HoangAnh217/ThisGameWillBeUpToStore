using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PointShootingController : MonoBehaviour
{
    public static PointShootingController Instance { get; private set; }

    [SerializeField] private List<Transform> points = new List<Transform>();
    private List<bool> isUnlocked = new List<bool>();
    private List<bool> isOccupied = new List<bool>();

    [SerializeField] private List<Tank> tankInShootingPoint = new List<Tank>(4);

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
        isUnlocked = new List<bool>(new bool[points.Count]);
        isOccupied = new List<bool>(new bool[points.Count]);

        // Mặc định unlock 4 điểm đầu tiên
        isUnlocked[0] = true;
        isUnlocked[1] = true;
        isUnlocked[2] = true;
        isUnlocked[3] = true;
    }

    public void UnlockNextPoint()
    {
        for (int i = 0; i < isUnlocked.Count; i++)
        {
            if (!isUnlocked[i])
            {
                isUnlocked[i] = true;
                Debug.Log($"Unlocked point {i}");
                return;
            }
        }
        Debug.Log("All points already unlocked.");
    }

    public Transform GetPoint()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (isUnlocked[i] && !isOccupied[i])
            {
                isOccupied[i] = true;
                return points[i];
            }
        }

        Debug.Log("No available unlocked slot");
        return null;
    }

    public void SetObj()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (isUnlocked[i] && !isOccupied[i])
            {
                isOccupied[i] = true;
                return;
            }
        }
    }

    public bool IsFullSlot()
    {
        for (int i = 0; i < isUnlocked.Count; i++)
        {
            if (isUnlocked[i] && !isOccupied[i])
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

    public void AddList(Tank tank)
    {
        for (int i = 0; i < tankInShootingPoint.Count; i++)
        {
            if (tankInShootingPoint[i] == null)
            {
                tankInShootingPoint[i] = tank;
                isOccupied[i] = true;
                return;
            }
        }
    }
    public void RemoveAt(int index)
    {
        tankInShootingPoint[index] = null;
        isOccupied[index] = false;
    }

    public int IndexTank(Tank tank)
    {
        return tankInShootingPoint.IndexOf(tank);
    }
}
