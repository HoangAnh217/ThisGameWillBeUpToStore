using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HorizontalObjectLayout))]
public class HorizontalObjectLayoutEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HorizontalObjectLayout layout = (HorizontalObjectLayout)target;

        if (GUILayout.Button("Apply Layout"))
        {
            layout.ApplyLayout();
        }
    }
}
