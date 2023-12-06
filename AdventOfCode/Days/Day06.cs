namespace AdventOfCode.Days;

internal class Day06
{
    public long Part1()
    {
        var time = new List<long>
        {
            38,94,79,70,
        };
        var distance = new List<long>
        {
            241,1549,1074,1091
        };

        long totalWaysToWin = 1;

        for (int race = 0; race < 4; race++)
        {
            long waysToWin = CountWaysToWin(distance[race], time[race]);
            totalWaysToWin *= waysToWin;
        }

        return totalWaysToWin;
    }

    static long CountWaysToWin(long record, long duration)
    {
        long ways = 0;

        for (var holdTime = 0; holdTime < duration; holdTime++)
        {
            var speed = holdTime;
            var travelTime = duration - holdTime;
            var distance = speed * travelTime;

            if (distance > record)
            {
                ways++;
            }
        }

        return ways;
    }

    public long Part2()
    {
        var time = new List<long>
        {
            38947970
        };
        var distance = new List<long>
        {
            241154910741091
        };

        long totalWays = 1;

        for (int race = 0; race < 1; race++)
        {
            long waysToWin = CountWaysToWin(distance[race], time[race]);
            totalWays *= waysToWin;
        }

        return totalWays;
    }
}
