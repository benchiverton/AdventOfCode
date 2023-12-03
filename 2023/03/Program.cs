var input = File.ReadLines("input.txt").ToList();

//input = @"467..114..
//...*......
//..35..633.
//......#...
//617*......
//.....+.58.
//..592.....
//......755.
//...$.*....
//.664.598..".Split(Environment.NewLine).ToList();

var map = new char[input.Count][];

for (int i = 0; i < input.Count; i++)
{
    map[i] = input[i].ToCharArray();
}

var results = new List<int>();
var gearHits = new List<(int number, int gearLocation)>();
for (var i = 0; i < input.Count; i++)
{
    var hit = false;
    var gearHitLocs = new List<int>();
    var number = 0;
    for (int j = 0; j < input[i].Length; j++)
    {
        var currentChar = input[i][j];
        if (char.IsDigit(currentChar))
        {
            number = number * 10 + int.Parse(currentChar.ToString());
            var adjacentValues = new (int, char?)[]
            {
                (GetLoc(j - 1, i - 1), GetChar(j - 1, i - 1)),
                (GetLoc(j, i - 1), GetChar(j, i - 1)),
                (GetLoc(j + 1, i - 1), GetChar(j + 1, i - 1)),
                (GetLoc(j - 1, i), GetChar(j - 1, i)),
                (GetLoc(j + 1, i), GetChar(j + 1, i)),
                (GetLoc(j - 1, i + 1), GetChar(j - 1, i + 1)),
                (GetLoc(j, i + 1), GetChar(j, i + 1)),
                (GetLoc(j + 1, i + 1), GetChar(j + 1, i + 1)),
            };
            if (adjacentValues.Any(v => IsSymbol(v.Item2)))
            {
                hit = true;
            }
            gearHitLocs.AddRange(adjacentValues.Where(v => v.Item2 == '*').Select(v => v.Item1));
            // boundry
            if (j == input[i].Length - 1)
            {
                if (hit)
                {
                    results.Add(number);
                }
                if (gearHitLocs.Any())
                {
                    gearHits.AddRange(gearHitLocs.Distinct().Select(ghl => (number, ghl)));
                }
                hit = false;
                number = 0; 
                gearHitLocs.Clear();
            }
        }
        else
        {
            if (hit)
            {
                results.Add(number);
            }
            if (gearHitLocs.Any())
            {
                gearHits.AddRange(gearHitLocs.Distinct().Select(ghl => (number, ghl)));
            }
            hit = false;
            gearHitLocs.Clear();
            number = 0;
        }
    }
}

var result = results.Sum();
Console.WriteLine(result);

var result2 = gearHits.GroupBy(g => g.gearLocation).Where(g => g.Count() > 1).Select(g => g.Select(x => x.number)).Select(g => g.Aggregate((current, next) => current * next)).Sum();
Console.WriteLine(result2);

int GetLoc(int x, int y)
{
    return x * 1000 + y;
}

char? GetChar(int x, int y)
{
    // :(
    try
    {
        return map[y][x];
    }
    catch (Exception)
    {
        return null;
    }
}

bool IsSymbol(char? c)
{
    if (c == null)
    {
        return false;
    }
    return !char.IsDigit(c.Value) && c.Value != '.';
}