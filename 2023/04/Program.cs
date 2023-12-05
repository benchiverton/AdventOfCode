var input = File.ReadLines("input.txt").ToList();

//input = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
//Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
//Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
//Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
//Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
//Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11".Split(Environment.NewLine).ToList();

var scratchCards = new List<(List<int> winning, List<int> draw)>();

foreach(var i in input)
{
    var numbers = i.SkipWhile(i => i != ':').Skip(1);
    var wins = new string(numbers.TakeWhile(n => n != '|').ToArray()).Split(' ').Where(i => i != "").Select(n => int.Parse(n)).ToList();
    var draw = new string(numbers.SkipWhile(n => n != '|').Skip(1).ToArray()).Split(' ').Where(i => i != "").Select(n => int.Parse(n)).ToList();
    scratchCards.Add((wins, draw));
}

//var score = 0;
var score = scratchCards.ToDictionary(sc => sc, sc => 1);
for (var i = 0; i < scratchCards.Count; i++)
{
    var game = scratchCards[i];
    //var gameScore = (int) Math.Pow(2, game.draw.Where(g => game.winning.Contains(g)).Count() - 1;
    var gameScore = game.draw.Where(g => game.winning.Contains(g)).Count();
    for (var j = 0; j < gameScore; j++)
    {
        if(i < scratchCards.Count())
        {
            var extraGame = scratchCards[i + j + 1];
            score[extraGame] += score[game];
        }
    }
}

Console.WriteLine(score.Values.Sum());
