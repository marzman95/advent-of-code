using AoCHelpers;
using System.Text.RegularExpressions;

ConsoleHelper.SetDayStart(3);
ConsoleHelper.SetSectionStart("Part 1");

// ---------------------------------------------
string rawInput = FileHelper.GetStringFromFile("input.txt");

Regex regProcessor = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
ConsoleHelper.WriteInline($"Processing input against: ");
ConsoleHelper.Write($"{regProcessor.ToString()}", ConsoleColor.Cyan, true);

List<string> mulCommands = new List<string>();
int sum = 0;

//ConsoleHelper.WriteSeparator(ConsoleColor.DarkBlue);
//ConsoleHelper.Write(" Calculations table", ConsoleColor.DarkGreen);
//ConsoleHelper.WriteSeparator(ConsoleColor.DarkBlue);
//ConsoleHelper.Write("| command      | d1  | d2  ||  mul   ||    sum     |", ConsoleColor.DarkBlue, true);
//ConsoleHelper.WriteSeparator(ConsoleColor.DarkBlue, true);

foreach (Match match in regProcessor.Matches(rawInput))
{
    //ConsoleHelper.WriteInline("| ", ConsoleColor.DarkBlue, true);
    
    string command = match.Value;
    mulCommands.Add(command);
    //ConsoleHelper.WriteInline(centeredString(command, 12), ConsoleColor.DarkMagenta, true);
    //ConsoleHelper.WriteInline(" | ", ConsoleColor.DarkBlue, true);

    List<int> numbers = GetNumbersCommand(command);
    //ConsoleHelper.WriteInline(centeredString(numbers[0].ToString(), 3), ConsoleColor.Magenta, true);
    //ConsoleHelper.WriteInline(" | ", ConsoleColor.DarkBlue, true);
    //ConsoleHelper.WriteInline(centeredString(numbers[1].ToString(), 3), ConsoleColor.Magenta, true);
    //ConsoleHelper.WriteInline(" || ", ConsoleColor.DarkBlue, true);

    int multiple = numbers[0] * numbers[1];
    //ConsoleHelper.WriteInline(centeredString(multiple.ToString(), 6), ConsoleColor.DarkGreen, true);
    //ConsoleHelper.WriteInline(" | ", ConsoleColor.DarkBlue, true);
    sum += multiple;
    //ConsoleHelper.WriteInline(centeredString(sum.ToString(), 10), ConsoleColor.DarkGreen, true);
    //ConsoleHelper.WriteInline("|\n", ConsoleColor.DarkBlue, true);
}

ConsoleHelper.WriteInline($"Found ");
ConsoleHelper.WriteInline($"{mulCommands.Count}", ConsoleColor.Cyan, true);
ConsoleHelper.Write(" multiplication commands!");

ConsoleHelper.WriteInline("Total sum: ", ConsoleColor.DarkGreen, true);
ConsoleHelper.Write(sum.ToString(), ConsoleColor.Green, true);

ConsoleHelper.SetSectionStart("Part 2");

// ---------------------------------------------

List<string> doCalculateSections = new List<string>();
string rawInputWithStart = "do()" + rawInput;
rawInputWithStart = rawInputWithStart.Replace("\n", "");

foreach(Match match in Regex.Matches(rawInputWithStart, @"do\(\)(.*?)don't\(\)")) {
    doCalculateSections.Add(match.Value);
}

ConsoleHelper.WriteInline($"Found ");
ConsoleHelper.WriteInline($"{doCalculateSections.Count}", ConsoleColor.Cyan, true);
ConsoleHelper.Write(" sections!");

List<string> sectionCommands = new List<string>();
int sectionsSum = 0;

ConsoleHelper.WriteSeparator(ConsoleColor.DarkBlue);
ConsoleHelper.Write(" Calculations table", ConsoleColor.DarkGreen);
ConsoleHelper.WriteSeparator(ConsoleColor.DarkBlue);
ConsoleHelper.Write("| command      | d1  | d2  ||  mul   ||    sum     |", ConsoleColor.DarkBlue, true);
ConsoleHelper.WriteSeparator(ConsoleColor.DarkBlue, true);

foreach (string section in doCalculateSections) {
    foreach (Match match in Regex.Matches(section, @"mul\(\d{1,3},\d{1,3}\)")) {
        ConsoleHelper.WriteInline("| ", ConsoleColor.DarkBlue, true);

        string command = match.Value;
        sectionCommands.Add(command);
        ConsoleHelper.WriteInline(centeredString(command, 12), ConsoleColor.DarkMagenta, true);
        ConsoleHelper.WriteInline(" | ", ConsoleColor.DarkBlue, true);

        List<int> numbers = GetNumbersCommand(command);
        ConsoleHelper.WriteInline(centeredString(numbers[0].ToString(), 3), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(" | ", ConsoleColor.DarkBlue, true);
        ConsoleHelper.WriteInline(centeredString(numbers[1].ToString(), 3), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(" || ", ConsoleColor.DarkBlue, true);

        int multiple = numbers[0] * numbers[1];
        ConsoleHelper.WriteInline(centeredString(multiple.ToString(), 6), ConsoleColor.DarkGreen, true);
        ConsoleHelper.WriteInline(" | ", ConsoleColor.DarkBlue, true);
        sectionsSum += multiple;
        ConsoleHelper.WriteInline(centeredString(sectionsSum.ToString(), 10), ConsoleColor.DarkGreen, true);
        ConsoleHelper.WriteInline("|\n", ConsoleColor.DarkBlue, true);
    }
    ConsoleHelper.WriteSeparator(ConsoleColor.DarkGray);
}

ConsoleHelper.WriteInline($"Found ");
ConsoleHelper.WriteInline($"{sectionCommands.Count}", ConsoleColor.Cyan, true);
ConsoleHelper.Write(" multiplication sectioned commands!");

ConsoleHelper.WriteInline("Total sections sum: ", ConsoleColor.DarkGreen, true);
ConsoleHelper.Write(sectionsSum.ToString(), ConsoleColor.Green, true);

int ProcessMulCmd(string cmd)
{
    List<int> numbers = new List<int>();
    foreach (Match match in Regex.Matches(cmd, "\\d{1,3}"))
    {
        numbers.Add(int.Parse(match.Value));
    }

    if (numbers.Count != 2)
    {
        throw new ArgumentOutOfRangeException($"Command contained {numbers.Count.ToString()} numbers!");
    }

    return numbers[0] * numbers[1];
}

List<int> GetNumbersCommand(string cmd)
{
    List<int> numbers = new List<int>();
    foreach (Match match in Regex.Matches(cmd, "\\d{1,3}"))
    {
        numbers.Add(int.Parse(match.Value));
    }

    if (numbers.Count != 2)
    {
        throw new ArgumentOutOfRangeException($"Command contained {numbers.Count.ToString()} numbers!");
    }

    return numbers;
}

// -----------------
// - mul table
// -----------------
// |              | d1  | d2  ||  mul   ||    sum     |
// ----------------------------------------------------
// | mul(ddd,ddd) | ddd | ddd || dddddd || dddddddddd |

static string centeredString(string s, int width)
{
    if (s.Length >= width)
    {
        return s;
    }

    int leftPadding = (width - s.Length) / 2;
    int rightPadding = width - s.Length - leftPadding;

    return new string(' ', leftPadding) + s + new string(' ', rightPadding);
}