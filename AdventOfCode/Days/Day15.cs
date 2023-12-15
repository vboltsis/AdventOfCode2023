using System.Text;

namespace AdventOfCode.Days;

public class Day15
{
    private readonly string _input;
    static char[,] platform;
    static int rows;
    static int cols;

    public Day15()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\15.txt");
    }

    public long Part1()
    {
        var line = _input.Split(Environment.NewLine);
        long sum = 0;

        var sequences = line[0].Split(',');

        foreach (var sequence in sequences)
        {
            sum += CalculateSum(sequence);
        }

        return sum;
    }

    public long CalculateSum(string input)
    {
        long currentValue = 0;

        foreach (var character in input)
        {
            int asciiValue = (int)character;
            currentValue += asciiValue;
            currentValue *= 17;
            currentValue %= 256;
        }

        return currentValue;
        
    }

    public long Part2()
    {
        var inputs = _input.Split(Environment.NewLine);
        var sum = 0;

        return sum;
    }
}
