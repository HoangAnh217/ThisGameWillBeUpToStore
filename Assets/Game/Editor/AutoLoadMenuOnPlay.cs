#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoPlayHandler
{
   /* static AutoPlayHandler()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        // Khi vừa bắt đầu vào Play mode
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            Debug.Log("Play mode started. Saving data and loading Menu scene...");

            // Lưu dữ liệu
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetString("LastPlayTime", System.DateTime.Now.ToString());
            PlayerPrefs.Save();

            // Tải scene "Menu"
            SceneManager.LoadScene("Menu");
        }
    }*/
}
#endif
