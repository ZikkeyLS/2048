using UnityEngine;

public class Swipes : MonoBehaviour
{
    [SerializeField] private GridController _controller;

    public bool LeftSwipe()
    {
        bool[] stopY = new bool[4];
        bool success = false;

        for (int r = 0; r < 4; r++)
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    BlockModel block = _controller.Blocks[x, y];

                    if (x > 0 && block.ContainsBlock)
                    {
                        int freeSpaceCount = 0;

                        for (int i = x - 1; i >= 0; i -= 1)
                        {
                            BlockModel leftBlock = _controller.Blocks[i, y];

                            if (leftBlock.ContainsBlock == false)
                            {
                                freeSpaceCount += 1;
                            }
                            else if (block.Value == leftBlock.Value && stopY[y] == false)
                            {
                                _controller.MoveBlock(true, block, leftBlock);

                                stopY[y] = true;
                                success = true;
                                freeSpaceCount = -1;
                                continue;
                            }
                            else if (block.Value != leftBlock.Value)
                            {
                                break;
                            }
                        }

                        if (freeSpaceCount == -1)
                            continue;

                        if (freeSpaceCount > 0)
                        {
                            BlockModel leftBlock = _controller.Blocks[x - freeSpaceCount, y];

                            _controller.MoveBlock(false, block, leftBlock);

                            success = true;
                        }
                    }
                }

        return success;
    }

    public bool RightSwipe()
    {
        bool[] stopY = new bool[4];
        bool success = false;

        for (int r = 0; r < 4; r++)
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    BlockModel block = _controller.Blocks[x, y];

                    if (x < 3 && block.ContainsBlock)
                    {
                        int freeSpaceCount = 0;

                        for (int i = x + 1; i < 4; i += 1)
                        {
                            BlockModel leftBlock = _controller.Blocks[i, y];

                            if (leftBlock.ContainsBlock == false)
                            {
                                freeSpaceCount += 1;
                            }
                            else if (block.Value == leftBlock.Value && stopY[y] == false)
                            {
                                _controller.MoveBlock(true, block, leftBlock);

                                stopY[y] = true;
                                success = true;
                                freeSpaceCount = -1;
                                continue;
                            }
                            else if (block.Value != leftBlock.Value)
                            {
                                break;
                            }
                        }

                        if (freeSpaceCount == -1)
                            continue;

                        if (freeSpaceCount > 0)
                        {
                            BlockModel leftBlock = _controller.Blocks[x + freeSpaceCount, y];

                            _controller.MoveBlock(false, block, leftBlock);

                            success = true;
                        }
                    }
                }

        return success;
    }

    public bool BottomSwipe()
    {
        bool[] stopX = new bool[4];
        bool success = false;

        for (int r = 0; r < 4; r++)
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    BlockModel block = _controller.Blocks[x, y];

                    if (y < 3 && block.ContainsBlock)
                    {
                        int freeSpaceCount = 0;

                        for (int i = y + 1; i < 4; i += 1)
                        {
                            BlockModel leftBlock = _controller.Blocks[x, i];

                            if (leftBlock.ContainsBlock == false)
                            {
                                freeSpaceCount += 1;
                            }
                            else if (block.Value == leftBlock.Value && stopX[x] == false)
                            {
                                _controller.MoveBlock(true, block, leftBlock);

                                stopX[x] = true;
                                success = true;
                                freeSpaceCount = -1;
                                continue;
                            }
                            else if (block.Value != leftBlock.Value)
                            {
                                break;
                            }
                        }

                        if (freeSpaceCount == -1)
                            continue;

                        if (freeSpaceCount > 0)
                        {
                            BlockModel leftBlock = _controller.Blocks[x, y + freeSpaceCount];

                            _controller.MoveBlock(false, block, leftBlock);

                            success = true;
                        }
                    }
                }

        return success;
    }

    public bool TopSwipe()
    {
        bool[] stopX = new bool[4];
        bool success = false;

        for (int r = 0; r < 4; r++)
            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    BlockModel block = _controller.Blocks[x, y];

                    if (y > 0 && block.ContainsBlock)
                    {
                        int freeSpaceCount = 0;

                        for (int i = y - 1; i >= 0; i -= 1)
                        {
                            BlockModel leftBlock = _controller.Blocks[x, i];

                            if (leftBlock.ContainsBlock == false)
                            {
                                freeSpaceCount += 1;
                            }
                            else if (block.Value == leftBlock.Value && stopX[x] == false)
                            {
                                _controller.MoveBlock(true, block, leftBlock);

                                stopX[x] = true;
                                success = true;
                                freeSpaceCount = -1;
                                continue;
                            }
                            else if (block.Value != leftBlock.Value)
                            {
                                break;
                            }
                        }

                        if (freeSpaceCount == -1)
                            continue;

                        if (freeSpaceCount > 0)
                        {
                            BlockModel leftBlock = _controller.Blocks[x, y - freeSpaceCount];

                            _controller.MoveBlock(false, block, leftBlock);

                            success = true;
                        }
                    }
                }

        return success;
    }

    public bool HasAnyMoves()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4 - 1; y++)
                if (_controller.Blocks[x, y].Value == _controller.Blocks[x, y + 1].Value)
                    return true;

        for (int x = 0; x < 4 - 1; x++)
            for (int y = 0; y < 4; y++)
                if (_controller.Blocks[x + 1, y].Value == _controller.Blocks[x, y].Value)
                    return true;

        return false;
    }
}
