using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [Header("Wave Data")]
    [SerializeField] private LevelData levelData;

    [Header("UI References")]
    [SerializeField] private RectTransform progressBarContainer; // Container width = full bar
    [SerializeField] private Image progressBarFill;      // (optional) nếu bạn dùng Image.fillAmount
    [SerializeField] private RectTransform flagPrefab;           // Prefab phải có Image component
    [SerializeField] private RectTransform flagParent;           // Prefab phải có Image component

    private List<float> timeStartWave = new List<float>();
    private List<RectTransform> flagMarkers = new List<RectTransform>();
    private List<bool> flagActivated = new List<bool>();
    private float timer;
    private int currentWaveIndex = 0;   
    private float totalDuration;

    private void Start()
    {
        for (int i = 0; i < levelData.Waves.Count; i++)
            timeStartWave.Add(levelData.Waves[i].StartTime);

        totalDuration = timeStartWave[timeStartWave.Count - 1];

        timer = 0f;
        InitMarker();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // (Tuỳ chọn) cập nhật fill amount nếu bạn xài Image.fill
        if (progressBarFill != null)
            progressBarFill.fillAmount = Mathf.Clamp01(timer / totalDuration);
        if (currentWaveIndex >= timeStartWave.Count)
        {
           // Debug.Log("Final wave");
            return;
        }
        if (timer >= timeStartWave[currentWaveIndex])
        {
            flagMarkers[currentWaveIndex].GetComponent<Image>().color = Color.white;
            currentWaveIndex++;
        }
    }

    private void InitMarker()
    {
        for (int i = 0; i < timeStartWave.Count; i++)
        {
            float norm = timeStartWave[i] / totalDuration;

            var flag = Instantiate(flagPrefab, flagParent);
            flag.gameObject.SetActive(true); 
            flag.anchorMin = new Vector2(0f, 0.5f);
            flag.anchorMax = new Vector2(0f, 0.5f);
            flag.pivot = new Vector2(0.5f, 0.5f);

            float xPos = norm * progressBarContainer.rect.width;
            flag.anchoredPosition = new Vector2(xPos, 0f);

            flagMarkers.Add(flag);
            flagActivated.Add(false);
        }
    }
}
