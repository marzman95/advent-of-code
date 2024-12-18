using AoCHelpers;

ConsoleHelper.SetDayStart(18);
ConsoleHelper.SetSectionStart("Part 1");


Console.SetWindowSize(100, 85);

// ---------------------------------------------
List<string> inputLines = FileHelper.GetLinesFromFile("input.txt");

List<List<int>> bytePositions = new List<List<int>>();

foreach (string line in inputLines)
{
    List<int> inputCoordinates = FileHelper.SeparateLineToInt(line, false, ",");
    bytePositions.Add(inputCoordinates);
}

ConsoleHelper.WriteSeparator(ConsoleColor.Gray, true);

int gridStartHeight = Console.CursorTop;

int dimension = 71;
char[,] memoryGrid = new char[dimension, dimension];

for (int y = 0; y < dimension; y++)
{
    for (int x = 0;  x < dimension; x++)
    {
        memoryGrid[x, y] = '.';
    }
}
ConsoleHelper.WriteCharGrid(memoryGrid, dimension, dimension, gridStartHeight);

ConsoleHelper.GoToNextLine();
ConsoleHelper.WriteSeparator(ConsoleColor.Gray);

List<char> specialChars = new List<char> { '#', 'X', 'O' };
List<ConsoleColor> specialColors = new List<ConsoleColor> { ConsoleColor.Gray, ConsoleColor.DarkRed, ConsoleColor.DarkGreen };

memoryGrid[0, 0] = 'O';
ConsoleHelper.UpdateImpactCharOnGrid(memoryGrid, 0, 0, gridStartHeight, specialChars, specialColors);

for (int i = 0; i < 1024; i++)
{
    List<int> position = bytePositions[i];
    int x = position[0];
    int y = position[1];
    memoryGrid[x, y] = '#';
    ConsoleHelper.UpdateImpactCharOnGrid(memoryGrid, x, y, gridStartHeight, specialChars, specialColors);
}


//ConsoleHelper.WriteImpactCharsGrid(memoryGrid, dimension, dimension, gridStartHeight, specialChars, specialColors);
Console.CursorTop = gridStartHeight + dimension + 1;

