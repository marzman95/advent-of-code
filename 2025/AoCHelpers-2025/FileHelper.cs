namespace AoCHelpers
{
    public class FileHelper
    {
        public static string GetStringFromFile(string filePath)
        {
            ConsoleHelper.Write($"Opening file: ", ConsoleColor.Blue, false, false);
            ConsoleHelper.Write(filePath, ConsoleColor.Cyan, true, false);
            string fileContent = File.ReadAllText(filePath);
            ConsoleHelper.Write(" with ", ConsoleColor.Blue, true, false);
            ConsoleHelper.Write(fileContent.Length.ToString(), ConsoleColor.Cyan, true, false);
            ConsoleHelper.Write(" characters processed!");
            return fileContent;
        }

        public static List<string> GetLinesFromFile(string filePath)
        {
            ConsoleHelper.Write($"Opening file: ", ConsoleColor.Blue, false, false);
            ConsoleHelper.Write(filePath, ConsoleColor.Cyan, true, false);
            string[] fileContent = File.ReadAllLines(filePath);
            ConsoleHelper.Write(" with ", ConsoleColor.Blue, true, false);
            ConsoleHelper.Write(fileContent.Length.ToString(), ConsoleColor.Cyan, true, false);
            ConsoleHelper.Write(" lines processed!");
            return [.. fileContent];
        }

        public static List<string> SeparateLineToStrings(string lineContent, bool printProcessed = true, string separator = " ")
        {
            string[] result = lineContent.Split(separator);

            if (printProcessed)
            {
                ConsoleHelper.Write("Processed ", ConsoleColor.Blue, false, false);
                ConsoleHelper.Write(result.Length.ToString(), ConsoleColor.Cyan, true, false);
                ConsoleHelper.Write(" strings into separate list!");
            }

            return [.. result];
        }

        public static List<int> SeparateLineToInt(string lineContent, bool printProcessed = true, string separator = " ")
        {
            List<int> result = new List<int>();
            List<string> strings = SeparateLineToStrings(lineContent, printProcessed, separator);
            foreach (string s in strings)
            {
                if (int.TryParse(s, out int i))
                {
                    result.Add(i);
                }
                else
                {
                    ConsoleHelper.WriteParseError($"Failed parsing {s} into integer in FileHelper!");
                    break;
                }
            }

            if (printProcessed)
            {
                ConsoleHelper.Write("Processed ", ConsoleColor.Blue, false, false);
                ConsoleHelper.Write(result.Count.ToString(), ConsoleColor.Cyan, true, false);
                ConsoleHelper.Write(" into integer list!");
            }

            return result;
        }

        public static void ExportStringToFile(string content, string filePath)
        {
            ConsoleHelper.WriteInline("Writing to: ");
            ConsoleHelper.WriteInline(filePath, ConsoleColor.Cyan, true);
            File.WriteAllText(filePath, content);
            ConsoleHelper.Write(" ... string written!");
        }

        public static List<char> LineToCharList(string lineContent, bool printProcessed = true)
        {
            List<char> result = new List<char>();
            result = lineContent.ToCharArray().ToList();
            if (printProcessed)
            {
                ConsoleHelper.Write("Processed ", ConsoleColor.Blue, false, false);
                ConsoleHelper.Write(result.Count.ToString(), ConsoleColor.Cyan, true, false);
                ConsoleHelper.Write(" characters into character list!");
            }
            return result;
        }
    }
}
