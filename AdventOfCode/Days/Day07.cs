namespace AdventOfCode.Days;

public class Day07
{
    private readonly string _input;

    public Day07()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\07.txt");
    }

    private static readonly Dictionary<char, int> CardValues = new Dictionary<char, int>
    {
        {'A', 14}, {'K', 13}, {'Q', 12}, {'J', 11}, {'T', 10},
        {'9', 9}, {'8', 8}, {'7', 7}, {'6', 6}, {'5', 5},
        {'4', 4}, {'3', 3}, {'2', 2}
    };

    private static readonly Dictionary<char, int> CardValuesJoker = new Dictionary<char, int>
    {
        {'A', 14}, {'K', 13}, {'Q', 12}, {'J', 1}, {'T', 10},
        {'9', 9}, {'8', 8}, {'7', 7}, {'6', 6}, {'5', 5},
        {'4', 4}, {'3', 3}, {'2', 2}
    };

    private static bool isPart2 = false;

    public long Part1()
    {
        var inputs = _input.Split(Environment.NewLine);
        long sum = 0;

        var fiveOfAKind = new List<string>();//AAAAA one letter
        var fourOfAKind = new List<string>();//AAAA3 two letters
        var fullHouse = new List<string>();//AAA22 two letters
        var threeOfAKind = new List<string>();//AAA23 three letters
        var twoPair = new List<string>();//AA223 three letters
        var onePair = new List<string>();//AA234 four letters
        var highCard = new List<string>();//A2345 five letters

        FillLists(inputs, fiveOfAKind, fourOfAKind, fullHouse, threeOfAKind, twoPair, onePair, highCard);

        fourOfAKind.Sort(CompareHands);
        fullHouse.Sort(CompareHands);
        threeOfAKind.Sort(CompareHands);
        twoPair.Sort(CompareHands);
        onePair.Sort(CompareHands);
        highCard.Sort(CompareHands);
        sum = GetSum(sum, fiveOfAKind, fourOfAKind, fullHouse, threeOfAKind, twoPair, onePair, highCard);

        return sum;
    }

    public long Part2()
    {
        var inputs = _input.Split(Environment.NewLine);
        long sum = 0;
        isPart2 = true;

        var fiveOfAKind = new List<string>();//AAAAA one letter
        var fourOfAKind = new List<string>();//AAAA3 two letters
        var fullHouse = new List<string>();//AAA22 two letters
        var threeOfAKind = new List<string>();//AAA23 three letters
        var twoPair = new List<string>();//AA223 three letters
        var onePair = new List<string>();//AA234 four letters
        var highCard = new List<string>();//A2345 five letters

        var inputsWithoutJoker = inputs.Where(i => !i.Contains('J')).ToArray();
        var inputsWithJoker = inputs.Where(i => i.Contains('J')).ToArray();

        FillLists(inputsWithoutJoker, fiveOfAKind, fourOfAKind, fullHouse, threeOfAKind, twoPair, onePair, highCard);
        FillListsWithJokers(inputsWithJoker, fiveOfAKind, fourOfAKind, fullHouse, threeOfAKind, twoPair, onePair, highCard);

        fiveOfAKind.Sort(CompareHands);
        fourOfAKind.Sort(CompareHands);
        fullHouse.Sort(CompareHands);
        threeOfAKind.Sort(CompareHands);
        twoPair.Sort(CompareHands);
        onePair.Sort(CompareHands);
        highCard.Sort(CompareHands);

        sum = GetSum(sum, fiveOfAKind, fourOfAKind, fullHouse, threeOfAKind, twoPair, onePair, highCard);

        return sum;
    }//254412181

    private void FillListsWithJokers(string[] inputsWithJoker, List<string> fiveOfAKind, List<string> fourOfAKind, List<string> fullHouse, List<string> threeOfAKind, List<string> twoPair, List<string> onePair, List<string> highCard)
    {
        foreach (var input in inputsWithJoker)
        {
            var letters = GetOccurrencesOfCharacters(input.Split(" ")[0]);
            if (letters.Count is 1)
            {
                fiveOfAKind.Add(input);
            }

            if (letters.Count is 2)
            {
                fiveOfAKind.Add(input);
            }

            if (letters.Count is 3)
            {
                if (letters.Values.Any(v => v is 3))
                {
                    fourOfAKind.Add(input);
                }
                else if (letters.Values.Any(v => v is 2) && letters.TryGetValue('J', out var js) && js is 2)
                {
                    fourOfAKind.Add(input);
                }
                else
                {
                    fullHouse.Add(input);
                }
            }

            if (letters.Count is 4)
            {
                threeOfAKind.Add(input);
            }

            if (letters.Count is 5)
            {
                onePair.Add(input);
            }
        }
    }

    private static void FillLists(string[] inputs,
        List<string> fiveOfAKind,
        List<string> fourOfAKind,
        List<string> fullHouse,
        List<string> threeOfAKind,
        List<string> twoPair,
        List<string> onePair,
        List<string> highCard)
    {
        foreach (var input in inputs)
        {
            var letters = GetOccurrencesOfCharacters(input.Split(" ")[0]);

            if (letters.Count is 1)
            {
                fiveOfAKind.Add(input);
            }
            if (letters.Count is 2)
            {
                if (letters.Values.Any(v => v is 4))
                {
                    fourOfAKind.Add(input);
                }
                else
                {
                    fullHouse.Add(input);
                }
            }
            if (letters.Count is 3)
            {
                if (letters.Values.Any(v => v is 3))
                {
                    threeOfAKind.Add(input);
                }
                else
                {
                    twoPair.Add(input);
                }
            }
            if (letters.Count is 4)
            {
                onePair.Add(input);
            }
            if (letters.Count is 5)
            {
                highCard.Add(input);
            }
        }
    }

    private static int CompareHands(string hand1, string hand2)
    {
        if (isPart2)
        {
            CardValues['J'] = 1;
        }

        for (int i = 0; i < hand1.Length; i++)
        {
            if (hand1[i] != hand2[i])
            {
                return CardValues[hand2[i]].CompareTo(CardValues[hand1[i]]);
            }
        }

        return 0;
    }

    private static long GetSum(long sum, List<string> fiveOfAKind, List<string> fourOfAKind, List<string> fullHouse, List<string> threeOfAKind, List<string> twoPair, List<string> onePair, List<string> highCard)
    {
        var rank = 1000;

        foreach (var hand in fiveOfAKind)
        {
            sum += rank * int.Parse(hand.Split(" ")[1]);
            rank--;
        }

        foreach (var hand in fourOfAKind)
        {
            sum += rank * int.Parse(hand.Split(" ")[1]);
            rank--;
        }

        foreach (var hand in fullHouse)
        {
            sum += rank * int.Parse(hand.Split(" ")[1]);
            rank--;
        }

        foreach (var hand in threeOfAKind)
        {
            sum += rank * int.Parse(hand.Split(" ")[1]);
            rank--;
        }

        foreach (var hand in twoPair)
        {
            sum += rank * int.Parse(hand.Split(" ")[1]);
            rank--;
        }

        foreach (var hand in onePair)
        {
            sum += rank * int.Parse(hand.Split(" ")[1]);
            rank--;
        }

        foreach (var hand in highCard)
        {
            sum += rank * int.Parse(hand.Split(" ")[1]);
            rank--;
        }

        return sum;
    }

    public static Dictionary<char, int> GetOccurrencesOfCharacters(string text)
    {
        var dictionary = new Dictionary<char, int>();

        foreach (var character in text)
        {
            if (dictionary.TryGetValue(character, out var value))
            {
                dictionary[character] = value + 1;
            }
            else
            {
                dictionary.Add(character, 1);
            }
        }

        return dictionary;
    }
}