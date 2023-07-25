using System.Collections;
using UnityEngine;

public class StatusScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    [SerializeField] private LocalizedUnique _winUnique;
    [SerializeField] private LocalizedUnique _loseUnique;

    [SerializeField] private Vector2 _panelRelocationStartPoint;
    [SerializeField] private Vector2 _panelRelocationPoint;

    [SerializeField] private float _moveTime = 2;
    [SerializeField] private float _removeScreenDelay = 7;

    public void ShowWinScreen(int moves)
    {
        LeanTween.moveLocal(_winScreen, _panelRelocationPoint, _moveTime);

        _winScreen.GetComponentInChildren<TMPro.TMP_Text>().text = _winUnique.Result(moves);

        StartCoroutine(RemoveWinScreenWithDelay());
    }

    private IEnumerator RemoveWinScreenWithDelay()
    {
        yield return new WaitForSeconds(_removeScreenDelay);

        RemoveWinScreen();
    }

    public void RemoveWinScreen()
    {
        LeanTween.moveLocal(_winScreen, _panelRelocationStartPoint, _moveTime);
    }

    public void ShowLoseScreen(int moves, int score)
    {
        LeanTween.moveLocal(_loseScreen, _panelRelocationPoint, _moveTime);

        _loseScreen.GetComponentInChildren<TMPro.TMP_Text>().text = _loseUnique.Result(score, moves);
    }

    public void RemoveLoseScreen()
    {
        LeanTween.moveLocal(_loseScreen, _panelRelocationStartPoint, _moveTime);
    }
}
