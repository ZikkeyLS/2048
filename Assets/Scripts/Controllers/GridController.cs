using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public BlockModel[,] Blocks { get; private set; }

    [SerializeField] private GridView _view;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private StatusScreenView _statusScreenView;

    [SerializeField] private SwipeSound _swipeSound;

    [SerializeField] private Swipes _swipes;
    [SerializeField] private Image _blockPrefab;

    [SerializeField] private int _possibilityToGenerate = 8;
    [SerializeField] private int _maximumGenerationIndex = 10;

    private int _score = 0;
    private int _moves = 0;

    private bool _loseEmulated = false;
    private bool _winEmulated = false;

    private void Awake()
    {
        _view = GetComponent<GridView>();

        InitializeBlocks();
        GenerateRandomGrid();
    }

    public void RestartGame()
    {
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

    public void MoveBlock(bool connect, BlockModel previousBlock, BlockModel block)
    {
        if (connect)
        {
            block.MultiplyValue(2);
            _score += block.Value;

            block.SetPreviousBlock(previousBlock);
            ClearBlock(previousBlock);
        }
        else
        {
            block.SetValue(previousBlock.Value);
            block.SetContainsBlock(true);

            block.SetPreviousBlock(previousBlock);
            ClearBlock(previousBlock);
        }
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
            _statusScreenView.ShowLoseScreen(_moves, _score);
            _loseEmulated = true;
        }
    }

    // Костыль, один раз появился блок со значением 0, так что на всякий
    private void TryShowWinScreen()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
            {
                BlockModel block = Blocks[x, y];

                if (block.ContainsBlock && block.Value == 0)
                    ClearBlock(block);

                if (block.ContainsBlock && block.Value == 2048 && _winEmulated == false)
                {
                    _statusScreenView.ShowWinScreen(_score);
                    _winEmulated = true;
                }
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
