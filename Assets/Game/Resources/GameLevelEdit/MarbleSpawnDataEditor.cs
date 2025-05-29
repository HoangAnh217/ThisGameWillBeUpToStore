#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(MarbleSpawnData))]
public class MarbleSpawnDataEditor : Editor
{
    private List<MarbleColor> colors = new List<MarbleColor>();
    private List<int> quantities = new List<int>();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Add Multiple Marble Batches", EditorStyles.boldLabel);

        // Bắt đầu danh sách
        int removeIndex = -1;
        for (int i = 0; i < colors.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            colors[i] = (MarbleColor)EditorGUILayout.EnumPopup(colors[i]);
            quantities[i] = EditorGUILayout.IntField(quantities[i]);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                removeIndex = i;
            }
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
            MarbleSpawnData data = (MarbleSpawnData)target;

            for (int i = 0; i < colors.Count; i++)
            {
                if (quantities[i] > 0)
                {
                    data.spawnList.Add(new MarbleSpawnData.MarbleBatch
                    {
                        /// color = colors[i],
                        quantity = quantities[i]
                    });
                }
            }

            EditorUtility.SetDirty(data);
            colors.Clear();
            quantities.Clear();
        }
    }
}
#endif
