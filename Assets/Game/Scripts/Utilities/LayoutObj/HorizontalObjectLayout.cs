using UnityEngine;

public class HorizontalObjectLayout : MonoBehaviour
{
    public float spacing = 2f;
    public bool center = true;

    public void ApplyLayout()
    {
        int count = transform.childCount;
        float totalWidth = spacing * (count - 1);
        float startX = center ? -totalWidth / 2f : 0f;

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 pos = child.localPosition;
            pos.x = startX + i * spacing;
            pos.y = 0f;
            pos.z = 0f;
            child.localPosition = pos;
        }
    }
}
