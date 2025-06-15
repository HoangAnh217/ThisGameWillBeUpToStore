#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(MarbleSpawnData))]
public class MarbleSpawnDataEditor : Editor
{
    private List<MarbleColor> colors = new List<MarbleColor>();
    private List<int> quantities = new List<int>();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var data = (MarbleSpawnData)target;

        // --- Phần 1: Summary số lượng theo màu ---
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("🗒️ Marble Count Summary", EditorStyles.boldLabel);

        // Khởi tạo dict cho tất cả enum
        var counts = Enum.GetValues(typeof(MarbleColor))
                         .Cast<MarbleColor>()
                         .ToDictionary(c => c, c => 0);

        // Cộng dồn quantity
        foreach (var batch in data.spawnList)
        {
            if (counts.ContainsKey(batch.color))
                counts[batch.color] += batch.quantity;
        }

        // Hiển thị
        foreach (var kv in counts)
        {
            EditorGUILayout.LabelField(
                $"{kv.Key}",
                $"{kv.Value}",
                GUILayout.MinWidth(100)
            );
        }

        // --- Phần 2: Thêm batch thủ công như trước ---
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Add Multiple Marble Batches", EditorStyles.boldLabel);

        // ... phần code thêm mới rows giống như bạn đã có ...

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

        // --- Phần 3: Shuffle ---
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Shuffle Spawn List"))
        {
            Undo.RecordObject(data, "Shuffle Spawn List");
            Shuffle(data.spawnList);
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
