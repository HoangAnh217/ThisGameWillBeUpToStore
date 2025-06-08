using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointShootingController : MonoBehaviour
{   
    public static PointShootingController Instance { get; private set; }

    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private List<bool> HaveSlot = new List<bool>();
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
    public void AddList(Tank tank)
    {
        for (int i = 0; i < tankInShootingPoint.Count; i++)
        {
            if (tankInShootingPoint[i] == null)
            {
                tankInShootingPoint[i] = tank;
                HaveSlot[i] = true; 
                return;
            }
        }
    }
    public void MergeTwoTank()
    {
        /*for (int i = 0; i < tankInShootingPoint.Count; i++)
        {
            Tank tank = tankInShootingPoint[i];
            if (tank == null)
            {
                continue;
            }
            for (int j = i + 1; j < tankInShootingPoint.Count; j++)
            {
                if (tankInShootingPoint[j] == null)
                {
                    continue;
                }
                if (tankInShootingPoint[j].GetColorIndex() != tank.GetColorIndex())
                {
                    break;
                }
                else
                {
                    ActiveMergeTwoTank(tank, tankInShootingPoint[j], j);
                }
            }

        }*/
    }
    private void ActiveMergeTwoTank(Tank a, Tank b, int index)
    {
        b.transform.DOMove(a.transform.position, 0.1f).OnComplete(() =>
        {
            {
               // effectSpawner.Spawn("LevelUp", a.transform.position, Quaternion.identity);
                a.GetComponent<TankController>().Upgrade();
              //  canvasInGameController.amountBulletShowUI.OutOfBullet(index);
                b.GetComponent<Tank>().DeSpawn();
                // Xóa tank B sau khi merge
            }
        });
    }
    public void RemoveAt(int index)
    {
        tankInShootingPoint[index] = null;
        HaveSlot[index] = false;
    }
    public int IndexTank(Tank tank)
    {
        return tankInShootingPoint.IndexOf(tank);
    }
}
