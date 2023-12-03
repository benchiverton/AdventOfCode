var input = File.ReadLines("input.txt");

//input = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
//Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
//Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
//Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
//Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green".Split(Environment.NewLine);

var target = new Dictionary<Colour, int>()
{
    {Colour.Red, 12}, {Colour.Green, 13}, {Colour.Blue, 14}
};

var games = new Dictionary<int, List<Dictionary<Colour, int>>>();
foreach (var game in input)
{
    var gameNumber = int.Parse(string.Concat(game.TakeWhile(c => c != ':').SkipWhile(c => c != ' ')));
    games.Add(gameNumber, new List<Dictionary<Colour, int>>());

    var setstr = string.Concat(game.SkipWhile(c => c != ':').Skip(1));
    var sets = setstr.Split(';');
    var setResults = sets.Select(s => s.Split([',', ';']).Select(v => v.Trim()).Select(v => v.Split(' ')).Select(v => (Enum.Parse<Colour>(v[1], true), int.Parse(v[0]))));
    foreach (var results in setResults)
    {
        var total = new Dictionary<Colour, int>() {
            {Colour.Red, 0}, {Colour.Green, 0}, {Colour.Blue, 0}
        };
        foreach (var r in results)
        {
            total[r.Item1] += r.Item2;
        }
        games[gameNumber].Add(total);
    }
}

var result = games.Where(g => g.Value.All(r => r[Colour.Red] <= target[Colour.Red] && r[Colour.Green] <= target[Colour.Green] && r[Colour.Blue] <= target[Colour.Blue])).Sum(r => r.Key);
Console.WriteLine(result);

var result2 = games.Select(g => g.Value.Select(r => r[Colour.Green]).Max() * g.Value.Select(r => r[Colour.Red]).Max() * g.Value.Select(r => r[Colour.Blue]).Max()).Sum();
Console.WriteLine(result2);

enum Colour
{
    Red,
    Green,
    Blue
}