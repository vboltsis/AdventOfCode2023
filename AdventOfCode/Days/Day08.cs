using System.Text.RegularExpressions;

namespace AdventOfCode.Days;

public partial class Day08
{
    private readonly string _input;

    public Day08()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\08.txt");
    }

    public int Part1()
    {
        var inputs = _input.Split(Environment.NewLine);
        var totalSteps = 0;

        var steps = inputs[0];
        var values = GetMap(inputs[2..]);

        KeyValuePair<string, (string Left, string Right)> currentPosition = values.FirstOrDefault(x => x.Key == "AAA");

        Start:
            foreach (var step in steps)
            {
                if (step == 'R')
                {
                    currentPosition = values.FirstOrDefault(x => x.Key == currentPosition.Value.Right);
                }
                else
                {
                    currentPosition = values.FirstOrDefault(x => x.Key == currentPosition.Value.Left);
                }

                totalSteps += 1;

                if (currentPosition.Key is "ZZZ")
                {
                    break;
                }

                if (totalSteps % steps.Length == 0)
                {
                    goto Start;
                }
            }

        return totalSteps;
    }

    public long Part2()
    {
        var inputs = _input.Split(Environment.NewLine);
        var steps = inputs.First();
        var regex = AOCRegex();

        var map = inputs.Skip(2)
            .Select(line => regex.Match(line))
            .Where(match => match.Success)
            .ToDictionary(
                match => match.Groups[1].Value,
                match => new List<string> { match.Groups[2].Value, match.Groups[3].Value }
            );

        var counts = map.Keys
            .Where(key => key.EndsWith('A'))
            .Select(startingPoint =>
            {
                var current = startingPoint;
                long count = 0;
                while (!current.EndsWith('Z'))
                {
                    foreach (var step in steps)
                    {
                        current = step == 'R' ? map[current][1] : map[current][0];
                    }
                    count += steps.Length;
                }
                return count;
            })
            .ToList();

        var lcm = counts.Aggregate(Lcm);

        return lcm;
    }

    private static long Lcm(long a, long b)
    {
        return a / Gcd(a, b) * b;
    }

    private static long Gcd(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static Dictionary<string, (string Left, string Right)> GetMap(string[] inputs)
    {
        var resultList = new Dictionary<string, (string Left, string Right)>();

        foreach (var input in inputs)
        {
            var parts = input.Split('=');

            var valuesPart = parts[1].Trim().TrimStart('(').TrimEnd(')');

            var values = valuesPart.Split(',');

            var left = values[0].Trim();
            var right = values[1].Trim();

            resultList.Add(parts[0].Trim(), (left, right));
        }

        return resultList;
    }

    public int BruteForcePart2()
    {
        var inputs = _input.Split(Environment.NewLine);
        var totalSteps = 0;

        var steps = inputs[0];
        var values = GetMap(inputs[2..]).OrderByDescending(s => s.Key.EndsWith('A'))
            .ThenBy(s => s.Key.EndsWith('A'))
            .ToList(); ;

        KeyValuePair<string, (string Left, string Right)> currentPosition1 = values.FirstOrDefault();
        KeyValuePair<string, (string Left, string Right)> currentPosition2 = values.Skip(1).FirstOrDefault();
        KeyValuePair<string, (string Left, string Right)> currentPosition3 = values.Skip(2).FirstOrDefault();
        KeyValuePair<string, (string Left, string Right)> currentPosition4 = values.Skip(3).FirstOrDefault();
        KeyValuePair<string, (string Left, string Right)> currentPosition5 = values.Skip(4).FirstOrDefault();
        KeyValuePair<string, (string Left, string Right)> currentPosition6 = values.Skip(5).FirstOrDefault();

    Start:
        foreach (var step in steps)
        {
            if (step == 'R')
            {
                currentPosition1 = values.FirstOrDefault(x => x.Key == currentPosition1.Value.Right);
                currentPosition2 = values.FirstOrDefault(x => x.Key == currentPosition2.Value.Right);
                currentPosition3 = values.FirstOrDefault(x => x.Key == currentPosition3.Value.Right);
                currentPosition4 = values.FirstOrDefault(x => x.Key == currentPosition4.Value.Right);
                currentPosition5 = values.FirstOrDefault(x => x.Key == currentPosition5.Value.Right);
                currentPosition6 = values.FirstOrDefault(x => x.Key == currentPosition6.Value.Right);
            }
            else
            {
                currentPosition1 = values.FirstOrDefault(x => x.Key == currentPosition1.Value.Left);
                currentPosition2 = values.FirstOrDefault(x => x.Key == currentPosition2.Value.Left);
                currentPosition3 = values.FirstOrDefault(x => x.Key == currentPosition3.Value.Left);
                currentPosition4 = values.FirstOrDefault(x => x.Key == currentPosition4.Value.Left);
                currentPosition5 = values.FirstOrDefault(x => x.Key == currentPosition5.Value.Left);
                currentPosition6 = values.FirstOrDefault(x => x.Key == currentPosition6.Value.Left);
            }

            totalSteps += 1;

            if (currentPosition1.Key.EndsWith('Z') &&
                currentPosition2.Key.EndsWith('Z') &&
                currentPosition3.Key.EndsWith('Z') &&
                currentPosition4.Key.EndsWith('Z') &&
                currentPosition5.Key.EndsWith('Z') &&
                currentPosition6.Key.EndsWith('Z'))
            {
                break;
            }

            if (totalSteps % steps.Length == 0)
            {
                goto Start;
            }
        }

        return totalSteps;
    }

    [GeneratedRegex(@"([A-Z]{3}) = \(([A-Z]{3}), ([A-Z]{3})\)")]
    private static partial Regex AOCRegex();
}
