var input = File.ReadLines("input.txt");

//input = @"1abc2
//pqr3stu8vwx
//a1b2c3d4e5f
//treb7uchet".Split(Environment.NewLine);

var map = new Dictionary<string, int>()
{
    { "1", 1 },
    { "2", 2 },
    { "3", 3 },
    { "4", 4 },
    { "5", 5 },
    { "6", 6 },
    { "7", 7 },
    { "8", 8 },
    { "9", 9 },
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

var result = 0;
foreach (var line in input)
{
    var linetemp = line;
    var first = 0;
    var last = 0;
    while (first == 0)
    {
        first = map.FirstOrDefault(m => linetemp.StartsWith(m.Key)).Value;
        if (first == 0)
        {
            linetemp = linetemp[1..];
        }
        else
        {
            break;
        }
    }
    while (true)
    {
        last = map.FirstOrDefault(m => linetemp.EndsWith(m.Key)).Value;
        if (last == 0)
        {
            linetemp = linetemp[..^1];
        }
        else
        {
            break;
        }
    }
    result += int.Parse($"{first}{last}");
}

Console.WriteLine(result);
