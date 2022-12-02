using System;
using System.Linq;

namespace _20
{
    class Control
    {
        class Game
        {
            private const int SIZE = 4;
            private int[,] _field;

            private int _col;
            private int _row;
            private int _score;

            private bool _moveIsDoing;

            private Random _random;
            private Paint _paint;

            public Game()
            {
                _random = new Random();
                _paint = new Paint();
                _field = new int[SIZE, SIZE];
                _score = 0;
                _moveIsDoing = false;
                for (int i = 0; i < 2; i++) { GetCoordinate(); AssignCoordinate(); }

                ShowAr();
            }

            public void ControlBlock(ConsoleKey key)
            {
                if (key == ConsoleKey.LeftArrow)
                {
                    Left();
                }

                if (key == ConsoleKey.RightArrow)
                {
                    Right();
                }

                if (key == ConsoleKey.UpArrow)
                {
                    Up();
                }

                if (key == ConsoleKey.DownArrow)
                {
                    Down();
                }

                if (IsEmptyCell())
                {
                    if (_moveIsDoing)
                    {
                        GetCoordinate();
                        AssignCoordinate();
                        _moveIsDoing = false;
                    }
                }

                _paint.Clear();
                ShowAr();
            }

            private void Right()
            {
                int startCoordiante = 2;

                for (int i = startCoordiante; i >= 0; i--)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (_field[j, i] == 0)
                        {
                            continue;
                        }

                        int doubleI = i;

                        while (doubleI < SIZE - 1)
                        {
                            if (_field[j, doubleI] != _field[j, doubleI + 1] && _field[j, doubleI + 1] != 0)
                            {
                                break;
                            }

                            _moveIsDoing = true;

                            if (_field[j, doubleI] == _field[j, doubleI + 1])
                            {
                                _field[j, doubleI + 1] *= 2;
                                _field[j, doubleI] = 0;
                                _score += _field[j, doubleI + 1];
                                break;
                            }
                            else if (_field[j, doubleI + 1] == 0)
                            {
                                int val = _field[j, doubleI];
                                _field[j, doubleI] = _field[j, doubleI + 1];
                                _field[j, doubleI + 1] = val;
                                doubleI++;
                            }
                        }
                    }
                }
            }

