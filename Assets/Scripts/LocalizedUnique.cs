using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LocalizedUnique
{
    [SerializeField] private LocalizedValue _ru;
    [SerializeField] private LocalizedValue _en;
    [SerializeField] private LocalizedValue _tr;

    public string Result(params object[] parameters)
    {
        if (YandexLocalization.Initialized == false)
            return "Language is not initialized";

        Dictionary<LanguageYandex, LocalizedValue> _parsedLocalization = new()
        {
            { LanguageYandex.ru, _ru },
            { LanguageYandex.en, _en },
            { LanguageYandex.tr, _tr },
        };

        return _parsedLocalization[YandexLocalization.Language].Result(parameters);
    }
}
