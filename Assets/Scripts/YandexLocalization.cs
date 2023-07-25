using Agava.YandexGames;
using UnityEngine;

public enum LanguageYandex : byte
{
    ru = 0,
    en,
    tr
}

public class YandexLocalization : MonoBehaviour
{
    public static bool Initialized = false;
    public static LanguageYandex Language = LanguageYandex.en;

    public void Intitialize()
    {
        switch (YandexGamesSdk.Environment.i18n.lang.ToLower())
        {
            case "ru":
                Language = LanguageYandex.ru;
                break;
            case "en":
                Language = LanguageYandex.en;
                break;
            case "tr":
                Language = LanguageYandex.tr;
                break;
        }

        Initialized = true;
    }
}
