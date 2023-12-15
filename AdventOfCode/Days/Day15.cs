using System.Text;

namespace AdventOfCode.Days;

public class Day15
{
    private readonly string _input;

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
            sum += CalculateHash(sequence);
        }

        return sum;
    }

    static int CalculateHash(string input)
    {
        var currentValue = 0;

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
        var line = _input.Split(Environment.NewLine);
        long sum = 0;

        var boxes = new List<List<Lens>>(256);

        for (int i = 0; i < 256; i++)
        {
            boxes.Add(new List<Lens>());
        }

        var sequences = line[0].Split(',');

        foreach (var sequence in sequences)
        {
            ProcessInstruction(boxes, sequence);
        }

        sum = CalculateFocusingPower(boxes);

        return sum;
    }

    static void ProcessInstruction(List<List<Lens>> boxes, string instruction)
    {
        string label = instruction.Replace("-", "");

        if (label.Contains('='))
        {
            label = label[..^2];
        }

        char operation = instruction.Contains('=') ? '=' : '-';

        var instructionSpan = instruction.AsSpan();
        int focalLength = operation == '=' ? int.Parse(instructionSpan.Slice(instructionSpan.Length - 1, 1)) : 0;

        int boxNumber = CalculateHash(label);

        int lensIndex = boxes[boxNumber].FindIndex(lens => lens.Label == label);

        if (operation == '=')
        {
            var newLens = new Lens (label, focalLength);

            if (lensIndex != -1)
            {
                boxes[boxNumber][lensIndex] = newLens;
            }
            else
            {
                boxes[boxNumber].Add(newLens);
            }
        }
        else if (operation == '-')
        {
            if (lensIndex != -1)
            {
                boxes[boxNumber].RemoveAt(lensIndex);
            }
        }
    }

    static long CalculateFocusingPower(List<List<Lens>> boxes)
    {
        long totalPower = 0;

        for (int boxIndex = 0; boxIndex < boxes.Count; boxIndex++)
        {
            var box = boxes[boxIndex];
            for (int slotIndex = 0; slotIndex < box.Count; slotIndex++)
            {
                Lens lens = box[slotIndex];
                int boxNumber = boxIndex + 1;
                int slotNumber = slotIndex + 1;
                int lensFocusingPower = boxNumber * slotNumber * lens.FocalStrength;
                totalPower += lensFocusingPower;
            }
        }

        return totalPower;
    }

    public readonly struct Lens
    {
        public readonly string Label { get; }
        public readonly int FocalStrength { get; }

        public Lens(string label, int focalStrength)
        {
            Label = label;
            FocalStrength = focalStrength;
        }
    }
}
