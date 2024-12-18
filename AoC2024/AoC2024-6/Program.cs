using AoCHelpers;

ConsoleHelper.SetDayStart(6);
ConsoleHelper.SetSectionStart("Part 1");
int sleepTime = 10;

//Console.SetWindowPosition(0, 0);
//Console.SetBufferSize(140, 140);


// ---------------------------------------------
List<string> inputLines = FileHelper.GetLinesFromFile("input.txt");

int lines = inputLines.Count;
int cols = inputLines[0].Length;

// Basic information, which can be processed during transformation to matrix
Position startPosition = new Position(0, 0);
int objectsInitialCount = 0;

char[,] patrolMatrix = new char[inputLines.Count, inputLines.Count];

for (int y = 0; y < inputLines.Count; y++)
{
    for (int x = 0; x < inputLines[y].Length; x++)
    {
        char c = inputLines[y][x];
        if (c.Equals('^'))
        {
            startPosition.SetPosition(y, x, true);
        } else if (c.Equals('#')) {
            objectsInitialCount++;
        }
        patrolMatrix[y, x] = c;
    }
}

// General information line (after transformation to matrix)
ConsoleHelper.WriteInline("Start position: ");
ConsoleHelper.WriteInline($"[{startPosition.line}, {startPosition.column}]", ConsoleColor.Cyan, true);
ConsoleHelper.WriteInline(", size: ");
ConsoleHelper.WriteInline($"[{lines}, {cols}]", ConsoleColor.Cyan, true);
ConsoleHelper.WriteInline(", obstructions: ");
ConsoleHelper.WriteInline(objectsInitialCount.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.WriteInline(", speed: ");
ConsoleHelper.Write($"{sleepTime.ToString()}", ConsoleColor.Cyan, true);
ConsoleHelper.WriteSeparator();

// Current processing information line
// ----------1---------2---------3---------4---------5---------6---------7---------8---------9
// curPos: [yyy, xxx]; lookPos: [yyy, xxx] = c; pos count: xxxx; cycle count: xxxx; hasLeft: T

int cursorInformationLine = Console.GetCursorPosition().Top;
ConsoleHelper.Write("curPos: [yyy, xxx]; lookPos: [yyy, xxx] = c; pos count: xxxx; cycle count: xxxx; hasLeft: T", ConsoleColor.DarkBlue, true);
ConsoleHelper.WriteSeparator(ConsoleColor.DarkCyan, true);


Position curPos = new Position(cursorInformationLine, 9, true);
Position lookPos = new Position(cursorInformationLine, 30, true);
Position charPos = new Position(cursorInformationLine, 42, true);
Position posCountPos = new Position(cursorInformationLine, 56, true);
Position cycleCountPos = new Position(cursorInformationLine, 75, true);
Position hasLeftPos = new Position(cursorInformationLine, 90, true);

// Initializing information line data and fill it into the line vars
Position currentPosition = new Position(0, 0, false);
Position nextPosition = new Position(0, 0, false);
char direction = 'n';
ConsoleColor iLineDataColor = ConsoleColor.Magenta;
ConsoleColor arrowColor = ConsoleColor.DarkYellow;
ConsoleColor lookingColor = ConsoleColor.DarkYellow;
ConsoleColor processedColor = ConsoleColor.Gray;
int uniquePositionCount = 0;
int cycleCount = 0;
bool hasLeft = false;

ConsoleHelper.WriteStringToPosition($"{currentPosition.line.ToString("000")}, {currentPosition.column.ToString("000")}", curPos.column, curPos.line, iLineDataColor, true);
ConsoleHelper.WriteStringToPosition($"{nextPosition.line.ToString("000")}, {nextPosition.column.ToString("000")}", lookPos.column, lookPos.line, iLineDataColor, true);
ConsoleHelper.WriteCharToPosition('?', charPos.column, charPos.line, iLineDataColor, true);
ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
ConsoleHelper.WriteStringToPosition($"{cycleCount.ToString("0000")}", cycleCountPos.column, cycleCountPos.line, iLineDataColor, true);
ConsoleHelper.WriteCharToPosition(hasLeft ? 'T': 'F', hasLeftPos.column, hasLeftPos.line, iLineDataColor, true);


int firstMatrixLine = cursorInformationLine + 2;
for (int y = 0; y < lines; y++)
{
    for (int x = 0; x < cols; x++)
    {
        char c = patrolMatrix[y, x];
        ConsoleHelper.WriteCharToPosition(c, x, firstMatrixLine + y, ConsoleColor.DarkGray);
    }
}

ConsoleHelper.WriteInline("\n");
ConsoleHelper.WriteSeparator(ConsoleColor.DarkCyan, true);
ConsoleHelper.Write("Starting processing of paths");

// Processing loop
currentPosition.SetPosition(startPosition.line, startPosition.column);
ConsoleHelper.WriteCharToPosition('^', currentPosition.column, currentPosition.line + firstMatrixLine, arrowColor);

while(!hasLeft)
{
    ConsoleHelper.WriteStringToPosition($"{currentPosition.line.ToString("000")}, {currentPosition.column.ToString("000")}", curPos.column, curPos.line, iLineDataColor, true);
    switch (direction)
    {
        case 'n':
            if (currentPosition.line - 1 < 0)
            {
                uniquePositionCount++;
                ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                hasLeft = true; break;
            }
            else
            {
                // step up
                nextPosition.SetPosition(currentPosition.line - 1, currentPosition.column);
                char nextChar = patrolMatrix[nextPosition.line, nextPosition.column];
                ConsoleHelper.WriteCharToPosition(nextChar, charPos.column, charPos.line, iLineDataColor, true);
                ConsoleHelper.WriteStringToPosition($"{nextPosition.line.ToString("000")}, {nextPosition.column.ToString("000")}", lookPos.column, lookPos.line, iLineDataColor, true);
                ConsoleHelper.WriteCharToPosition(nextChar, nextPosition.column, nextPosition.line + firstMatrixLine, lookingColor);
                if (nextChar == '#')
                {
                    // Turn and stay in same place
                    ConsoleHelper.WriteCharToPosition('#', nextPosition.column, nextPosition.line + firstMatrixLine, processedColor);
                    direction = 'e';
                    nextPosition.SetPosition(currentPosition.line, currentPosition.column);
                    patrolMatrix[currentPosition.line, currentPosition.column] = '>';
                    ConsoleHelper.WriteCharToPosition('>', currentPosition.column, currentPosition.line + firstMatrixLine, arrowColor);
                }
                else
                {
                    // Write current pos with X, if not yet seen
                    if (nextChar == '.')
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        uniquePositionCount++;
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    } else if (nextChar == 'X') // Write X, but do not count
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    }
                    
                    // Write next pos with ^
                    patrolMatrix[nextPosition.line, nextPosition.column] = '^';
                    ConsoleHelper.WriteCharToPosition('^', nextPosition.column, nextPosition.line + firstMatrixLine, arrowColor);
                }
                break;
            }
        case 'e':
            if (currentPosition.column + 1 >= cols)
            {
                uniquePositionCount++;
                ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                hasLeft = true; break;
            }
            else
            {
                // step left
                nextPosition.SetPosition(currentPosition.line, currentPosition.column + 1);
                char nextChar = patrolMatrix[nextPosition.line, nextPosition.column];
                ConsoleHelper.WriteCharToPosition(nextChar, charPos.column, charPos.line, iLineDataColor, true);
                ConsoleHelper.WriteStringToPosition($"{nextPosition.line.ToString("000")}, {nextPosition.column.ToString("000")}", lookPos.column, lookPos.line, iLineDataColor, true);
                ConsoleHelper.WriteCharToPosition(nextChar, nextPosition.column, nextPosition.line + firstMatrixLine, lookingColor);
                if (nextChar == '#')
                {
                    // Turn and stay in same place
                    ConsoleHelper.WriteCharToPosition('#', nextPosition.column, nextPosition.line + firstMatrixLine, processedColor);
                    direction = 's';
                    nextPosition.SetPosition(currentPosition.line, currentPosition.column);
                    patrolMatrix[currentPosition.line, currentPosition.column] = 'V';
                    ConsoleHelper.WriteCharToPosition('V', currentPosition.column, currentPosition.line + firstMatrixLine, arrowColor);
                }
                else
                {
                    // Write current pos with X, if not yet seen
                    if (nextChar == '.')
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        uniquePositionCount++;
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    }
                    else if (nextChar == 'X') // Write X, but do not count
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    }

                    // Write next pos with ^
                    patrolMatrix[nextPosition.line, nextPosition.column] = '>';
                    ConsoleHelper.WriteCharToPosition('>', nextPosition.column, nextPosition.line + firstMatrixLine, arrowColor);
                }
                break;
            }
        case 's':
            if (currentPosition.line + 1 >= lines)
            {
                uniquePositionCount++;
                ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                hasLeft = true; break;
            }
            else
            {
                // step left
                nextPosition.SetPosition(currentPosition.line + 1, currentPosition.column);
                char nextChar = patrolMatrix[nextPosition.line, nextPosition.column];
                ConsoleHelper.WriteCharToPosition(nextChar, charPos.column, charPos.line, iLineDataColor, true);
                ConsoleHelper.WriteStringToPosition($"{nextPosition.line.ToString("000")}, {nextPosition.column.ToString("000")}", lookPos.column, lookPos.line, iLineDataColor, true);
                ConsoleHelper.WriteCharToPosition(nextChar, nextPosition.column, nextPosition.line + firstMatrixLine, lookingColor);
                if (nextChar == '#')
                {
                    // Turn and stay in same place
                    ConsoleHelper.WriteCharToPosition('#', nextPosition.column, nextPosition.line + firstMatrixLine, processedColor);
                    direction = 'w';
                    nextPosition.SetPosition(currentPosition.line, currentPosition.column);
                    patrolMatrix[currentPosition.line, currentPosition.column] = '<';
                    ConsoleHelper.WriteCharToPosition('<', currentPosition.column, currentPosition.line + firstMatrixLine, arrowColor);
                }
                else
                {
                    // Write current pos with X, if not yet seen
                    if (nextChar == '.')
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        uniquePositionCount++;
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    }
                    else if (nextChar == 'X') // Write X, but do not count
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    }

                    // Write next pos with ^
                    patrolMatrix[nextPosition.line, nextPosition.column] = 'V';
                    ConsoleHelper.WriteCharToPosition('V', nextPosition.column, nextPosition.line + firstMatrixLine, arrowColor);
                }
                break;
            }
        case 'w':
            if (currentPosition.column - 1 < 0)
            {
                uniquePositionCount++;
                ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                hasLeft = true; break;
            }
            else
            {
                // step left
                nextPosition.SetPosition(currentPosition.line, currentPosition.column - 1);
                char nextChar = patrolMatrix[nextPosition.line, nextPosition.column];
                ConsoleHelper.WriteCharToPosition(nextChar, charPos.column, charPos.line, iLineDataColor, true);
                ConsoleHelper.WriteStringToPosition($"{nextPosition.line.ToString("000")}, {nextPosition.column.ToString("000")}", lookPos.column, lookPos.line, iLineDataColor, true);
                ConsoleHelper.WriteCharToPosition(nextChar, nextPosition.column, nextPosition.line + firstMatrixLine, lookingColor);
                if (nextChar == '#')
                {
                    // Turn and stay in same place
                    ConsoleHelper.WriteCharToPosition('#', nextPosition.column, nextPosition.line + firstMatrixLine, processedColor);
                    direction = 'n';
                    nextPosition.SetPosition(currentPosition.line, currentPosition.column);
                    patrolMatrix[currentPosition.line, currentPosition.column] = '^';
                    ConsoleHelper.WriteCharToPosition('^', currentPosition.column, currentPosition.line + firstMatrixLine, arrowColor);
                }
                else
                {
                    // Write current pos with X, if not yet seen
                    if (nextChar == '.')
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        uniquePositionCount++;
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    }
                    else if (nextChar == 'X') // Write X, but do not count
                    {
                        patrolMatrix[currentPosition.line, currentPosition.column] = 'X';
                        ConsoleHelper.WriteCharToPosition('X', currentPosition.column, currentPosition.line + firstMatrixLine, processedColor);
                        ConsoleHelper.WriteStringToPosition($"{uniquePositionCount.ToString("0000")}", posCountPos.column, posCountPos.line, iLineDataColor, true);
                    }

                    // Write next pos with ^
                    patrolMatrix[nextPosition.line, nextPosition.column] = '<';
                    ConsoleHelper.WriteCharToPosition('<', nextPosition.column, nextPosition.line + firstMatrixLine, arrowColor);
                }
                break;
            }
    }

    // Temporary to create a break
    cycleCount++;
    ConsoleHelper.WriteStringToPosition($"{cycleCount.ToString("0000")}", cycleCountPos.column, cycleCountPos.line, iLineDataColor, true);
    //if (cycleCount > 100)
    //{
    //    hasLeft = true;
    //}
    Thread.Sleep(sleepTime);
    currentPosition.SetPosition(nextPosition.line, nextPosition.column);
}

// Reset the cursor
Console.SetCursorPosition(0, firstMatrixLine + lines + 2);


class Position
{
    public int line { get; private set; } = 0;
    public int column { get; private set; } = 0;
    public bool isLocked { get; private set; } = false;

    public Position(int y, int x, bool locked = false)
    {
        line = y;
        column = x;
        isLocked = locked;
    }

    public void SetPosition(int y, int x, bool locked = false)
    {
        SetLine(y);
        SetColumn(x);
        SetLocked(locked);
    }

    public void SetLine(int x)
    {
        if (!isLocked)
        {
            line = x;
        }
    }

    public void SetColumn(int y) {
        if (!isLocked)
        {
            column = y;
        }
    }
    
    public void SetLocked(bool locked)
    {
        if (!isLocked)
        {
            isLocked = locked;
        }
    }
}