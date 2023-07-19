using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class YandexInitializer : MonoBehaviour
{
    [SerializeField] private ScoreView _view;

    private IEnumerator Start()
    {
#if UNITY_EDITOR || !UNITY_WEBGL
        yield break;
#endif

        if (YandexGamesSdk.IsInitialized)
            yield break;

        yield return YandexGamesSdk.Initialize();

        Leaderboard.GetPlayerEntry("ScoreTable", (result) =>
        {
            if (result != null)
            {
                GlobalData.Rank = result.rank;
                GlobalData.MaxScore = result.score;

            }

            _view.UpdateRank();
            _view.UpdateUI(0);
        });

        StickyAd.Show();
    }

}
