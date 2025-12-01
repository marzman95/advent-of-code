using System.Drawing;

namespace AoCHelpers
{
    public class ConsoleHelper
    {
        // Colors
        //      Black:          Unused
        //      DarkBlue:       Indicator 1 (processing)
        //      DarkGreen:      Intermediate (current) good answer (item valid)
        //      DarkCyan:       Debug
        //      DarkRed:        Intermediate (current)  wrong anser (item invalid)
        //      DarkMagenta:    Processing context (line/array)
        //      DarkYellow:     Titles
        //      Gray:           Standard/system
        //      DarkGray:       Line/separator
        //      Blue:           General text
        //      Green:          (Final) answer for exercise
        //      Cyan:           Parameter/counter value
        //      Red:            Error - unfinished termination / Unhandled exception
        //      Magenta:        Process detail (current item within context)
        //      Yellow:         Indicator 2 (general)
        //      White:          Appears same as Gray

        public static void SetDayStart(int day)
        {
            string consoleTitle = $"Advent of Code day {day}";
            string today = $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}";
            string solutionTitle = $"{consoleTitle} (solved on {today})";

            WriteSeparator();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            SetTitle(consoleTitle);
            Console.WriteLine(solutionTitle);
            Write("Solution by Marcin van de Ven (marzman95)");
        }

        public static void SetSectionStart(string sectionName)
        {
            WriteSeparator();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Write(sectionName);
            WriteSeparator(ConsoleColor.DarkGray, true);
            ResetColor();
        }

        public static void SetTitle(string title)
        {
            Console.Title = title;
        }

        public static void Write(string message, ConsoleColor color = ConsoleColor.Blue, bool resetColor = false, bool newline = true)
        {
            if (color != ConsoleColor.Blue)
            {
                Console.ForegroundColor = color;
            }

            // Write message, with or without new line
            if (newline)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }

            // Reset color
            if (resetColor)
            {
                ResetColor();
            }
        }

        public static void WriteInline(string message, ConsoleColor color = ConsoleColor.Blue, bool resetColor = false)
        {
            Write(message, color, resetColor, false);
        }

        public static void WriteSeparator(ConsoleColor color = ConsoleColor.DarkGray, bool doubleLine = false)
        {
            Console.ForegroundColor = color;
            int width = Console.WindowWidth;
            char s = doubleLine ? '=' : '-';
            for (int i = 0; i < width; i++)
            {
                Console.Write(s);
            }
            Console.Write('\n');
            ResetColor();
        }

        public static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public static void WriteParseError(string error)
        {
            Write(error, ConsoleColor.Magenta, true);
        }

        public static void WriteCharToPosition(char c, int x, int y, ConsoleColor color, bool resetColor = true)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(c);
            if (resetColor)
            {
                ResetColor();
            }
        }

        public static void WriteStringToPosition(string s, int x, int y, ConsoleColor color, bool resetColor = true)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(s);
            if (resetColor)
            {
                ResetColor();
            }
        }

        public static string IntListAsString(List<int> list, char separator = ' ')
        {
            string result = "";
            if (list.Count == 0)
            {
                return result;
            }

            foreach (Int128 n in list)
            {
                result = result + n.ToString() + " ";
            }
            return result.Remove(result.Length - 1, 1);
        }

        public static void WriteCharGrid(char[,] grid, int maxHeight, int maxWidth, int topOffset, ConsoleColor color = ConsoleColor.DarkGray, bool resetColor = true)
        {
            for (int y = 0; y < maxHeight; y++)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    char c = grid[x, y];
                    WriteCharToPosition(c, x, y + topOffset, color, false);
                }
            }

            if (resetColor)
            {
                ResetColor();
            }
        }

        public static void WriteImpactCharsGrid(char[,] grid, int maxHeight, int maxWidth, int topOffset, List<char> impactChars, List<ConsoleColor> impactColors, ConsoleColor baseColor = ConsoleColor.DarkGray)
        {
            if (impactChars.Count != impactColors.Count)
            {
                throw new ArgumentException("List of special characters and colors are not equally length!");
            }

            for (int y = 0; y < maxHeight; y++)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    char c = grid[x, y];
                    int cIndex = impactChars.IndexOf(c);
                    ConsoleColor writeColor = baseColor;
                    if (cIndex > -1)
                    {
                        writeColor = impactColors[cIndex];
                    }

                    WriteCharToPosition(c, x, y + topOffset, writeColor);
                }
            }
        }

        public static void UpdateImpactCharOnGrid(char[,] grid, int x, int y, int topOffset, List<char> impactChars, List<ConsoleColor> impactColors, ConsoleColor baseColor = ConsoleColor.DarkGray)
        {
            char c = grid[x, y];
            int cIndex = impactChars.IndexOf(c);
            ConsoleColor writeColor = baseColor;
            if (cIndex > -1)
            {
                writeColor = impactColors[cIndex];
            }

            WriteCharToPosition(c, x, y + topOffset, writeColor);
        }

        public static void GoToNextLine()
        {
            Console.WriteLine();
        }
    }
}
