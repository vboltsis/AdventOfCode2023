using System.Text;

namespace AdventOfCode.Days;

public class Day12
{
    private readonly string _input;

    public Day12()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\12.txt");
    }

    public long Part1()
    {
        var inputs = _input.Split(Environment.NewLine);
        long sum = 0;

        var springs = inputs.Select(x => x.Split(' ')).ToList();

        Parallel.ForEach(springs, spring =>
        {
            var localSum = CountArrangements(spring);

            Interlocked.Add(ref sum, localSum);
        });

        return sum;
    }



    public long Part2()
    {
        var inputs = _input.Split(Environment.NewLine);
        long sum = 0;

        var current = 1;
        foreach (var line in inputs)
        {
            var parts = line.Split(' ');
            var springs = parts[0];
            var values = parts[1];

            var newSprings = GenerateFiveInputs(true, springs);
            var newValues = GenerateFiveInputs(false, values)
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var memo = new Dictionary<(int, int, int), long>();
            sum += DepthFirstSearch(memo, newSprings, newValues, 0, 0, 0);

            Console.WriteLine(current);
            current++;
        }

        return sum;
    }

    private static long DepthFirstSearch(
        Dictionary<(int rowIndex, int groupIndex, int withinGroupCount), long> memoization,
        string springsPattern,
        List<int> groupSizes,
        int currentIndex,
        int currentGroupIndex,
        int springsInCurrentGroup)
    {
        if (memoization.TryGetValue((currentIndex, currentGroupIndex, springsInCurrentGroup), out long cachedResult))
        {
            return cachedResult;
        }

        if (currentIndex == springsPattern.Length)
        {
            bool isLastGroupComplete = currentGroupIndex == groupSizes.Count - 1 && springsInCurrentGroup == groupSizes[currentGroupIndex];
            bool areAllGroupsProcessed = currentGroupIndex == groupSizes.Count && springsInCurrentGroup == 0;

            return (areAllGroupsProcessed || isLastGroupComplete) ? 1 : 0;
        }

        long count = 0;
        char currentSpring = springsPattern[currentIndex];

        if (currentSpring == '.' || currentSpring == '?')
        {
            if (springsInCurrentGroup == 0)
            {
                count += DepthFirstSearch(memoization, springsPattern, groupSizes, currentIndex + 1, currentGroupIndex, 0);
            }
            else if (currentGroupIndex < groupSizes.Count && springsInCurrentGroup == groupSizes[currentGroupIndex])
            {
                count += DepthFirstSearch(memoization, springsPattern, groupSizes, currentIndex + 1, currentGroupIndex + 1, 0);
            }
        }

        if ((currentSpring == '#' || currentSpring == '?') && currentGroupIndex < groupSizes.Count && springsInCurrentGroup < groupSizes[currentGroupIndex])
        {
            count += DepthFirstSearch(memoization, springsPattern, groupSizes, currentIndex + 1, currentGroupIndex, springsInCurrentGroup + 1);
        }

        memoization[(currentIndex, currentGroupIndex, springsInCurrentGroup)] = count;
        return count;
    }

    /// <summary>
    /// // PART 1 //////////////////////////////////////////
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static int CountArrangements(string[] input)
    {
        string springs = input[0];
        int[] groupSizes = input[1].Split(',').Select(int.Parse).ToArray();

        var arrangements = GenerateArrangements(springs, 0);

        int validArrangements = arrangements.Count(arr => IsValidArrangement(arr, groupSizes));

        return validArrangements;
    }

    private static List<string> GenerateArrangements(string springs, int index)
    {
        if (index == springs.Length)
        {
            return new List<string> { springs };
        }

        if (springs[index] != '?')
        {
            return GenerateArrangements(springs, index + 1);
        }

        var results = new List<string>();
        results.AddRange(GenerateArrangements(springs.Remove(index, 1).Insert(index, "."), index + 1));
        results.AddRange(GenerateArrangements(springs.Remove(index, 1).Insert(index, "#"), index + 1));
        return results;
    }

    private static bool IsValidArrangement(string arrangement, int[] groupSizes)
    {
        int groupIndex = 0;
        int count = 0;

        foreach (char c in arrangement)
        {
            if (c == '#')
            {
                count++;
                if (groupIndex >= groupSizes.Length || count > groupSizes[groupIndex])
                {
                    return false;
                }
            }
            else
            {
                if (count > 0)
                {
                    if (count != groupSizes[groupIndex])
                    {
                        return false;
                    }
                    groupIndex++;
                    count = 0;
                }
            }
        }

        if (count != 0)
        {
            if (count != groupSizes[groupIndex])
            {
                return false;
            }
            groupIndex++;
        }

        return groupIndex == groupSizes.Length;
    }

    private string GenerateFiveInputs(bool isSpring, string input)
    {
        var builder = new StringBuilder();

        for (int i = 0; i < 4; i++)
        {
            builder.Append(input);

            if (isSpring)
            {
                builder.Append('?');
            }
            else
            {
                builder.Append(',');
            }
        }

        builder.Append(input);

        return builder.ToString();
    }
}
