using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSystem : MonoBehaviour
{   
    public static MergeSystem Instance { get; private set; }
    private CanvasInGameController canvasInGameController;

    [SerializeField] private List<Tank> tankInShootingPoint = new List<Tank>(4);
    private EffectSpawner effectSpawner;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        effectSpawner = EffectSpawner.Instance;
        canvasInGameController = CanvasInGameController.Instance;
    }
    public void AddList(Tank tank)
    {
        for (int i = 0; i < tankInShootingPoint.Count; i++)
        {
            if (tankInShootingPoint[i]==null)
            {
                tankInShootingPoint[i] = tank;
                return;
            }
        }
    }
    public void MergeTwoTank()
    {
        for (int i = 0; i < tankInShootingPoint.Count; i++)
        {   
            Tank tank = tankInShootingPoint[i];
            if (tank ==null)
            {
                continue;
            }
            for (int j = i+1;  j < tankInShootingPoint.Count;  j++)
            {
                if (tankInShootingPoint[j] ==null )
                {
                    continue;
                }
                if ( tankInShootingPoint[j].GetColorIndex() != tank.GetColorIndex())
                {
                    break;
                }
                else
                {
                    ActiveMergeTwoTank(tank, tankInShootingPoint[j], j);
                }
            }

        }
    }
    private void ActiveMergeTwoTank(Tank a, Tank b,int index)
    {
        b.transform.DOMove(a.transform.position, 0.1f).OnComplete(() =>
        {
            {
                effectSpawner.Spawn("LevelUp", a.transform.position, Quaternion.identity);
                a.Upgrade();
                canvasInGameController.amountBulletShowUI.OutOfBullet(index);
                b.GetComponent<Tank>().DeSpawn();
                // Xóa tank B sau khi merge
            }
        });
    }
    public void RemoveAt(int index)
    {
        tankInShootingPoint[index] = null;
    }
    public int IndexTank(Tank tank)
    {
        return tankInShootingPoint.IndexOf(tank);
    }
}
