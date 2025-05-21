using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    public string currentLanguageCode;
    [SerializeField] private List<string> languageCodes; // Phải khớp thứ tự với tickImage
    [SerializeField] private List<RectTransform> tickImage;
    public void SetLanguage(string languageCode)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (var locale in locales)
        {
            if (locale.Identifier.Code == languageCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                PlayerPrefs.SetString("LanguageCode", languageCode);
                PlayerPrefs.Save();

                currentLanguageCode = languageCode;
                UpdateLanguageButtons();
                break;
            }
        }
    }

    void Start()
    {
        string savedCode = PlayerPrefs.GetString("LanguageCode", "");
        if (!string.IsNullOrEmpty(savedCode))
        {
            SetLanguage(savedCode);
        }
        else
        {
            currentLanguageCode = LocalizationSettings.SelectedLocale?.Identifier.Code;
            UpdateLanguageButtons();
        }
    }

    public void UpdateLanguageButtons()
    {
        for (int i = 0; i < tickImage.Count; i++)
        {
            bool isCurrent = (languageCodes[i] == currentLanguageCode);
            tickImage[i].gameObject.SetActive(isCurrent);
        }
    }
}
