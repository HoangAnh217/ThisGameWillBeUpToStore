using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageUI : MonoBehaviour
{
    string key = "LanguageCode";
    public void SetLanguage(string languageCode)
    {
        PlayerPrefs.SetString(key, languageCode);
        PlayerPrefs.Save();
    }
}
