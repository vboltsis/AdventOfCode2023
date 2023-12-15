using System.Text;

namespace AdventOfCode.Days;

public class Day14
{
    private readonly string _input;
    static char[,] platform;
    static int rows;
    static int cols;

    public Day14()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\14.txt");
    }

    public long Part2()
    {
        var inputs = _input.Split(Environment.NewLine);
        var sum = 0;
        InitializePlatform(inputs);

        var seenStates = new Dictionary<string, int>();
        int cycleCount = 0;
        const int totalCycles = 1000000000;
        int patternLength = 0;

        while (cycleCount < totalCycles)
        {
            string currentState = GetPlatformState();

            if (seenStates.TryGetValue(currentState, out int value))
            {
                patternLength = cycleCount - value;
                break;
            }

            seenStates[currentState] = cycleCount;
            PerformSpinCycle();
            cycleCount++;
        }

        if (patternLength > 0)
        {
            int remainingCycles = (totalCycles - cycleCount) % patternLength;
            for (int i = 0; i < remainingCycles; i++)
            {
                PerformSpinCycle();
            }
        }

        sum = CalculateTotalLoad();

        return sum;
    }

    public long Part1()
    {
        var inputs = _input.Split(Environment.NewLine);
        var sum = 0;

        InitializePlatform(inputs);
        TiltNorth();
        sum = CalculateTotalLoad();

        return sum;
    }

    static void PerformSpinCycle()
    {
        TiltNorth();
        TiltWest();
        TiltSouth();
        TiltEast();
    }

    static string GetPlatformState()
    {
        var sb = new StringBuilder(10200);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                sb.Append(platform[i, j]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    static void InitializePlatform(string[] input)
    {
        rows = input.Length;
        cols = input[0].Length;
        platform = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                platform[i, j] = input[i][j];
            }
        }
    }

    static void TiltNorth()
    {
        for (int col = 0; col < cols; col++)
        {
            int emptySpace = -1;
            for (int row = 0; row < rows; row++)
            {
                if (platform[row, col] == 'O' && emptySpace != -1)
                {
                    platform[emptySpace, col] = 'O';
                    platform[row, col] = '.';
                    emptySpace++;
                }
                else if (platform[row, col] == '.')
                {
                    if (emptySpace == -1)
                        emptySpace = row;
                }
                else if (platform[row, col] == '#')
                {
                    emptySpace = -1;
                }
            }
        }
    }

    static void TiltWest()
    {
        for (int row = 0; row < rows; row++)
        {
            int emptySpace = -1;
            for (int col = 0; col < cols; col++)
            {
                if (platform[row, col] == 'O' && emptySpace != -1)
                {
                    platform[row, emptySpace] = 'O';
                    platform[row, col] = '.';
                    emptySpace++;
                }
                else if (platform[row, col] == '.')
                {
                    if (emptySpace == -1)
                        emptySpace = col;
                }
                else if (platform[row, col] == '#')
                {
                    emptySpace = -1;
                }
            }
        }
    }

    static void TiltSouth()
    {
        for (int col = 0; col < cols; col++)
        {
            int emptySpace = -1;
            for (int row = rows - 1; row >= 0; row--)
            {
                if (platform[row, col] == 'O' && emptySpace != -1)
                {
                    platform[emptySpace, col] = 'O';
                    platform[row, col] = '.';
                    emptySpace--;
                }
                else if (platform[row, col] == '.')
                {
                    if (emptySpace == -1)
                        emptySpace = row;
                }
                else if (platform[row, col] == '#')
                {
                    emptySpace = -1;
                }
            }
        }
    }

    static void TiltEast()
    {
        for (int row = 0; row < rows; row++)
        {
            int emptySpace = -1;
            for (int col = cols - 1; col >= 0; col--)
            {
                if (platform[row, col] == 'O' && emptySpace != -1)
                {
                    platform[row, emptySpace] = 'O';
                    platform[row, col] = '.';
                    emptySpace--;
                }
                else if (platform[row, col] == '.')
                {
                    if (emptySpace == -1) 
                        emptySpace = col;
                }
                else if (platform[row, col] == '#')
                {
                    emptySpace = -1;
                }
            }
        }
    }

    static int CalculateTotalLoad()
    {
        int load = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (platform[row, col] == 'O')
                {
                    load += rows - row;
                }
            }
        }
        return load;
    }
}
