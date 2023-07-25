using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public BlockModel[,] Blocks { get; private set; }

    [SerializeField] private GridView _view;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private StatusScreenView _statusScreenView;

    [SerializeField] private Sounds _swipeSound;

    [SerializeField] private Swipes _swipes;
    [SerializeField] private Image _blockPrefab;

    [SerializeField] private int _possibilityToGenerate = 8;
    [SerializeField] private int _maximumGenerationIndex = 10;

    private const int GamesTillAd = 2;

    private int _score = 0;
    private int _moves = 0;
    private int _gamesTillAd = 0;

    private bool _loseEmulated = false;
    private bool _winEmulated = false;

    private void Awake()
    {
        _view = GetComponent<GridView>();
        _gamesTillAd = GamesTillAd;
        
        InitializeBlocks();
        GenerateRandomGrid();
    }

    public void RestartGame()
    {
        TryUpdateScore();

        _score = 0;
        _moves = 0;
        _loseEmulated = false;
        _winEmulated = false;

        ClearBlocks();

        GenerateRandomGrid();

        _statusScreenView.RemoveLoseScreen();
        _statusScreenView.RemoveWinScreen();

        _scoreView.UpdateUI(_score);
        _view.UpdateUI(Blocks, _blockPrefab);

#if !UNITY_EDITOR
        _gamesTillAd -= 1;

        if (_gamesTillAd == 0)
        {
            InterstitialAd.Show(() => { Sounds.Instance.PauseMusic(); }, (arg) => { Sounds.Instance.UnPauseMusic(); });
            _gamesTillAd = GamesTillAd;
        }
#endif
    }

    public void ClearBlocks()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                ClearBlock(Blocks[x, y]);
    }

    public void ClearBlock(BlockModel block)
    {
        block.SetValue(0);
        block.SetContainsBlock(false);
    }

    public void MoveBlock(bool connect, BlockModel from, BlockModel to)
    {
        if (connect)
        {
            to.MultiplyValue(2);
            _score += to.Value;
        }
        else
        {
            to.SetValue(from.Value);
            to.SetContainsBlock(true);
        }

        to.SetPreviousBlock(from);
        ClearBlock(from);
    }

    public void Swipe(TouchInput.Direction direction)
    {
        bool status = false;

        switch (direction)
        {
            case TouchInput.Direction.left:
                status = _swipes.LeftSwipe();
                break;
            case TouchInput.Direction.right:
                status = _swipes.RightSwipe();
                break;
            case TouchInput.Direction.bottom:
                status = _swipes.BottomSwipe();
                break;
            case TouchInput.Direction.top:
                status = _swipes.TopSwipe();
                break;
        }

        if (status)
        {
            _swipeSound.Play();
            _moves += 1;
        }

        if (status && UsedSlotsCount() != 16)
            GenerateRandomBlock();

        TryShowWinScreen();

        _view.UpdateUI(Blocks, _blockPrefab);
        _scoreView.UpdateUI(_score);

        if (UsedSlotsCount() == 16 && _swipes.HasAnyMoves() == false && _loseEmulated == false)
        {
            TryUpdateScore();
            _statusScreenView.ShowLoseScreen(_moves, _score);
            _loseEmulated = true;
        }
    }

    private void TryShowWinScreen()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
            {
                BlockModel block = Blocks[x, y];

                if (block.ContainsBlock && block.Value == 2048 && _winEmulated == false)
                {
                    TryUpdateScore();
                    _statusScreenView.ShowWinScreen(_score);
                    _winEmulated = true;
                }
            }
    }

    private void TryUpdateScore()
    {
        if (_score > GlobalData.MaxScore)
        {
            GlobalData.MaxScore = _score;

#if !UNITY_EDITOR
            Leaderboard.SetScore("ScoreTable", GlobalData.MaxScore, () => { _scoreView.UpdateRank(); });
#endif
        }
    }

    private void InitializeBlocks()
    {
        Blocks = new BlockModel[4, 4];

        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                Blocks[x, y] = new(transform.GetChild(x + (4 * y)));
    }

    private void GenerateRandomGrid()
    {
        BlockModel[] blocks = PlaceRandomBlocks(Random.Range(2, 4), false);

        for (int i = 0; i < blocks.Length; i++)
            blocks[i].SetValue(i == 2 ? 4 : 2);

        _view.UpdateUI(Blocks, _blockPrefab);
        _scoreView.UpdateUI(_score);
    }

    private void GenerateRandomBlock()
    {
        BlockModel block = PlaceRandomBlocks(1, false)[0];

        block.SetValue(Random.Range(0, _maximumGenerationIndex) > _possibilityToGenerate ? 4 : 2);

        _view.UpdateUI(Blocks, _blockPrefab);
        _scoreView.UpdateUI(_score);
        _view.SmoothScale(block);
    }

    private BlockModel[] PlaceRandomBlocks(int count, bool updateUI = true)
    {
        List<BlockModel> blocks = new();

        while (blocks.Count < count)
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    BlockModel slot = Blocks[x, y];

                    if (blocks.Count < count && slot.ContainsBlock == false && Random.Range(0, _maximumGenerationIndex) > _possibilityToGenerate)
                    {
                        slot.SetContainsBlock(true);
                        blocks.Add(slot);
                    }
                }

        if (updateUI)
        {
            _view.UpdateUI(Blocks, _blockPrefab);
            _scoreView.UpdateUI(_score);
        }

        return blocks.ToArray();
    }

    private int UsedSlotsCount()
    {
        int count = 0;

        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
            {
                BlockModel slot = Blocks[x, y];

                if (slot.ContainsBlock)
                    count += 1;
            }

        return count;
    }
}
