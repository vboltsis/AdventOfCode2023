namespace AdventOfCode.Days;

internal class Day04
{
    private readonly string _input;

    public Day04()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\04.txt");
    }

    public int Part1()
    {
        var lines = _input.Split(Environment.NewLine).ToList();
        var sum = 0;

        foreach (var line in lines)
        {
            var split = line.Split(':');

            var card = split[0].Trim();
            var game = split[1].Trim();

            var splitNumbers = game.Split('|');

            var winningNumbers = splitNumbers[0].TrimEnd().Replace("  ", " ").Split(' ').Select(int.Parse).ToList();
            var myNumbers = splitNumbers[1].TrimStart().Replace("  ", " ").Split(' ').Select(int.Parse).ToList();

            var commonNumbers = winningNumbers.Intersect(myNumbers).ToList();

            if (commonNumbers.Count == 0)
            {
                continue;
            }

            var result = GetPoints(commonNumbers.Count);

            sum += result;
        }

        return sum;
    }

    public int Part2()
    {
        var inputs = _input.Split(Environment.NewLine).ToList();
        var sum = 0;
        var cardCount = new Dictionary<int, int>(198);

        for (int i = 0; i < inputs.Count; i++)
        {
            cardCount[i] = 1;
        }

        foreach (var input in inputs)
        {
            var split = input.Split(':');

            var card = int.Parse(split[0].Replace(" ", "").Split("Card")[1]);
            var game = split[1].Trim();

            var splitNumbers = game.Split('|');

            var winningNumbers = splitNumbers[0].TrimEnd().Replace("  ", " ").Split(' ').Select(int.Parse).ToList();
            var myNumbers = splitNumbers[1].TrimStart().Replace("  ", " ").Split(' ').Select(int.Parse).ToList();

            var commonNumbers = winningNumbers.Intersect(myNumbers).ToList();

            if (commonNumbers.Count == 0)
            {
                continue;
            }

            for (var j = 0; j < commonNumbers.Count && j + card < inputs.Count; ++j)
            {
                cardCount[card + j] += cardCount[card - 1];
            }
        }

        var result = cardCount.Values.Sum();

        return sum;
    }

    public static int GetPoints(int n)
    {
        int result = 0;
        for (int i = 1; i <= n; i++)
        {
            if (result == 0)
            {
                result = 1;
            }
            else
            {
                result *= 2;
            }
        }
        return result;
    }

}
