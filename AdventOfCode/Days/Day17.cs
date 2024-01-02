namespace AdventOfCode.Days;

public class Day17
{
    private readonly string _input;

    public Day17()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\17.txt");
    }

    public long Part1()
    {
        var lines = _input.Split(Environment.NewLine);
        long sum = 0;

        var grid = ParseGrid(lines);
        sum = MinHeatLoss(grid, 3);

        return sum;
    }

    public long Part2()
    {
        var lines = _input.Split(Environment.NewLine);
        long sum = 0;

        var grid = ParseGrid(lines);
        sum = MinHeatLoss2(grid);

        return sum;
    }

    private static int[,] ParseGrid(string[] gridStr)
    {
        int rows = gridStr.Length;
        int cols = gridStr[0].Length;
        var grid = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = gridStr[i][j] - '0';
            }
        }

        return grid;
    }

    private readonly record struct State(int X, int Y, int DirIdx, int Moves, int HeatLoss);

    private static int MinHeatLoss2(int[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        var directions = new (int, int)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var pq = new PriorityQueue<State, int>(12384827);
        var visited = new HashSet<State>(145659669);

        pq.Enqueue(new State(0, 0, 1, 0, 0), 0);

        while (pq.Count > 0)
        {
            var currentState = pq.Dequeue();

            if (currentState.X == rows - 1 && currentState.Y == cols - 1)
            {
                return currentState.HeatLoss;
            }

            if (visited.Contains(currentState))
            {
                continue;
            }

            visited.Add(currentState);

            for (int i = -1; i <= 1; i++)
            {
                if (currentState.Moves < 4 && i != 0)
                    continue;

                int newDirIdx = (currentState.DirIdx + i + 4) % 4;
                var (dx, dy) = directions[newDirIdx];
                int nx = currentState.X + dx;
                int ny = currentState.Y + dy;

                if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
                {
                    int newMoves = i == 0 ? currentState.Moves + 1 : 1;
                    if (newMoves <= 10 || (nx == rows - 1 && ny == cols - 1))
                    {
                        int newHeatLoss = currentState.HeatLoss + (nx != 0 || ny != 0 ? grid[nx, ny] : 0);
                        pq.Enqueue(new State(nx, ny, newDirIdx, newMoves, newHeatLoss), newHeatLoss);
                    }
                }
            }
        }

        return -1;
    }
    
    
    private static int MinHeatLoss(int[,] grid, int movesAllowed)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        var directions = new (int, int)[] { (-1, 0), (0, 1), (1, 0), (0, -1) };
        var pq = new PriorityQueue<State, int>(12384827);
        var visited = new HashSet<State>(145659669);

        pq.Enqueue(new State(0, 0, 1, 0, 0), 0);

        while (pq.Count > 0)
        {
            var currentState = pq.Dequeue();

            if (currentState.X == rows - 1 && currentState.Y == cols - 1)
            {
                return currentState.HeatLoss;
            }

            if (visited.Contains(currentState))
            {
                continue;
            }

            visited.Add(currentState);

            for (int i = -1; i <= 1; i++)
            {
                int newDirIdx = (currentState.DirIdx + i + 4) % 4;
                var (dx, dy) = directions[newDirIdx];
                int nx = currentState.X + dx;
                int ny = currentState.Y + dy;

                if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
                {
                    int newMoves = i == 0 ? currentState.Moves + 1 : 1;
                    if (newMoves <= movesAllowed)
                    {
                        int newHeatLoss = currentState.HeatLoss + (nx != 0 || ny != 0 ? grid[nx, ny] : 0);
                        pq.Enqueue(new State(nx, ny, newDirIdx, newMoves, newHeatLoss), newHeatLoss);
                    }
                }
            }
        }

        return -1;
    }

}
