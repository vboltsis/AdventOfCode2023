using System.Security.Principal;
using System.Text;

namespace AdventOfCode.Days;

public class Day11
{
    private readonly string _input;

    public Day11()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\11.txt");
    }

    public long Part1()
    {
        var inputs = _input.Split(Environment.NewLine);

        var rowsToExpand = Enumerable.Range(0, inputs.Length)
                       .Where(x => inputs[x].All(c => c == '.'))
                       .ToList();

        var columnsToExpand = Enumerable.Range(0, inputs[0].Length)
                                  .Where(col => inputs.All(row => row[col] == '.'))
                                  .ToList();

        var stars = inputs.SelectMany((line, x) => line.Select((col, y) => (col, x, y)))
                        .Where(item => item.col == '#')
                        .Select(item => (item.x, item.y))
                        .ToList();

        long distance = 0, distance2 = 0;

        for (int i = 0; i < stars.Count - 1; i++)
        {
            for (int j = i + 1; j < stars.Count; j++)
            {
                var (x1, y1) = stars[i];
                var (x2, y2) = stars[j];
                foreach (var row in rowsToExpand)
                {
                    if ((x1 < row && row < x2) || (x2 < row && row < x1))
                    {
                        distance += 1;
                        distance2 += 999999;
                    }
                }
                foreach (var column in columnsToExpand)
                {
                    if ((y1 < column && column < y2) || (y2 < column && column < y1))
                    {
                        distance += 1;
                        distance2 += 999999;
                    }
                }
                distance += Math.Abs(x1 - x2) + Math.Abs(y2 - y1);
                distance2 += Math.Abs(x1 - x2) + Math.Abs(y2 - y1);
            }
        }

        return distance;
    }
}
