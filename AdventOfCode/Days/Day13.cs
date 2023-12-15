using System.Text;

namespace AdventOfCode.Days;

public class Day13
{
    private readonly string _input;

    public Day13()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\13.txt");
    }

    public long Part1()
    {
        var inputs = _input.Split(Environment.NewLine);
        var patterns = ParsePatterns(inputs);
        long part1Score = 0, part2Score = 0;

        foreach (var pattern in patterns)
        {
            part1Score += FindReflectionScore(pattern, 0);
            part2Score += FindReflectionScore(pattern, 2);
        }

        return part1Score;
    }

    private static int FindReflectionScore(string pattern, int nonmatches)
    {
        var rows = pattern.Split('\n').Select(row => row.ToCharArray()).ToArray();
        var reflectionRow = FindReflection(rows, nonmatches);
        if (reflectionRow.HasValue)
            return 100 * (reflectionRow.Value + 1);

        var cols = Transpose(rows);
        var reflectionCol = FindReflection(cols, nonmatches);
        if (reflectionCol.HasValue)
            return reflectionCol.Value + 1;

        return 0;
    }

    static int? FindReflection(char[][] pattern, int allowedDiscrepancies)
    {
        for (int reflectionLine = 0; reflectionLine < pattern.Length - 1; reflectionLine++)
        {
            int discrepancyCount = 0;

            for (int rowIndex = 0; rowIndex < pattern.Length; rowIndex++)
            {
                int mirroredIndex = reflectionLine + 1 + (reflectionLine - rowIndex);

                if (mirroredIndex >= 0 && mirroredIndex < pattern.Length)
                {
                    discrepancyCount += CountDiscrepancies(pattern[rowIndex], pattern[mirroredIndex]);
                }
            }

            if (discrepancyCount == allowedDiscrepancies)
            {
                return reflectionLine;
            }
        }

        return null;
    }

    private static int CountDiscrepancies(char[] row, char[] mirroredRow)
    {
        return row.Where((character, index) => character != mirroredRow[index]).Count();
    }

    static char[][] Transpose(char[][] array)
    {
        int width = array[0].Length;
        int height = array.Length;
        char[][] transposed = new char[width][];
        for (int i = 0; i < width; i++)
        {
            transposed[i] = new char[height];
            for (int j = 0; j < height; j++)
            {
                transposed[i][j] = array[j][i];
            }
        }
        return transposed;
    }

    private static List<string> ParsePatterns(string[] patterns)
    {
        var parsedPatterns = new List<string>(100);
        var currentPattern = new StringBuilder();

        foreach (var item in patterns)
        {
            if (item is "")
            {
                parsedPatterns.Add(currentPattern.ToString().TrimEnd());
                currentPattern = new StringBuilder();
            }
            else
            {
                currentPattern.Append(item);
                currentPattern.Append('\n');
            }
        }

        if (currentPattern.Length > 0)
        {
            parsedPatterns.Add(currentPattern.ToString().TrimEnd());
        }

        return parsedPatterns;
    }


    public long Part2()
    {
        var inputs = _input.Split(Environment.NewLine);
        long sum = 0;


        return sum;
    }
}
