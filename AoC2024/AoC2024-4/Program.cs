using AoCHelpers;

ConsoleHelper.SetDayStart(4);
ConsoleHelper.SetSectionStart("Part 1");
string word = "XMAS";

// ---------------------------------------------
List<string> inputLines = FileHelper.GetLinesFromFile("exampleInput.txt");
int length = inputLines.Count;

ConsoleHelper.WriteInline("Creating christmas word search of ");
ConsoleHelper.WriteInline(length.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.Write(" lines.");

ConsoleHelper.WriteSeparator();

char[,] wordSearch = new char[inputLines.Count, inputLines.Count];
char[,] wordCopy = new char[inputLines.Count, inputLines.Count];

for (int y = 0; y < length; y++)
{
    for (int x = 0; x < length; x++)
    {
        wordSearch[y, x] = inputLines[y].ElementAt(x);
        wordCopy[y, x] = '.';
    }
}

int startMatrixLine = Console.CursorTop;
WriteMatrix(startMatrixLine);
ConsoleHelper.GoToNextLine();
ConsoleHelper.WriteSeparator();

int wordsFound = 0;

for (int y = 0; y < length; y++)
{
    for (int x = 0; x < length; x++)
    {
        char current = GetLetterAtPos(x, y);
        if (current == 'X' && FindWord(x, y, "X"))
        {
            wordsFound++;
            ConsoleHelper.WriteInline("Word found at (");
            ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
            ConsoleHelper.WriteInline(", ");
            ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
            ConsoleHelper.WriteInline("). Total count: ");
            ConsoleHelper.Write(wordsFound.ToString(), ConsoleColor.Magenta, true);
        }
    }
}

bool FindWord(int x, int y, string prev)
{
    if (x == 0 || y == 0 || x == length || y == length)
    {
        return false;
    }

    char cur = GetLetterAtPos(x, y);
    if (cur == 'S' && prev == "XMA")
    {
        return true;
    }

    if (word.IndexOf(GetLetterAtPos(x - 1, y - 1)) - word.IndexOf(cur) == 1) {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x - 1, y - 1, prev + cur);
    }
    if (word.IndexOf(GetLetterAtPos(x, y - 1)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x, y - 1, prev + cur);
    }
    if (word.IndexOf(GetLetterAtPos(x + 1, y - 1)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x + 1, y - 1, prev + cur);
    }

    if (word.IndexOf(GetLetterAtPos(x - 1, y)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x - 1, y, prev + cur);
    }
    if (word.IndexOf(GetLetterAtPos(x, y)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x, y, prev + cur);
    }
    if (word.IndexOf(GetLetterAtPos(x + 1, y)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x + 1, y, prev + cur);
    }

    if (word.IndexOf(GetLetterAtPos(x - 1, y + 1)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x - 1, y + 1, prev + cur);
    }
    if (word.IndexOf(GetLetterAtPos(x, y + 1)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x, y + 1, prev + cur);
    }
    if (word.IndexOf(GetLetterAtPos(x + 1, y + 1)) - word.IndexOf(cur) == 1)
    {
        ConsoleHelper.WriteInline("Possible word found at (");
        ConsoleHelper.WriteInline(x.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(", ");
        ConsoleHelper.WriteInline(y.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.Write(").");
        return FindWord(x + 1, y + 1, prev + cur);
    }

    return false;
    /*
     * [x - 1, y - 1]: diagonal left-up
     * [x, y - 1]: above
     * [x + 1, y - 1]: diagonal right-up
     * [x - 1, y]: left
     * [x, y]: current/ski[
     * [x + 1, y]: right
     * [x - 1, y + 1]: diagonal left-below
     * [x, y + 1]: below
     * [x + 1, y + 1]: diagonal right-below
     */
}

char GetLetterAtPos(int x, int y)
{
    return wordSearch[y, x];
}

void WriteMatrix(int startY = 0)
{
    for (int y = 0; y < length; y++)
    {
        for (int x = 0; x < length; x++)
        {
            ConsoleHelper.WriteCharToPosition(GetLetterAtPos(x, y), x, y + startY, ConsoleColor.DarkGray);
        }
    }
}