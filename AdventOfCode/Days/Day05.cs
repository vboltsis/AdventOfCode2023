namespace AdventOfCode.Days;

internal class Day05
{
    private readonly string _input;

    public Day05()
    {
        _input = File.ReadAllText("C:\\Users\\v.boltsis\\Desktop\\AOC\\AdventOfCode.Template\\AdventOfCode\\Inputs\\05.txt");
    }

    public long Part1()
    {
        var lines = _input.Split(Environment.NewLine).ToList();
        var seeds = lines[0].Split(": ")[1].Split(' ').Select(long.Parse).ToArray();

        var seedToSoilMap = ParseLines(lines[3..35]);
        var soilToFertilizerMap = ParseLines(lines[37..66]);
        var fertilizerToWaterMap = ParseLines(lines[68..103]);
        var waterToLightMap = ParseLines(lines[105..130]);
        var lightToTemperatureMap = ParseLines(lines[132..146]);
        var temperatureToHumidityMap = ParseLines(lines[148..180]);
        var humidityToLocationMap = ParseLines(lines[182..]);

        long location = long.MaxValue;

        foreach (var seed in seeds)
        {
            var soil = GetMapping(seed, seedToSoilMap);
            var fertilizer = GetMapping(soil, soilToFertilizerMap);
            var water = GetMapping(fertilizer, fertilizerToWaterMap);
            var light = GetMapping(water, waterToLightMap);
            var temperature = GetMapping(light, lightToTemperatureMap);
            var humidity = GetMapping(temperature, temperatureToHumidityMap);
            var seedLocation = GetMapping(humidity, humidityToLocationMap);
            location = Math.Min(location, seedLocation);
        }

        return location;
    }

    public long Part2()
    {
        var lines = _input.Split(Environment.NewLine).ToList();
        var seeds = lines[0].Split(": ")[1].Split(' ').Select(long.Parse).ToArray();

        var seedToSoilMap = ParseLines(lines[3..35]);
        var soilToFertilizerMap = ParseLines(lines[37..66]);
        var fertilizerToWaterMap = ParseLines(lines[68..103]);
        var waterToLightMap = ParseLines(lines[105..130]);
        var lightToTemperatureMap = ParseLines(lines[132..146]);
        var temperatureToHumidityMap = ParseLines(lines[148..180]);
        var humidityToLocationMap = ParseLines(lines[182..]);

        var seedRanges = new List<(long Start, long End)>(seeds.Length / 2);

        for (int i = 0; i < seeds.Length; i += 2)
        {
            seedRanges.Add(new ValueTuple<long, long>(seeds[i], seeds[i + 1]));
        }

        long location = long.MaxValue;

        foreach (var (Start, End) in seedRanges)
        {
            for (long i = 0; i < End; i++)
            {
                long seed = Start + i;
                var soil = GetMapping(seed, seedToSoilMap);
                var fertilizer = GetMapping(soil, soilToFertilizerMap);
                var water = GetMapping(fertilizer, fertilizerToWaterMap);
                var light = GetMapping(water, waterToLightMap);
                var temperature = GetMapping(light, lightToTemperatureMap);
                var humidity = GetMapping(temperature, temperatureToHumidityMap);
                var seedLocation = GetMapping(humidity, humidityToLocationMap);
                location = Math.Min(location, seedLocation);
            }
        }

        return location;
    }

    static long GetMapping(long thingToMap, (long destination, long source, long length)[] map)
    {
        foreach (var (destination, source, length) in map)
        {
            if (thingToMap >= source && thingToMap < source + length)
            {
                return destination + (thingToMap - source);
            }
        }

        return thingToMap;
    }

    static (long, long, long)[] ParseLines(List<string> lines)
    {
        return lines
            .Select(x =>
            {
                var parts = x.Split(' ');
                return new ValueTuple<long, long, long>(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
            }).ToArray();
    }
}
