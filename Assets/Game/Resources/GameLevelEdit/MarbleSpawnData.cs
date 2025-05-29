// MarbleSpawnData.cs
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Marble/Spawn Data", fileName = "NewMarbleSpawnData")]

public class MarbleSpawnData : ScriptableObject
{
    [System.Serializable]
    public class MarbleBatch
    {
        public MarbleColor color;
        public int quantity;
    }

    public List<MarbleBatch> spawnList = new List<MarbleBatch>();
}
