ConsoleColor startColor = Console.ForegroundColor;
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("This is a Advent Of Code solution application created by:");
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Marcin van de Ven (@marzman95)");
Console.ForegroundColor = startColor;
Console.WriteLine("----------------------------------------------");

string inputFile = $"C:\\Users\\MarcinvandeVen\\Source\\Repos\\marzman95\\advent-of-code\\2023\\Day2\\files\\personalInput.txt";
List<string> lines = new List<string>();
List<int> sums = new List<int>();
FileStream _stream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
StreamReader _reader = new StreamReader(_stream);
int lineCount = 0;
int totalScore = 0;

Console.WriteLine($"Reading input from {_stream.Name}");
// Read input lines into memory
for (int i = 0; !_reader.EndOfStream; i++)
{
    lines.Add(_reader.ReadLine());
}
lineCount = lines.Count;
Console.WriteLine($"Read {lineCount} lines of input from {_stream.Name}");

foreach (string line in lines)
{
    // clean line of single digit numbers
    string cleanedLine = CleanLine(line);
    Console.Write($"Processing {line.Substring(0, 7)}");
    // get list of winning numbers
    int[] winningNumbers = GetListWinningNumbers(cleanedLine);
    // get list of having numbers
    int[] havingNumbers = GetListHavingNumbers(cleanedLine);

    // for each having number, check winning list
    int linePoints = 0;
    foreach (var number in havingNumbers)
    {
        if (winningNumbers.Contains(number))
        {
            if (linePoints == 0)
            {
                linePoints = 1;
            } else
            {
                linePoints = linePoints * 2;
            }
        }
    }
    totalScore += linePoints;
    Console.WriteLine($"{line.Substring(0, 7)} has {linePoints} points, making a total of {totalScore}");
    //  contains = true:
    //      if points = 0, set points 1
    //      or (points > 0), points * 2
    //  contains = false:
    //      continue
    // add points of line to total score
}
Console.WriteLine($"Processed {lineCount} input cards, making a score of {totalScore}");

string CleanLine(string line)
{
    line = line.Replace("  1", " 01");
    line = line.Replace("  2", " 02");
    line = line.Replace("  3", " 03");
    line = line.Replace("  4", " 04");
    line = line.Replace("  5", " 05");
    line = line.Replace("  6", " 06");
    line = line.Replace("  7", " 07");
    line = line.Replace("  8", " 08");
    line = line.Replace("  9", " 09");
    return line;
}

int[] GetListWinningNumbers(string inputLine)
{
    int startWinningNumbers = inputLine.IndexOf(": ") + 2;
    int delimiter = inputLine.IndexOf(" | ");
    string winningNumbersString = inputLine.Substring(startWinningNumbers, (delimiter - startWinningNumbers));
    Console.Write($" winning numbers: {winningNumbersString}");
    string[] winningNumbersStringList = winningNumbersString.Split(" ");
    int[] winningNumbers = new int[winningNumbersStringList.Length];
    for (int i = 0; i < winningNumbersStringList.Length; i++)
    {
        winningNumbers[i] = int.Parse(winningNumbersStringList[i]);
    }
    return winningNumbers;
}

int[] GetListHavingNumbers(string inputLine)
{
    int startHavingNumbers = inputLine.IndexOf(" | ") + 3;
    string havingNumbersString = inputLine.Substring(startHavingNumbers);
    Console.Write($" & having numbers: {havingNumbersString}\n");
    string[] havingNumbersStringList = havingNumbersString.Split(" ");
    int[] havingNumbers = new int[havingNumbersStringList.Length];
    for (int i = 0;i < havingNumbersStringList.Length; i++)
    {
        havingNumbers[i] = int.Parse(havingNumbersStringList[i]);
    }
    return havingNumbers;
}