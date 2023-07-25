using Agava.YandexGames;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private LocalizedUnique _rankUnique;
    [SerializeField] private LocalizedUnique _scoreUnique;
    [SerializeField] private LocalizedUnique _maxScoreUnique;

    [SerializeField] private TMPro.TMP_Text _rank;
    [SerializeField] private TMPro.TMP_Text _score;
    [SerializeField] private TMPro.TMP_Text _maxScore;

    public void UpdateUI(int score)
    {
        _score.text = _scoreUnique.Result(score);
        _maxScore.text = _maxScoreUnique.Result(GlobalData.MaxScore);
    }

    public void UpdateRank()
    {
        if (GlobalData.Rank == -1)
        {
            _rank.text = "";
        }
        else
        {
            Leaderboard.GetPlayerEntry("ScoreTable", (result) =>
            {
                if (result != null)
                {
                    GlobalData.Rank = result.rank;
                    _rank.text = _rankUnique.Result(GlobalData.Rank);
                }
            });
        }
    }
}
