using UnityEngine;

public class SaveDataViewer : MonoBehaviour
{
    public int currentLevel;

    private void OnValidate()
    {
        currentLevel = SaveData.LoadLevel(); // cập nhật mỗi khi Inspector refresh
    }

    [ContextMenu("Reset Player Level")]
    public void ResetPlayerLevel()
    {
        SaveData.ResetLevel();
        currentLevel = SaveData.LoadLevel(); // cập nhật lại sau khi reset
    }

    [ContextMenu("Next Level (Test)")]
    public void SimulateNextLevel()
    {
        int newLevel = SaveData.LoadLevel() + 1;
        SaveData.SaveLevel(newLevel);
        currentLevel = newLevel;
    }
}
