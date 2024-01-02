using System.Drawing;

namespace AdventOfCode.Days;

public class Day18
{
    private readonly string _input;
    private static readonly char[] separator = [' ', '(', ')'];

    public Day18()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\18.txt");
    }

    public long Part1()
    {
        var lines = _input.Split(Environment.NewLine);
        long sum = 0;

        var directionInfos = ParseLines(lines);

        return sum;
    }

    public List<DirectionInfo> ParseLines(string[] lines)
    {
        var directionInfos = new List<DirectionInfo>();

        foreach (var line in lines)
        {
            var parts = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3)
            {
                var directionInfo = new DirectionInfo
                (
                    parts[0][0],
                    int.Parse(parts[1]),
                    ColorTranslator.FromHtml(parts[2])
                );
                directionInfos.Add(directionInfo);
            }
        }

        return directionInfos;
    }

    public long Part2()
    {
        var lines = _input.Split(Environment.NewLine);
        long sum = 0;


        return sum;
    }

    public readonly struct DirectionInfo
    {
        public readonly char Direction;
        public readonly int Number;
        public readonly Color Color;

        public DirectionInfo(char direction, int number, Color color)
        {
            Direction = direction;
            Number = number;
            Color = color;
        }
    }
}
