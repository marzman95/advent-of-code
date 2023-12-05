ConsoleColor startColor = Console.ForegroundColor;
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("This is a Advent Of Code solution application created by:");
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Marcin van de Ven (@marzman95)");
Console.ForegroundColor = startColor;
Console.WriteLine("----------------------------------------------");

string inputFile = $"C:\\Users\\MarcinvandeVen\\source\\repos\\marzman95\\advent-of-code\\2023\\Day2\\files\\personalInput.txt";
List<string> lines = new List<string>();
List<int> sums = new List<int>();
FileStream _stream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
StreamReader _reader = new StreamReader(_stream);
int lineCount = 0;
int totalScore = 0;

int maxRed = 12;
int maxGreen = 13;
int maxBlue = 14;

Console.WriteLine($"Reading input from {_stream.Name}");
// Read input lines into memory
for (int i = 0; !_reader.EndOfStream; i++)
{
    lines.Add(_reader.ReadLine());
}
lineCount = lines.Count;
Console.WriteLine($"Read {lineCount} lines of input from {_stream.Name}");

foreach (string game in lines)
{
    int gameId = GetGameId(game);
    string[] draws = ExtractDraws(game);
    int highestRed = 0;
    int highestGreen = 0;
    int highestBlue = 0;

    foreach (string draw in draws)
    {
        List<KeyValuePair<string, int>> CubeSets = ExtractCubes(draw);
        foreach (KeyValuePair<string, int> cubeSet in CubeSets)
        {
            if ((cubeSet.Key == "red") && (cubeSet.Value > highestRed))
            {
                highestRed = cubeSet.Value;
            }
            if ((cubeSet.Key == "green") && (cubeSet.Value > highestGreen)) 
            { 
                highestGreen = cubeSet.Value;
            }
            if ((cubeSet.Key == "blue") && (cubeSet.Value > highestBlue))
            {
                highestBlue = cubeSet.Value;
            }
        }
    }
    int power = highestRed * highestGreen * highestBlue;
    totalScore += power;
    Console.WriteLine($"Game {gameId} has power {power}, making the total sum: {totalScore}");
}

// for each line
//  extract each play/draw by ";"
//  set #gameHighest_{color} = 0
//  for each draw in game, extract each color
//      for each color in draw
//          check if # cubes for color is higher than already highest value
//          if #currentAmount_{color} > #gameHighest_{color}
//              #gameHighest_{color} = #currentAmount{color}
//  power = #gameHighest_{colorR} * #gameHightest_{ColorG} * #gameHighest_{ColorB}
//  Add power to total score (console that game is possible)
// Display total score

string[] ExtractDraws(string line)
{
    int startIndexFirstDraw = line.IndexOf(":");
    string newLine = line.Substring(startIndexFirstDraw + 1);
    string[] drawList = newLine.Split(";");
    return drawList;
}

List<KeyValuePair<string, int>> ExtractCubes(string draw)
{
    List<KeyValuePair<string, int>> cubeSet = new List<KeyValuePair<string, int>>();
    draw = draw.Substring(1);
    string[] cubesList = draw.Split(", ");
    foreach (string cube in cubesList)
    {
        string[] couple = cube.Split(' ');
        cubeSet.Add(new KeyValuePair<string, int>(couple[1], int.Parse(couple[0])));
    }
    return cubeSet;
}

int GetGameId(string line)
{
    int startIndex = line.IndexOf("Game ") + 4;
    int endIndex = line.IndexOf(":");
    string numberString = line.Substring(startIndex, endIndex - startIndex);

    return int.Parse(numberString.Trim());
}