namespace AdventOfCode.Days;

public class Day16
{
    private readonly string _input;
    static char[,] beamGrid;
    static int gridColumns;
    static int gridRows;

    public Day16()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\16.txt");
    }

    public long Part1()
    {
        var lines = _input.Split(Environment.NewLine);
        long sum = 0;

        InitializeBeamGrid(lines);
        var result = Solve(beamGrid);

        return sum;
    }

    public static (int Part1, int Part2) Solve(char[,] grid)
    {
        int part1Result = GetBeamCoverage(grid, (0, -1, 0, 1));
        int part2Result = 0;

        for (int row = 0; row < gridRows; row++)
        {
            part2Result = Math.Max(part2Result, GetBeamCoverage(grid, (row, -1, 0, 1)));
            part2Result = Math.Max(part2Result, GetBeamCoverage(grid, (row, gridColumns, 0, -1)));
        }

        for (int column = 0; column < gridColumns; column++)
        {
            part2Result = Math.Max(part2Result, GetBeamCoverage(grid, (-1, column, 1, 0)));
            part2Result = Math.Max(part2Result, GetBeamCoverage(grid, (gridRows, column, -1, 0)));
        }

        return (part1Result, part2Result);
    }

    public static int GetBeamCoverage(char[,] grid, (int row, int column, int deltaRow, int deltaColumn) start)
    {
        var seenTiles = new HashSet<(int, int, int, int)>();
        var tileQueue = new Stack<(int, int, int, int)>();
        tileQueue.Push(start);

        while (tileQueue.Count > 0)
        {
            var (currentRow, currentColumn, rowIncrement, columnIncrement) = tileQueue.Pop();

            int nextRow = currentRow + rowIncrement;
            int nextColumn = currentColumn + columnIncrement;

            if (seenTiles.Contains((nextRow, nextColumn, rowIncrement, columnIncrement)))
                continue;

            if (!(0 <= nextRow && nextRow < gridRows && 0 <= nextColumn && nextColumn < gridColumns)) 
                continue;

            seenTiles.Add((nextRow, nextColumn, rowIncrement, columnIncrement));
            char currentTile = grid[nextRow, nextColumn];

            switch (currentTile)
            {
                case '/':
                    (rowIncrement, columnIncrement) = (-columnIncrement, -rowIncrement);
                    break;
                case '\\':
                    (rowIncrement, columnIncrement) = (columnIncrement, rowIncrement);
                    break;
                case '|':
                    if (columnIncrement != 0)
                    {
                        (rowIncrement, columnIncrement) = (1, 0);
                        tileQueue.Push((nextRow, nextColumn, -1, 0));
                    }
                    break;
                case '-':
                    if (rowIncrement != 0)
                    {
                        (rowIncrement, columnIncrement) = (0, 1);
                        tileQueue.Push((nextRow, nextColumn, 0, -1));
                    }
                    break;
            }
            tileQueue.Push((nextRow, nextColumn, rowIncrement, columnIncrement));
        }

        var energizedTiles = seenTiles.Select(x => (x.Item1, x.Item2)).ToHashSet();
        return energizedTiles.Count;
    }

    static void InitializeBeamGrid(string[] input)
    {
        gridRows = input.Length;
        gridColumns = input[0].Length;
        beamGrid = new char[gridRows, gridColumns];

        for (int rowIndex = 0; rowIndex < gridRows; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < gridColumns; columnIndex++)
            {
                beamGrid[rowIndex, columnIndex] = input[rowIndex][columnIndex];
            }
        }
    }
}
