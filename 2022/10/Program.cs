var input = @"noop
addx 26
addx -21
addx 2
addx 3
noop
noop
addx 23
addx -17
addx -1
noop
noop
addx 7
noop
addx 3
addx 1
noop
noop
addx 2
noop
addx 7
noop
addx -12
addx 13
addx -38
addx 5
addx 34
addx -2
addx -29
addx 2
addx 5
addx 2
addx 3
addx -2
addx -1
addx 8
addx 2
addx 6
addx -26
addx 23
addx -26
addx 33
addx 2
addx -37
addx -1
addx 1
noop
noop
noop
addx 5
addx 5
addx 3
addx -2
addx 2
addx 5
addx 5
noop
noop
addx -2
addx 4
noop
noop
noop
addx 3
noop
noop
addx 7
addx -1
addx -35
addx -1
addx 5
addx 3
noop
addx 4
noop
noop
noop
noop
noop
addx 5
addx 1
noop
noop
noop
addx -7
addx 12
addx 2
addx 7
noop
addx -2
noop
noop
addx 7
addx 2
addx -39
noop
noop
addx 5
addx 2
addx -4
addx 25
addx -18
addx 7
noop
addx -2
addx 5
addx 2
addx 6
addx -5
addx 2
addx -22
addx 29
addx -21
addx -7
addx 31
addx 2
noop
addx -36
addx 1
addx 5
noop
addx 1
addx 4
addx 5
noop
noop
noop
addx 3
noop
addx -13
addx 15
noop
addx 5
noop
addx 1
noop
addx 3
addx 2
addx 4
addx 3
noop
addx -3
noop";

//input = @"addx 15
//addx -11
//addx 6
//addx -3
//addx 5
//addx -1
//addx -8
//addx 13
//addx 4
//noop
//addx -1
//addx 5
//addx -1
//addx 5
//addx -1
//addx 5
//addx -1
//addx 5
//addx -1
//addx -35
//addx 1
//addx 24
//addx -19
//addx 1
//addx 16
//addx -11
//noop
//noop
//addx 21
//addx -15
//noop
//noop
//addx -3
//addx 9
//addx 1
//addx -3
//addx 8
//addx 1
//addx 5
//noop
//noop
//noop
//noop
//noop
//addx -36
//noop
//addx 1
//addx 7
//noop
//noop
//noop
//addx 2
//addx 6
//noop
//noop
//noop
//noop
//noop
//addx 1
//noop
//noop
//addx 7
//addx 1
//noop
//addx -13
//addx 13
//addx 7
//noop
//addx 1
//addx -33
//noop
//noop
//noop
//addx 2
//noop
//noop
//noop
//addx 8
//noop
//addx -1
//addx 2
//addx 1
//noop
//addx 17
//addx -9
//addx 1
//addx 1
//addx -3
//addx 11
//noop
//noop
//addx 1
//noop
//addx 1
//noop
//noop
//addx -13
//addx -19
//addx 1
//addx 3
//addx 26
//addx -30
//addx 12
//addx -1
//addx 3
//addx 1
//noop
//noop
//noop
//addx -9
//addx 18
//addx 1
//addx 2
//noop
//noop
//addx 9
//noop
//noop
//noop
//addx -1
//addx 2
//addx -37
//addx 1
//addx 3
//noop
//addx 15
//addx -21
//addx 22
//addx -6
//addx 1
//noop
//addx 2
//addx 1
//noop
//addx -10
//noop
//noop
//addx 20
//addx 1
//addx 2
//addx 2
//addx -6
//addx -11
//noop
//noop
//noop";

//input = @"noop
//addx 3
//addx -5";

var dict = new Dictionary<int, int>();

var cycleNumber = 0;
var xRegister = 1;
foreach(var instruction in input.Split("\r\n"))
{
    if(instruction == "noop")
    {
        cycleNumber += 1;
        dict.Add(cycleNumber, xRegister);
    }

    // assume addx
    var inputs = instruction.Split(' ');
    if (inputs[0] == "addx")
    {
        cycleNumber += 1;
        dict.Add(cycleNumber, xRegister);
        cycleNumber += 1;
        dict.Add(cycleNumber, xRegister);
        var registerNum = int.Parse(inputs[1]);
        xRegister += registerNum;
    }
}

var answer = 20 * dict[20] + 60 * dict[60] + 100 * dict[100] + 140 * dict[140] + 180 * dict[180] + 220 * dict[220];

Console.WriteLine(answer);

var myConsole = new char[6, 40];
for(var i = 0; i < myConsole.GetLength(0); i ++)
{
    for (var j = 0; j < myConsole.GetLength(1); j++)
    {
        myConsole[i, j] = '.';
    }
}

// logic
foreach(var item in dict)
{
    var yCoord = (item.Key - 1) / 40;
    var spritePositions = new int[] { item.Value - 1, item.Value, item.Value + 1 };
    var crtLoc = (item.Key - 1) - (40 * yCoord);
    if (spritePositions.Contains(crtLoc))
    {
        myConsole[yCoord, crtLoc] = '#';
    }
}

for (int i = 0; i < myConsole.GetLength(0); i++)
{
    for (int j = 0; j < myConsole.GetLength(1); j++)
    {
        Console.Write(myConsole[i, j]);
    }
    Console.WriteLine();
}