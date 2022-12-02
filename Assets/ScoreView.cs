using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _score;

    public void UpdateUI(int score)
    {
        _score.text = score.ToString();
    }
}
