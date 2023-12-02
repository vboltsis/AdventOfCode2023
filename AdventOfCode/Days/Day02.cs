namespace AdventOfCode.Days;

internal class Day02
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\02.txt");
    }

    public int Part1()
    {
        var inputs = _input.Split(Environment.NewLine).ToList();
        var splitGames = inputs.Select(inputs => inputs.Split(';')).ToList();
        var sum = 0;
        var cubeDictionary = new Dictionary<string, int>
        {
            {
                "red", 12
            },
            {
                "green", 13
            },
            {
                "blue", 14
            }
        };

        foreach (var games in splitGames)
        {
            int gameName = 0;
            foreach (var game in games)
            {
                if (game.Contains(':'))
                {
                    var splitGame = game.Split(':');
                    gameName = int.Parse(splitGame[0].Split(' ')[1]);

                    var cubes = splitGame[1].Split(',');

                    foreach (var cube in cubes)
                    {
                        var cubeSplit = cube.Trim().Split(' ');

                        var cubeNumber = int.Parse(cubeSplit[0]);
                        
                        var color = cubeSplit[1];

                        if (cubeDictionary.TryGetValue(color, out int value))
                        {
                            if (cubeNumber > value)
                            {
                                goto nextGame;
                            }
                        }
                    }
                }
                else
                {
                    var cubes = game.Split(',');

                    foreach (var cube in cubes)
                    {
                        var cubeSplit = cube.Trim().Split(' ');

                        var cubeNumber = int.Parse(cubeSplit[0]);

                        var color = cubeSplit[1];

                        if (cubeDictionary.TryGetValue(color, out int value))
                        {
                            if (cubeNumber > value)
                            {
                                goto nextGame;
                            }
                        }
                    }
                }
            }

            sum += gameName;

            nextGame:
                continue;
        }

        return sum;
    }

    public int Part2()
    {
        var inputs = _input.Split(Environment.NewLine).ToList();
        var splitGames = inputs.Select(inputs => inputs.Split(';')).ToList();
        var sum = 0;

        foreach (var games in splitGames)
        {
            int gameName = 0;
            var red = 0;
            var green = 0;
            var blue = 0;

            foreach (var game in games)
            {
                if (game.Contains(':'))
                {
                    var splitGame = game.Split(':');
                    gameName = int.Parse(splitGame[0].Split(' ')[1]);

                    var cubes = splitGame[1].Split(',');

                    foreach (var cube in cubes)
                    {
                        var cubeSplit = cube.Trim().Split(' ');

                        var cubeNumber = int.Parse(cubeSplit[0]);

                        var color = cubeSplit[1];

                        switch (color)
                        {
                            case "red":
                                if (red < cubeNumber)
                                {
                                    red = cubeNumber;
                                }
                                break;
                            case "green":
                                if (green < cubeNumber)
                                {
                                    green = cubeNumber;
                                }
                                break;
                            case "blue":
                                if (blue < cubeNumber)
                                {
                                    blue = cubeNumber;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    var cubes = game.Split(',');

                    foreach (var cube in cubes)
                    {
                        var cubeSplit = cube.Trim().Split(' ');

                        var cubeNumber = int.Parse(cubeSplit[0]);

                        var color = cubeSplit[1];

                        switch (color)
                        {
                            case "red":
                                if (red < cubeNumber)
                                {
                                    red = cubeNumber;
                                }
                                break;
                            case "green":
                                if (green < cubeNumber)
                                {
                                    green = cubeNumber;
                                }
                                break;
                            case "blue":
                                if (blue < cubeNumber)
                                {
                                    blue = cubeNumber;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            var multOfCubes = red * green * blue;
            sum += multOfCubes;
        }

        return sum;
    }
}
