using Agava.YandexGames;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _rank;
    [SerializeField] private TMPro.TMP_Text _score;
    [SerializeField] private TMPro.TMP_Text _maxScore;

    public void UpdateUI(int score)
    {
        _score.text = $"Current:\n{score}";
        _maxScore.text = $"Max:\n{GlobalData.MaxScore}";
    }

    public void UpdateRank()
    {
        if (GlobalData.Rank == -1)
        {
            _rank.text = "Unranked";
        }
        else
        {
            Leaderboard.GetPlayerEntry("ScoreTable", (result) =>
            {
                if (result != null)
                {
                    GlobalData.Rank = result.rank;
                    _rank.text = $"Rank #{GlobalData.Rank}";
                }
            });
        }
    }
}
