namespace AdventOfCode.Days;

public class Day09
{
    private readonly string _input;

    public Day09()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\09.txt");
    }

    public long Part1()
    {
        var inputs = _input.Split(Environment.NewLine);

        long sum = inputs.Sum(input => FindNextValue(CalculateDifferences(input)));

        return sum;
    }

    public long Part2()
    {
        var inputs = _input.Split(Environment.NewLine);

        long sum = inputs.Sum(input => FindPreviousValue(CalculateDifferences(input)));

        return sum;
    }

    private static long FindNextValue(List<List<long>> differences)
    {
        for (int i = differences.Count - 2; i >= 0; i--)
        {
            differences[i].Add(differences[i + 1][^1] + differences[i][^1]);
        }

        return differences[0][^1];
    }

    private static long FindPreviousValue(List<List<long>> differences)
    {
        for (int i = differences.Count - 2; i >= 0; i--)
        {
            differences[i].Insert(0, differences[i][0] - differences[i + 1][0]);
        }

        return differences[0][0];
    }

    private static List<List<long>> CalculateDifferences(string history)
    {
        var differences = new List<List<long>>
        {
            history.Split(' ').Select(long.Parse).ToList()
        };

        while (true)
        {
            var currentHistory = differences[^1];
            var nextHistory = new List<long>();

            for (int i = 0; i < currentHistory.Count - 1; i++)
            {
                nextHistory.Add(currentHistory[i + 1] - currentHistory[i]);
            }

            if (nextHistory.All(h => h == 0))
            {
                break;
            }

            differences.Add(nextHistory);
        }

        return differences;
    }
}
