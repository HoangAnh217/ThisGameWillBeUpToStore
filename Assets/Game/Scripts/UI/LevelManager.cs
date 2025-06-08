using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("UI Setup")]
    public Transform levelsParent;             // Parent chứa các level buttons
    //public GameObject characterIcon;           // Icon nhân vật
    public ScrollRect scrollRect;              // ScrollRect chứa danh sách level
    public RectTransform content;              // Content của ScrollView
    public RectTransform viewport;             // Viewport của ScrollView

    [Header("Level Settings")]
    public int currentLevel = 1;               // Level hiện tại (bắt đầu từ 1)

    [Header("Scroll Limits")]
    [SerializeField] private float minY = 0f;  // phải nhất (do Reverse Arrangement)
    [SerializeField] private float maxY = 1f;  // trái nhất


    private List<Transform> levelList = new List<Transform>();

    void Start()
    {
        LoadLevels();
        UpdateCurrentLevelUI();
        //Invoke("ScrollToCurrentLevel", 0.1f);  // Đợi layout xong rồi scroll
    }
    private void Update()
    {
        float posY = content.anchoredPosition.y;
        if (posY < minY)
        {
            float newY = Mathf.Lerp(posY, minY, Time.deltaTime * 5f);
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, newY);
        }
        else if (posY > maxY)
        {
            float newY = Mathf.Lerp(posY, maxY, Time.deltaTime * 5f);
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, newY);
        }
    }

    void LoadLevels()
    {
        levelList.Clear();

        foreach (Transform child in levelsParent)
        {
            levelList.Add(child);

            // Gán số level (nếu có Text)
            TextMeshProUGUI levelText = child.GetComponentInChildren<TextMeshProUGUI>();
            if (levelText != null)
            {
                int levelNumber = levelList.Count;
                levelText.text = levelNumber.ToString();
            }

            // Gán sự kiện click nếu cần
            Button btn = child.GetComponent<Button>();
            int index = levelList.Count;
            if (btn != null)
            {
                btn.onClick.AddListener(() => OnLevelClicked(index));
            }
        }
    }

    void UpdateCurrentLevelUI()
    {
        if (currentLevel < 1 || currentLevel > levelList.Count)
        {
            Debug.LogWarning("Current level out of range");
            return;
        }

        // Gán character icon vào đúng level
        Transform currentTransform = levelList[currentLevel - 1];
      //  characterIcon.transform.SetParent(currentTransform);
      //  characterIcon.transform.localPosition = Vector3.zero;

        // Highlight level hiện tại
        foreach (Transform level in levelList)
        {
            Image img = level.GetComponent<Image>();
            if (img != null) img.color = Color.gray;
        }

        Image currentImg = currentTransform.GetComponent<Image>();
        if (currentImg != null) currentImg.color = Color.yellow;
    }

    void OnLevelClicked(int level)
    {
        Debug.Log("Clicked level: " + level);
        SceneController.instance.LoadScene(level);
    }

    public void SetCurrentLevel(int newLevel)
    {
        currentLevel = newLevel;
        UpdateCurrentLevelUI();
    //    ScrollToCurrentLevel();
    }

    /*void ScrollToCurrentLevel()
    {
        if (currentLevel <= 0 || currentLevel > content.childCount)
            return;

        RectTransform levelItem = content.GetChild(currentLevel - 1).GetComponent<RectTransform>();

        float contentWidth = content.rect.height;
        float viewportWidth = viewport.rect.height;

        float itemCenterX = -levelItem.localPosition.y + (levelItem.rect.height / 2f);

        // Tính tỉ lệ scroll
        float normalizedPos = Mathf.Clamp01((itemCenterX - viewportWidth / 2f) / (contentWidth - viewportWidth));

        // Vì đang dùng Reverse Arrangement → cần giữ nguyên (không đảo ngược như dọc)
        scrollRect.horizontalNormalizedPosition = normalizedPos;
    }*/

}
