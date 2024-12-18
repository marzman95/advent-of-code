using AoCHelpers;

ConsoleHelper.SetDayStart(2);
ConsoleHelper.SetSectionStart("Part 1");

// ---------------------------------------------
List<string> rawReportLines = FileHelper.GetLinesFromFile("input.txt");

List<List<int>> reports = new List<List<int>>();

foreach (string line in rawReportLines)
{
    reports.Add(FileHelper.SeparateLineToInt(line, false));
}

ConsoleHelper.Write("Processed ", ConsoleColor.Blue, false, false);
ConsoleHelper.Write(reports.Count.ToString(), ConsoleColor.Cyan, true, false);
ConsoleHelper.Write(" lines into reports!\n");

//int safeReports = 0;
//foreach (List<int> report in reports)
//{
//    if (isReportSafe(report))
//    {
//        safeReports++;
//    }
//    //ConsoleHelper.WriteInline(" Current count: ", ConsoleColor.Blue);
//    //ConsoleHelper.WriteInline(safeReports.ToString() + "\n", ConsoleColor.Cyan, true);
//}

//ConsoleHelper.Write("Safe reports (answer part 1): ", ConsoleColor.DarkGreen, false, false);
//ConsoleHelper.Write(safeReports.ToString(), ConsoleColor.Green, true, true);

// ---------------------------------------------
ConsoleHelper.SetSectionStart("Part 2");
int safeDampenedReports = 0;
foreach (List<int> report in reports)
{
    if (isReportSafe(report))
    {
        safeDampenedReports++;
    } else if (isReportDampenedSafe(report))
    {
        safeDampenedReports++;
    }

    ConsoleHelper.WriteInline(" Current count: ", ConsoleColor.Blue);
    ConsoleHelper.WriteInline(safeDampenedReports.ToString() + "\n", ConsoleColor.Cyan, true);
}
ConsoleHelper.Write("Safe dampened reports (answer part 2): ", ConsoleColor.DarkGreen, false, false);
ConsoleHelper.Write(safeDampenedReports.ToString(), ConsoleColor.Green, true, true);

// --------------- Functions

bool isReportSafe(List<int> report)
{
    ConsoleHelper.WriteInline($"Processing report ({report.Count}): ", ConsoleColor.DarkCyan, false);

    int mode = 0; // 0 = unset, 1 = increase, 2 = decrease
    int previousLevel = -1;
    bool isSafe = true;


    foreach (int level in report)
    {
        // First level to check, skip to next, set previous
        if (previousLevel == -1)
        {
            ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.Gray, true);
            previousLevel = level;
            continue;
        }

        // Skip if we know unsafe already
        if (!isSafe)
        {
            ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.DarkRed, true);
            previousLevel = level;
            continue;
        }

        // Level is same as previous, no increase/decrease (diff = 0), unsafe
        if (level == previousLevel)
        {
            // Unsafe, as diff cannot be 0, go to next
            ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.DarkRed, true);
            isSafe = false;
            previousLevel = level;
            continue;
        }

        // Increasing
        if (level > previousLevel && (mode == 1 || mode == 0))
        {
            mode = 1;

            int diff = Math.Abs(level - previousLevel);
            if (diff > 0 && diff < 4)
            {
                ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.DarkGreen, true);
                previousLevel = level;
                continue;
            }
            else
            {
                ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.DarkRed, true);
                previousLevel = level;
                isSafe = false;
                continue;
            }
        }

        // Decreasing
        else if (level < previousLevel && (mode == 2 || mode == 0))
        {
            mode = 2;

            int diff = Math.Abs(level - previousLevel);
            if (diff > 0 && diff < 4)
            {
                ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.DarkGreen, true);
                previousLevel = level;
                continue;
            } else
            {
                ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.DarkRed, true);
                previousLevel = level;
                isSafe = false;
                continue;
            }
        } else
        {
            // Unsafe due to increase/decrease difference
            ConsoleHelper.WriteInline(level.ToString() + " ", ConsoleColor.DarkMagenta, true);
            isSafe = false;
        }
    }

    if (isSafe)
    {
        ConsoleHelper.WriteInline("report is: ", ConsoleColor.Blue);
        ConsoleHelper.WriteInline("safe!\n", ConsoleColor.DarkGreen, true);
        return true;
    } else {
        ConsoleHelper.WriteInline("report is: ", ConsoleColor.Blue);
        ConsoleHelper.WriteInline("unsafe!\n", ConsoleColor.DarkRed, true);
        return false; 
    }
}

bool isReportDampenedSafe(List<int> report)
{
    ConsoleHelper.Write($"  Processing dampened report [{ListAsString(report)}]: ", ConsoleColor.DarkCyan, true);

    for (int i = 0; i < report.Count; i++) {
        int[] copyOfReport = report.ToArray(); ;
        List<int> newReport = copyOfReport.ToList();
        newReport.RemoveAt(i);
        ConsoleHelper.WriteInline("  new report for processing: ", ConsoleColor.DarkCyan);
        ConsoleHelper.WriteInline(ListAsString(newReport), ConsoleColor.Cyan, true);
        if (isReportSafe(newReport))
        {
            //ConsoleHelper.WriteInline("which is: ", ConsoleColor.Blue);
            //ConsoleHelper.WriteInline("safe!\n", ConsoleColor.DarkGreen, true);
            return true;
        } else
        {
            //ConsoleHelper.WriteInline("which is: ", ConsoleColor.Blue);
            //ConsoleHelper.WriteInline("unsafe!\n", ConsoleColor.DarkMagenta, true);
        }
    }
    return false;
}

static string ListAsString(List<int> list, string separator = " ")
{
    string result = "";
    foreach(int i in list)
    {
        result = result + i.ToString() + separator;
    }
    return result;
}