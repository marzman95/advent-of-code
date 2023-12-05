ConsoleColor startColor = Console.ForegroundColor;
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("This is a Advent Of Code solution application created by:");
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Marcin van de Ven (@marzman95)");
Console.ForegroundColor = startColor;
Console.WriteLine("----------------------------------------------");

string inputFile = $"{{INPUTFILE}}";
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
Console.WriteLine($"Read {lines.Count} lines of input from {_stream.Name}");

foreach (string line in lines)
{
    int gameId = GetGameId(line);
    string[] draws = ExtractDraws(line);
    bool gameValid = true;
    foreach (string draw in draws)
    {
        List<KeyValuePair<string, int>> CubeSets = ExtractCubes(draw);
        foreach (KeyValuePair<string, int> cubeSet in CubeSets)
        {
            if (!VerifyColor(cubeSet.Key, cubeSet.Value))
            {
                // False, game not possible
                gameValid = false;
                Console.WriteLine($"Game {gameId} is not valid!");
                break;
            }
        }
        if (!gameValid)
        {
            break;
        }
    }
    if (gameValid)
    {
        totalScore += gameId;
        Console.WriteLine($"Game {gameId} is valid, making the total of valid games: {totalScore}");
    }
}

// for each line
//  extract each play/draw by ";"
//  for each draw, extract each color
//      for each color in draw <--
//          check if # cubes is less than given/preset numbers
//          if #cubes_{color} < #guess_{color}
//              possible = true;
//          else
//              possible = false;
//              break of play loop (make play list empty); (console that game is not possible)
//  Game is possible (end of play loop)
//  Add +1 to total score (console that game is possible)
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

bool VerifyColor(string color, int value)
{
    bool result = true;
    switch (color)
    {
        case "red":
            if (value > maxRed)
            {
                result = false;
            }
            break;
        case "green":
            if (value > maxGreen)
            {
                result = false;
            }
            break;
        case "blue":
            if (value > maxBlue)
            {
                result = false;
            }
            break;
    }
    return result;
}

int GetGameId(string line)
{
    int startIndex = line.IndexOf("Game ") + 4;
    int endIndex = line.IndexOf(":");
    string numberString = line.Substring(startIndex, endIndex-startIndex);

    return int.Parse(numberString.Trim());
}