            private void Left()
            {
                int startCoordiante = 1;

                for (int i = startCoordiante; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (_field[j, i] == 0)
                        {
                            continue;
                        }

                        int doubleI = i;

                        while (doubleI > 0)
                        {
                            if (_field[j, doubleI] != _field[j, doubleI - 1] && _field[j, doubleI - 1] != 0)
                            {
                                break;
                            }

                            _moveIsDoing = true;

                            if (_field[j, doubleI] == _field[j, doubleI - 1])
                            {
                                _field[j, doubleI - 1] *= 2;
                                _field[j, doubleI] = 0;
                                _score += _field[j, doubleI - 1];
                                break;
                            }
                            else if (_field[j, doubleI - 1] == 0)
                            {
                                int val = _field[j, doubleI];
                                _field[j, doubleI] = _field[j, doubleI - 1];
                                _field[j, doubleI - 1] = val;
                                doubleI--;
                            }
                        }
                    }
                }
            }

            private void Up()
            {
                int startCoordiante = 1;

                for (int i = startCoordiante; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (_field[i, j] == 0)
                        {
                            continue;
                        }

                        int doubleI = i;

                        while (doubleI > 0)
                        {
                            if (_field[doubleI, j] != _field[doubleI - 1, j] && _field[doubleI - 1, j] != 0)
                            {
                                break;
                            }

                            _moveIsDoing = true;

                            if (_field[doubleI, j] == _field[doubleI - 1, j])
                            {
                                _field[doubleI - 1, j] *= 2;
                                _field[doubleI, j] = 0;
                                _score += _field[doubleI - 1, j];
                                break;
                            }
                            else if (_field[doubleI - 1, j] == 0)
                            {
                                int val = _field[doubleI, j];
                                _field[doubleI, j] = _field[doubleI - 1, j];
                                _field[doubleI - 1, j] = val;
                                doubleI--;
                            }
                        }
                    }
                }
            }

            private void Down()
            {
                int startCoordiante = 2;

                for (int i = startCoordiante; i >= 0; i--)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (_field[i, j] == 0)
                        {
                            continue;
                        }

                        int doubleI = i;

                        while (doubleI < SIZE - 1)
                        {
                            if (_field[doubleI, j] != _field[doubleI + 1, j] && _field[doubleI + 1, j] != 0)
                            {
                                break;
                            }

                            _moveIsDoing = true;

                            if (_field[doubleI, j] == _field[doubleI + 1, j])
                            {
                                _field[doubleI + 1, j] *= 2;
                                _field[doubleI, j] = 0;
                                _score += _field[doubleI + 1, j];
                                break;
                            }
                            else if (_field[doubleI + 1, j] == 0)
                            {
                                int val = _field[doubleI, j];
                                _field[doubleI, j] = _field[doubleI + 1, j];
                                _field[doubleI + 1, j] = val;
                                doubleI++;
                            }
                        }
                    }
                }
            }

            private bool IsEmptyCell()
            {
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (_field[i, j] == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            private void GetCoordinate()
            {
                _col = _random.Next(SIZE);
                _row = _random.Next(SIZE);

                if (_field[_col, _row] == 0)
                {
                    return;
                }

                for (int i = _col; i < SIZE; i++)
                {
                    for (int j = _row; j < SIZE; j++)
                    {
                        if (_field[i, j] == 0)
                        {
                            _col = i;
                            _row = j;
                            return;
                        }
                    }
                }

                for (int i = _col; i >= 0; i--)
                {
                    for (int j = _row; j >= 0; j--)
                    {
                        if (_field[i, j] == 0)
                        {
                            _col = i;
                            _row = j;
                            return;
                        }
                    }
                }
            }

            private void AssignCoordinate()
            {
                if (_random.Next(100) > 10)
                {
                    _field[_col, _row] = 2;
                }
                else
                {
                    _field[_col, _row] = 4;
                }
            }

            private void ShowAr()
            {
                _paint.ShowField(_field, _score, SIZE);
            }

            public bool IsMovies()
            {
                if (IsEmptyCell())
                {
                    return true;
                }

                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE - 1; j++)
                    {
                        if (_field[i, j] == _field[i, j + 1])
                        {
                            return true;
                        }
                    }
                }

                for (int i = 0; i < SIZE - 1; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (_field[i + 1, j] == _field[i, j])
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        class Paint
        {
            public void ShowField(int[,] field, int score, int size, int cellPadding = 4)
            {
                string plus = "+";
                char minus = '-';
                char vertLine = '|';


                string[,] numsAsString = new string[size, size];
                int maxSymbols = 0;

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        numsAsString[i, j] = field[i, j].ToString();

                        int symbolsCount = numsAsString[i, j].Length;

                        if (symbolsCount > maxSymbols)
                            maxSymbols = symbolsCount;

                    }
                }

                int cellWidth = maxSymbols + cellPadding * 2;

                string horizontalLine = plus + string.Concat(Enumerable.Repeat(plus.PadLeft(cellWidth + 1, minus), size));
                Console.WriteLine(horizontalLine);

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        int leftSpacesCount = cellWidth / 2 - numsAsString[i, j].Length / 2;
                        int rightSpacesCount = cellWidth - numsAsString[i, j].Length - leftSpacesCount;

                        string spacesL = new string(' ', leftSpacesCount);
                        string spacesR = new string(' ', rightSpacesCount);

                        Console.Write($"{vertLine}{spacesL}");

                        Console.ForegroundColor = GetColor(field[i, j]);
                        Console.Write($"{numsAsString[i, j]}{spacesR}");
                        Console.ResetColor();
                    }
                    Console.Write(vertLine + "\n");
                    Console.WriteLine(horizontalLine);
                }
                Console.WriteLine($"Score: {score}");
            }

            private ConsoleColor GetColor(int val)
            {
                return val switch
                {
                    2 => ConsoleColor.Red,
                    4 => ConsoleColor.DarkMagenta,
                    8 => ConsoleColor.Green,
                    16 => ConsoleColor.Magenta,
                    32 => ConsoleColor.Cyan,
                    64 => ConsoleColor.DarkCyan,
                    128 => ConsoleColor.DarkBlue,
                    256 => ConsoleColor.DarkGreen,
                    512 => ConsoleColor.DarkYellow,
                    1024 => ConsoleColor.DarkRed,
                    _ => ConsoleColor.White,
                };
            }

            public void Clear()
            {
                Console.Clear();
            }
        }
    }
}