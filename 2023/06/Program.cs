var input = File.ReadLines("input.txt").ToList();

//input = @"Time:      7  15   30
//Distance: 9  40  200".Split(Environment.NewLine);

var races = new List<(long allowedTime, long recordDistance)>();
var time = long.Parse(input[0].Split(' ').Where(t => long.TryParse(t, out _)).Aggregate((a, b) => a + b));
var distance = long.Parse(input[1].Split(' ').Where(t => long.TryParse(t, out _)).Aggregate((a, b) => a + b));
races.Add((time, distance));
var results = races.ToDictionary(r => r, r => new List<int>());

foreach (var race in races)
{
    var i = 0;
    while (i < race.allowedTime)
    {
        i++;
        if (i * (race.allowedTime - i) > race.recordDistance)
        {
            results[race].Add(i);
        }
    }
}

var answer = (long) 1;
foreach( var r in results)
{
    answer *= r.Value.Count;
}

Console.WriteLine(answer);
