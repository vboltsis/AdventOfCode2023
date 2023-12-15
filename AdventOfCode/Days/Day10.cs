namespace AdventOfCode.Days;

public class Day10
{
    private readonly string _input;

    public Day10()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\10.txt");
    }

    private static readonly (int Χ, int Υ)[] Directions = [(0, 1), (1, 0), (0, -1), (-1, 0)];
    private static readonly string[] PipeTypesPerDirection = ["-7J", "|LJ", "-FL", "|F7"];
    private static readonly Dictionary<(int, char), int> DirectionTransformations = new()
    {
        {(0, '-'), 0}, {(0, '7'), 1}, {(0, 'J'), 3},
        {(2, '-'), 2}, {(2, 'F'), 1}, {(2, 'L'), 3},
        {(1, '|'), 1}, {(1, 'L'), 0}, {(1, 'J'), 2},
        {(3, '|'), 3}, {(3, 'F'), 0}, {(3, '7'), 2},
    };

    public long Part1()
    {
        var grid = _input.Split(Environment.NewLine);

        int height = grid.Length;
        int width = grid[0].Length;

        var startPosition = FindStartPosition(grid);
        var validDirectionsFromStart = FindValidDirectionsFromStart(grid, startPosition);
        bool[,] loopPath = new bool[height, width];
        int loopLength = TraceLoop(grid, startPosition, validDirectionsFromStart, loopPath);

        var result = loopLength / 2;

        return result;
    }

    public long Part2()
    {
        var grid = _input.Split(Environment.NewLine);
        int height = grid.Length;
        int width = grid[0].Length;

        var startPosition = FindStartPosition(grid);
        var validDirectionsFromStart = FindValidDirectionsFromStart(grid, startPosition);
        bool[,] loopPath = new bool[height, width];
        _ = TraceLoop(grid, startPosition, validDirectionsFromStart, loopPath);

        var canGoUp = validDirectionsFromStart.Contains(3);
        int areaWithinLoop = CalculateAreaWithinLoop(grid, loopPath, canGoUp);

        return areaWithinLoop;
    }

    private static (int Χ, int Υ) FindStartPosition(string[] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            int position = grid[i].IndexOf('S');
            if (position >= 0)
                return (i, position);
        }
        return (-1, -1);
    }

    private static List<int> FindValidDirectionsFromStart(string[] grid, (int Χ, int Υ) startPosition)
    {
        var validDirections = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            var (dx, dy) = Directions[i];
            int x = startPosition.Χ + dx;
            int y = startPosition.Υ + dy;
            if (x >= 0 && x < grid.Length && y >= 0 &&
                y < grid[0].Length &&
                PipeTypesPerDirection[i].Contains(grid[x][y]))
            {
                validDirections.Add(i);
            }
        }
        return validDirections;
    }

    private static int TraceLoop(string[] grid, (int Χ, int Υ) startPosition,
        List<int> validDirections, bool[,] loopPath)
    {
        int currentDirection = validDirections[0];
        var (x, y) = (startPosition.Χ +
            Directions[currentDirection].Χ, startPosition.Υ + Directions[currentDirection].Υ);
        int length = 1;
        loopPath[startPosition.Χ, startPosition.Υ] = true;

        while ((x, y) != startPosition)
        {
            loopPath[x, y] = true;
            length++;
            currentDirection = DirectionTransformations[(currentDirection, grid[x][y])];
            (x, y) = (x + Directions[currentDirection].Χ, y + Directions[currentDirection].Υ);
        }

        return length;
    }

    private static int CalculateAreaWithinLoop(string[] grid, bool[,] loopPath, bool canGoUp)
    {
        int count = 0;
        for (int row = 0; row < grid.Length; row++)
        {
            bool inside = false;
            for (int column = 0; column < grid.Length; column++)
            {
                if (loopPath[row, column])
                {
                    if ("|JL".Contains(grid[row][column]) || (grid[row][column] == 'S' && canGoUp))
                        inside = !inside;
                }
                else if (inside)
                    count++;
            }
        }
        return count;
    }
}
