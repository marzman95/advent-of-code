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
List<int> copiesTempList = new List<int>();


Console.WriteLine($"Reading input from {_stream.Name}");
// Read input lines into memory
for (int i = 0; !_reader.EndOfStream; i++)
{
    lines.Add(_reader.ReadLine());
    copiesTempList[i] = 1;
}
lineCount = lines.Count;
int[] cardCopies = copiesTempList.ToArray();

Console.WriteLine($"Read {lineCount} lines of input from {_stream.Name}");

// add lines to list, count lines, initialize array of copies

// for each line
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
    int matching = 0;
    foreach (var number in havingNumbers)
    {
        
        if (winningNumbers.Contains(number))
        {
            // contains = true:
            //  if matching = 0, set matching to 1
            //  or (points > 0), matching + 1
            if (matching == 0)
            {
                matching = 1;
            }
            else
            {
                matching++;
            }
        }
        // add +1 to copies for the next matching amount
        // IMPLEMENT
    }
    Console.WriteLine($"{line.Substring(0, 7)} has {matching} matches");
}
// sum all numbers in copies array
// IMPLEMENT
Console.WriteLine($"Processed {lineCount} input cards, making a total of {cards}");

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
    for (int i = 0; i < havingNumbersStringList.Length; i++)
    {
        havingNumbers[i] = int.Parse(havingNumbersStringList[i]);
    }
    return havingNumbers;
}