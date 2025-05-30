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
    [SerializeField] private MarbleColor currentColorType; 

    private MarbleDespawn marbleDespawn;
    private MarbleManager marbleManager;

    private void Start()
    {
        marbleDespawn = GetComponent<MarbleDespawn>();
        marbleManager = MarbleManager.Instance;
    }
    public void SetColor(MarbleColor colorType)
    {
        spriteRenderer.color = GetColor(colorType);
        currentColorType = colorType;
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
    public int  GetCurrentColorIndex()
    {
        switch (currentColorType)
        {
            case MarbleColor.Red: return 0;
            case MarbleColor.Blue: return 1;
            case MarbleColor.Green: return 2;
            case MarbleColor.Yellow: return 3;
            case MarbleColor.Purple: return 4;
            default: return -1;
        }
    }
    public void Die()
    {   
       // marbleManager.RemoveMarble(GetComponent<MarbleMover2D>());
        marbleDespawn.DeSpawnObj();
    }
}
