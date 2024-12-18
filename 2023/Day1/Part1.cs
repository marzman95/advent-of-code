ConsoleColor startColor = Console.ForegroundColor;
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("This is a Advent Of Code solution application created by:");
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Marcin van de Ven (@marzman95)");
Console.ForegroundColor = startColor;
Console.WriteLine("----------------------------------------------");

string inputFile = $"{{PUTINPUTFILEHERE}}";
List<string> lines = new List<string>();
List<int> sums = new List<int>();
FileStream _stream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
StreamReader _reader = new StreamReader(_stream);

int totalSum = 0;

Console.WriteLine($"Reading input from {_stream.Name}");
// Read input lines into memory
for(int i = 0; !_reader.EndOfStream; i++)
{
    lines.Add(_reader.ReadLine());
}
Console.WriteLine($"Read {lines.Count} lines of input from {_stream.Name}");

foreach(string line in lines)
{
    int lineValue = GetLineSum(line);
    Console.WriteLine($"Line: {line} has sum: {lineValue.ToString()}");
    sums.Add(lineValue);
    totalSum += lineValue;
}

Console.WriteLine($"Calculated {sums.Count} sums, with a total value of {totalSum}");

int GetLineSum(string line)
{
    char[] chars = line.ToCharArray();
    int lineValue = 0;
    char[] digits = new char[2];
    int digit = 0;
    for (int i = 0; i < chars.Length; i++)
    {
        if (int.TryParse(chars[i].ToString(), out digit))
        {
            if (digits[0] == 0)
            {
                digits[0] = char.Parse(digit.ToString());
                digits[1] = char.Parse(digit.ToString());
            } else
            {
                digits[1] = char.Parse(digit.ToString());
            }
        }
    }
    
    string sumString = digits[0].ToString() + digits[1].ToString();
    lineValue = int.Parse(sumString);
    
    return lineValue;
}