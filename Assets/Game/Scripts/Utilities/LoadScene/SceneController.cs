using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField] private Animator transitionAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Không phá hủy object này khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectLevel(int level)
    {
        StartCoroutine(SelectLevelPlay(level));
    }
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }
    private IEnumerator LoadLevel(int sceneIndex)
    {
        // Fade to black (Start)
        if (transitionAnim != null)
        {
            transitionAnim.SetTrigger("Start");
            yield return new WaitForSeconds(0.6f); // đợi màn hình đen hoàn toàn
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null; // đợi frame tiếp theo
        }

        if (transitionAnim != null)
        {
            transitionAnim.SetTrigger("End"); // fade out từ đen sang game
        }

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Scene loaded and activated.");
    }


    public IEnumerator SelectLevelPlay(int level)
    {
        if (transitionAnim != null)
        {
            transitionAnim.SetTrigger("Start");
            yield return new WaitForSeconds(1);
        }

        int nextSceneIndex = level;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadSceneAsync(nextSceneIndex);

        if (transitionAnim != null)
        {
            transitionAnim.SetTrigger("End");
        }
    }
}
