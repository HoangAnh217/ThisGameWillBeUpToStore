using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSpawner : Spawner
{
    public static MarbleSpawner Instance { get; private set; }
    public static string Marble = "Marble";
    protected override void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ đối tượng này khi chuyển cảnh
        }
        else
        {
            Destroy(gameObject); // Nếu đã có một instance, hủy đối tượng này
        }*/
        Instance = this;
    }
    public Transform Spawn(MarbleColor color,string name, Vector3 pos, Quaternion rotattion)
    {
        Transform a =  base.Spawn(name, pos, rotattion);
        Marble marble = a.GetComponent<Marble>();
        marble.SetColor(color);
        return a;
    }
}
