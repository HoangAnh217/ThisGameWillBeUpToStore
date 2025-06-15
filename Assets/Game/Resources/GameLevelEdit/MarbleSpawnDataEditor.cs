#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(MarbleSpawnData))]
public class MarbleSpawnDataEditor : Editor
{
    private List<MarbleColor> colors = new List<MarbleColor>();
    private List<int> quantities = new List<int>();

    // Input fields for desired total per color
    private Dictionary<MarbleColor, int> desiredCounts = Enum.GetValues(typeof(MarbleColor))
        .Cast<MarbleColor>()
        .ToDictionary(c => c, c => 0);

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var data = (MarbleSpawnData)target;

        // --- Summary số lượng theo màu ---
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("🗒️ Marble Count Summary", EditorStyles.boldLabel);

        var counts = Enum.GetValues(typeof(MarbleColor))
                         .Cast<MarbleColor>()
                         .ToDictionary(c => c, c => 0);
        foreach (var batch in data.spawnList)
        {
            if (counts.ContainsKey(batch.color))
                counts[batch.color] += batch.quantity;
        }
        foreach (var kv in counts)
        {
            EditorGUILayout.LabelField(kv.Key.ToString(), kv.Value.ToString());
        }

        // --- Add multiple batches ---
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Add Multiple Marble Batches", EditorStyles.boldLabel);

        int removeIndex = -1;
        for (int i = 0; i < colors.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            colors[i] = (MarbleColor)EditorGUILayout.EnumPopup(colors[i]);
            quantities[i] = EditorGUILayout.IntField(quantities[i]);
            if (GUILayout.Button("X", GUILayout.Width(20))) removeIndex = i;
            EditorGUILayout.EndHorizontal();
        }
        if (removeIndex >= 0)
        {
            colors.RemoveAt(removeIndex);
            quantities.RemoveAt(removeIndex);
        }
        if (GUILayout.Button("Add New Row"))
        {
            colors.Add(MarbleColor.Red);
            quantities.Add(1);
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Add All to Spawn List"))
        {
            Undo.RecordObject(data, "Add All to Spawn List");
            for (int i = 0; i < colors.Count; i++)
            {
                if (quantities[i] > 0)
                {
                    data.spawnList.Add(new MarbleSpawnData.MarbleBatch
                    {
                        color = colors[i],
                        quantity = quantities[i]
                    });
                }
            }
            colors.Clear();
            quantities.Clear();
            EditorUtility.SetDirty(data);
        }

        // --- Shuffle existing list ---
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Shuffle Spawn List"))
        {
            Undo.RecordObject(data, "Shuffle Spawn List");
            Shuffle(data.spawnList);
            EditorUtility.SetDirty(data);
        }

        // --- Generate random batches from desired totals ---
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("🎯 Desired Totals per Color", EditorStyles.boldLabel);
        foreach (var color in desiredCounts.Keys.ToList())
        {
            int input = EditorGUILayout.IntField(color.ToString(), desiredCounts[color]);
            desiredCounts[color] = Mathf.Max(0, input);
        }
        EditorGUILayout.Space(5);
        if (GUILayout.Button("Generate Random Batches"))
        {
            Undo.RecordObject(data, "Generate Random Batches");
            data.spawnList.Clear();

            var newList = new List<MarbleSpawnData.MarbleBatch>();
            var rng = new System.Random();
            foreach (var kv in desiredCounts)
            {
                int remaining = kv.Value;
                while (remaining > 0)
                {
                    int maxBatch = Math.Min(12, remaining);
                    int batchQty = rng.Next(1, maxBatch + 1);
                    newList.Add(new MarbleSpawnData.MarbleBatch
                    {
                        color = kv.Key,
                        quantity = batchQty
                    });
                    remaining -= batchQty;
                }
            }
            // Shuffle combined list
            for (int i = newList.Count - 1; i > 0; i--)
            {
                int j = rng.Next(0, i + 1);
                var tmp = newList[i];
                newList[i] = newList[j];
                newList[j] = tmp;
            }
            data.spawnList.AddRange(newList);
            EditorUtility.SetDirty(data);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }
}
#endif
