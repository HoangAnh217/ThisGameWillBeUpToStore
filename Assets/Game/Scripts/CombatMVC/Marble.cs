using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarbleColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple
}

public class Marble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetColor(MarbleColor colorType)
    {
        spriteRenderer.color = GetColor(colorType);
    }

    private Color GetColor(MarbleColor colorType)
    {
        switch (colorType)
        {
            case MarbleColor.Red: return Color.red;
            case MarbleColor.Blue: return Color.blue;
            case MarbleColor.Green: return Color.green;
            case MarbleColor.Yellow: return Color.yellow;
            case MarbleColor.Purple: return new Color(0.6f, 0.2f, 0.8f); // tím custom
            default: return Color.white;
        }
    }
}
