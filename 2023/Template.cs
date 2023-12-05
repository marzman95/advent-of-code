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

Console.WriteLine($"Reading input from {_stream.Name}");
// Read input lines into memory
for (int i = 0; !_reader.EndOfStream; i++)
{
    lines.Add(_reader.ReadLine());
}
Console.WriteLine($"Read {lines.Count} lines of input from {_stream.Name}");

foreach (string line in lines)
{
    totalScore += 1;
}
