using UnityEngine;
using ToxicFamilyGames.MenuEditor;

public class LanguageAutoDetection : MonoBehaviour
{
    private void Awake()
    {
        SetLanguage();
    }

    private void SetLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Russian:
                TranslateSettings.SetLanguage("ru");
                break;
            case SystemLanguage.English:
                TranslateSettings.SetLanguage("en");
                break;
            case SystemLanguage.Turkish:
                TranslateSettings.SetLanguage("tr");
                break;
            case SystemLanguage.Spanish:
                TranslateSettings.SetLanguage("es");
                break;
            case SystemLanguage.Portuguese:
                TranslateSettings.SetLanguage("pt");
                break;
            default:
                TranslateSettings.SetLanguage("en");
                break;
        }
    }
}
