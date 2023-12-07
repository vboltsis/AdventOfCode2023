using System.Text;

namespace AdventOfCode.Days;

internal class Day03
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\03.txt");
    }

    public int Part1()
    {
        var inputs = _input.Split(Environment.NewLine).ToList();
        var numbers = new List<NumberAndPosition>(1215);
        var symbols = new List<SymbolAndPosition>(720);
        ExtractNumbersAndSymbols(inputs, numbers, symbols);

        var sum = numbers
            .Where(number =>
            {
                var startRow = number.Start.Row;
                var startColumn = number.Start.Column - 1;
                var endColumn = number.End.Column + 1;

                return symbols.Any(symbol =>
                    Math.Abs(symbol.Position.Row - startRow) <= 1 &&
                    symbol.Position.Column >= startColumn &&
                    symbol.Position.Column <= endColumn);
            })
            .Sum(number => number.Number);

        return sum;
    }

    public int Part2()
    {
        var inputs = _input.Split(Environment.NewLine).ToList();
        var numbers = new List<NumberAndPosition>(1215);
        var symbols = new List<SymbolAndPosition>(720);
        ExtractNumbersAndSymbols(inputs, numbers, symbols);

        var sum = symbols
            .Where(symbol => symbol.Value == '*')
            .Select(symbol =>
            {
                var row = symbol.Position.Row;
                var column = symbol.Position.Column;
                return numbers
                    .Where(number =>
                        Math.Abs(row - number.Start.Row) <= 1 &&
                        column >= number.Start.Column - 1 &&
                        column <= number.End.Column + 1)
                    .ToArray();
            })
            .Where(gears => gears.Length == 2)
            .Sum(gears => gears[0].Number * gears[1].Number);

        return sum;
    }

    private static void ExtractNumbersAndSymbols(List<string> inputs, List<NumberAndPosition> numbers, List<SymbolAndPosition> symbols)
    {
        for (var row = 0; row < inputs.Count; row++)
        {
            for (var col = 0; col < inputs[row].Length; col++)
            {
                var character = inputs[row][col];
                if (character is '.')
                    continue;

                var digits = new StringBuilder();

                if (char.IsDigit(character))
                {
                    digits.Append(character);
                    var start = (row, col);

                    while (col < inputs[row].Length - 1 && char.IsDigit(inputs[row][col + 1]))
                    {
                        digits.Append(inputs[row][col + 1]);
                        col++;
                    }

                    var end = (row, col);
                    var value = int.Parse(digits.ToString());
                    numbers.Add(new NumberAndPosition(value, start, end));
                }
                else
                {
                    symbols.Add(new SymbolAndPosition(character, (row, col)));
                }
            }
        }
    }

    readonly record struct NumberAndPosition
    {
        public readonly int Number;
        public readonly (int Row, int Column) Start;
        public readonly (int Row, int Column) End;

        public NumberAndPosition(int number, (int Row, int Column) start, (int Row, int Column) end)
        {
            Number = number;
            Start = start;
            End = end;
        }
    }

    readonly record struct SymbolAndPosition
    {
        public readonly char Value;
        public readonly (int Row, int Column) Position;

        public SymbolAndPosition(char value, (int Row, int Column) position)
        {
            Value = value;
            Position = position;
        }
    }
}
