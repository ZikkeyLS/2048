using UnityEngine;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private GameObject _screen;

    [SerializeField] private Vector2 _panelRelocationStartPoint;
    [SerializeField] private Vector2 _panelRelocationPoint;

    [SerializeField] private float _moveTime = 2;

    private bool _showed = false;


    public void Toggle()
    {
        _showed = !_showed;

        if (_showed)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        LeanTween.moveLocal(_screen, _panelRelocationPoint, _moveTime);

        GlobalData.InMenu = true;
    }

    private void Hide()
    {
        LeanTween.moveLocal(_screen, _panelRelocationStartPoint, _moveTime);

        GlobalData.InMenu = false;
    }
}
