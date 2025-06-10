using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    private const string LevelKey = "PlayerLevel";

    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt(LevelKey, level);
        PlayerPrefs.Save();
        Debug.Log("Saved level: " + level);
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt(LevelKey, 1); 
    }

    public static void ResetLevel()
    {
        PlayerPrefs.DeleteKey(LevelKey);
        Debug.Log("Level reset.");
    }
}
