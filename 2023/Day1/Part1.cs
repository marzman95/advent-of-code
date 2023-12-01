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
int totalSum = 0;

Console.WriteLine($"Reading input from {_stream.Name}");
// Read input lines into memory
for (int i = 0; !_reader.EndOfStream; i++)
{
    lines.Add(_reader.ReadLine());
}
Console.WriteLine($"Read {lines.Count} lines of input from {_stream.Name}");

foreach (string line in lines)
{
    int sumOfLine = GetLineSum(line);
    sums.Add(sumOfLine);
    totalSum += sumOfLine;
    //Console.Write($"{totalSum}");
    //Console.WriteLine();
}

Console.WriteLine($"Calculated {sums.Count} sums, with a total value of {totalSum}");

int GetLineSum(string line)
{
    string firstWordChanged = ChangeFirstDigitWord(line);
    string lineChanged = ChangeLastDigitWord(firstWordChanged);
    char[] chars = lineChanged.ToCharArray();
    int lineValue = 0;
    char[] digits = new char[2];
    digits[0] = '0';
    digits[1] = '0';
    int digit = 0;
    for (int i = 0; i < chars.Length; i++)
    {
        if (int.TryParse(chars[i].ToString(), out digit))
        {
            if (digits[0] == '0')
            {
                digits[0] = char.Parse(digit.ToString());
                digits[1] = char.Parse(digit.ToString());
            }
            else
            {
                digits[1] = char.Parse(digit.ToString());
            }
        }
    }
    lineCount++;
    string sumString = digits[0].ToString() + digits[1].ToString();
    lineValue = int.Parse(sumString);
    Console.WriteLine($"Line {lineCount}: {line} ({lineChanged}) has sum: {digits[0].ToString()} & {digits[1].ToString()}: {lineValue.ToString()}");
    //Console.Write($"{lineCount} - {lineValue} - ");
    return lineValue;
}

string ChangeFirstDigitWord(string line)
{
    int firstWordPosition = 100;
    int firstWordDigit = 0;
    string result = "";

    int startIndex = -1;
    startIndex = line.IndexOf("one");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 1;
    }
    startIndex = line.IndexOf("two");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 2;
    }
    startIndex = line.IndexOf("three");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 3;
    }
    startIndex = line.IndexOf("four");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 4;
    }
    startIndex = line.IndexOf("five");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 5;
    }
    startIndex = line.IndexOf("six");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 6;
    }
    startIndex = line.IndexOf("seven");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 7;
    }
    startIndex = line.IndexOf("eight");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 8;
    }
    startIndex = line.IndexOf("nine");
    if (startIndex <= firstWordPosition && startIndex > -1)
    {
        firstWordPosition = startIndex;
        firstWordDigit = 9;
    }

    switch (firstWordDigit)
    {
        case 1:
            result = line.Replace("one", "o1e");
            break;
        case 2:
            result = line.Replace("two", "t2o");
            break;
        case 3:
            result = line.Replace("three", "t3e");
            break;
        case 4:
            result = line.Replace("four", "f4r");
            break;
        case 5:
            result = line.Replace("five", "f5e");
            break;
        case 6:
            result = line.Replace("six", "s6x");
            break;
        case 7:
            result = line.Replace("seven", "s7n");
            break;
        case 8:
            result = line.Replace("eight", "e8t");
            break;
        case 9:
            result = line.Replace("nine", "n9e");
            break;
    }
    if (result == "")
    {
        return line;
    }
    else
    {
        return result;
    }
}

string ChangeLastDigitWord(string line)
{
    int lastWordPosition = 0;
    int lastWordDigit = 0;
    string result = "";

    int startIndex = -1;
    startIndex = line.LastIndexOf("one");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 1;
    }
    startIndex = line.LastIndexOf("two");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 2;
    }
    startIndex = line.LastIndexOf("three");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 3;
    }
    startIndex = line.LastIndexOf("four");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 4;
    }
    startIndex = line.LastIndexOf("five");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 5;
    }
    startIndex = line.LastIndexOf("six");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 6;
    }
    startIndex = line.LastIndexOf("seven");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 7;
    }
    startIndex = line.LastIndexOf("eight");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 8;
    }
    startIndex = line.LastIndexOf("nine");
    if (startIndex >= lastWordPosition && startIndex > -1)
    {
        lastWordPosition = startIndex;
        lastWordDigit = 9;
    }

    switch (lastWordDigit)
    {
        case 1:
            result = line.Replace("one", "1");
            break;
        case 2:
            result = line.Replace("two", "2");
            break;
        case 3:
            result = line.Replace("three", "3");
            break;
        case 4:
            result = line.Replace("four", "4");
            break;
        case 5:
            result = line.Replace("five", "5");
            break;
        case 6:
            result = line.Replace("six", "6");
            break;
        case 7:
            result = line.Replace("seven", "7");
            break;
        case 8:
            result = line.Replace("eight", "8");
            break;
        case 9:
            result = line.Replace("nine", "9");
            break;
    }

    if (result == "")
    {
        return line;
    }
    else
    {
        return result;
    }
}