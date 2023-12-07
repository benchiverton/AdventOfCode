using System.Numerics;

var input = File.ReadLines("input.txt").ToList();

//input = @"seeds: 79 14 55 13

//seed-to-soil map:
//50 98 2
//52 50 48

//soil-to-fertilizer map:
//0 15 37
//37 52 2
//39 0 15

//fertilizer-to-water map:
//49 53 8
//0 11 42
//42 0 7
//57 7 4

//water-to-light map:
//88 18 7
//18 25 70

//light-to-temperature map:
//45 77 23
//81 45 19
//68 64 13

//temperature-to-humidity map:
//0 69 1
//1 0 69

//humidity-to-location map:
//60 56 37
//56 93 4".Split(Environment.NewLine);


//var seeds = new string(input[0].SkipWhile(i => !char.IsDigit(i)).ToArray()).Split(' ').Select(i => BigInteger.Parse(i)).ToList();
var newSeeds = new string(input[0].SkipWhile(i => !char.IsDigit(i)).ToArray()).Split(' ').Select(i => BigInteger.Parse(i)).ToList();
var seeds = new List<(BigInteger start, BigInteger end)>();
for (var i = 0; i < newSeeds.Count(); i += 2)
{
    seeds.Add((newSeeds[i], newSeeds[i] + newSeeds[i + 1]));
    //for (var j = 0; j < newSeeds[i + 1]; j++)
    //{
    //    seeds.Add(newSeeds[i] + j);
    //}
}

var maps = new Dictionary<RangeType, List<Map>>();
var mapSection = string.Join(Environment.NewLine, input.Skip(2));
var mapSections = mapSection.Split(Environment.NewLine + Environment.NewLine);
foreach (var section in mapSections)
{
    var lines = section.Split(Environment.NewLine);
    var rangeType = GetRangeType(lines[0].Split(' ')[0]);
    maps.Add(rangeType, new List<Map>());
    foreach (var line in lines.Skip(1))
    {
        var values = line.Split(' ').Select(i => BigInteger.Parse(i)).ToList();
        maps[rangeType].Add(new Map(values[0], values[1], values[2]));
    }
}

var results = new Dictionary<(BigInteger start, BigInteger end), Dictionary<RangeType, List<(BigInteger start, BigInteger end)>>>();

foreach (var seed in seeds)
{
    var output = new Dictionary<RangeType, List<(BigInteger start, BigInteger end)>>();
    foreach (var map in maps)
    {
        var result = new List<(BigInteger, BigInteger)>();
        List<(BigInteger start, BigInteger end)> inp;
        if (output.Any())
        {
            inp = output.Last().Value;
        }
        else
        {
            inp = new List<(BigInteger, BigInteger)> { seed };
        }
        foreach (var i in map.Value)
        {
            foreach (var r in inp)
            {
                var start = r.start < i.SourceRangeStart ? i.SourceRangeStart : r.start;
                var end = r.end > i.SourceRangeStart + i.RangeLength ? i.SourceRangeStart + i.RangeLength : r.end;
                if (end < start)
                {
                    continue;
                }
                result.Add((i.DestRangeStart + (start - i.SourceRangeStart), i.DestRangeStart + (end - i.SourceRangeStart)));
            }
        }
        output.Add(map.Key, result);
    }
    results.Add(seed, output);
}

var answer = results.Min(r => r.Value[RangeType.HumidityToLocation].Min(i => i.start));
Console.WriteLine(answer);

RangeType GetRangeType(string input) => input switch
{
    "seed-to-soil" => RangeType.SeedToSoil,
    "soil-to-fertilizer" => RangeType.SoilToFertilizer,
    "fertilizer-to-water" => RangeType.FertilizerToWater,
    "water-to-light" => RangeType.WaterToLight,
    "light-to-temperature" => RangeType.LightToTemperature,
    "temperature-to-humidity" => RangeType.TemperatureToHumidity,
    "humidity-to-location" => RangeType.HumidityToLocation
};

record Map(BigInteger DestRangeStart, BigInteger SourceRangeStart, BigInteger RangeLength);

enum RangeType
{
    SeedToSoil,
    SoilToFertilizer,
    FertilizerToWater,
    WaterToLight,
    LightToTemperature,
    TemperatureToHumidity,
    HumidityToLocation
}
