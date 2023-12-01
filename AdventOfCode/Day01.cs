using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day01
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\01.txt");
    }

    public int Part1()
    {
        var inputs = _input.Split(Environment.NewLine).ToList();
        var result = ParseInputs(inputs);

        return result;
    }
    public int Part2()
    {
        var inputs = _input.Split(Environment.NewLine)
            .Select(x => ReplaceString(x))
            .ToList();

        var result = ParseInputs(inputs);

        return result;
    }

    private string ReplaceString(string x)
    {
        var replaced = x.Replace("one", "o1e")
        .Replace("two", "t2o")
        .Replace("three", "t3e")
        .Replace("four", "f4r")
        .Replace("five", "f5e")
        .Replace("six", "s6x")
        .Replace("seven", "s7n")
        .Replace("eight", "e8t")
        .Replace("nine", "n9e");

        return replaced;
    }

    private static int ParseInputs(List<string> inputs)
    {
        var sum = 0;

        foreach (var input in inputs)
        {
            var firstDigit = -1000000;
            var secondDigit = 0;

            foreach (var character in input)
            {
                if (char.IsDigit(character))
                {
                    if (firstDigit == -1000000)
                    {
                        firstDigit = int.Parse(character.ToString());
                    }

                    secondDigit = int.Parse(character.ToString());
                }
            }

            var finalNumber = int.Parse($"{firstDigit}{secondDigit}");
            sum += finalNumber;
        }

        return sum;
    }
}
