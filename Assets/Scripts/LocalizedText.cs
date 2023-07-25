using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] private LocalizedValue _ru;
    [SerializeField] private LocalizedValue _en;
    [SerializeField] private LocalizedValue _tr;

    private IEnumerator Start()
    {
#if UNITY_EDITOR || !UNITY_WEBGL
        yield break;
#endif
        yield return new WaitUntil(() => YandexLocalization.Initialized);

        Dictionary<LanguageYandex, LocalizedValue> _parsedLocalization = new()
        { 
            { LanguageYandex.ru, _ru },
            { LanguageYandex.en, _en },
            { LanguageYandex.tr, _tr },
        };

        TMPro.TMP_Text localizedObject = GetComponent<TMPro.TMP_Text>();
        localizedObject.text = _parsedLocalization[YandexLocalization.Language].Result(new object[0]);
    }
}
