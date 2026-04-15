using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public int languageId = 1;

    public LanguageText[] languageTexts;
    public EnableObjectByLanguage[] enableObjects;

    void Start()
    {
        languageTexts = FindObjectsOfType<LanguageText>(true);
        enableObjects = FindObjectsOfType<EnableObjectByLanguage>(true);
        SetLanguage();
    }

    public void SetLanguage()
    {
        languageId = 1;

        foreach (LanguageText languageText in languageTexts)
        {
            languageText.SetLanguage(languageId);
        }

        foreach (EnableObjectByLanguage enableObject in enableObjects)
        {
            enableObject.SetLanguage(languageId);
        }
    }
}
