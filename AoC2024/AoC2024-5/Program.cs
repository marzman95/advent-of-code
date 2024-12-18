using AoCHelpers;

ConsoleHelper.SetDayStart(5);
ConsoleHelper.SetSectionStart("Part 2");

// ---------------------------------------------
List<string> inputLines = FileHelper.GetLinesFromFile("invalidRuleSet.txt");

List<PageOrder> orderRules = new List<PageOrder>();
List<string> orderRulesAsString = new List<string>();
List<List<int>> pageUpdates = new List<List<int>>();
List<List<int>> newValidUpdates = new List<List<int>>();

List<int> listOfBeforePages = new List<int>();
List<int> listOfAfterPages = new List<int>();

foreach (string line in inputLines)
{
    if (line.Contains("|"))
    {
        // add line to orders
        PageOrder newOrder = PageOrder.ListToOrder(FileHelper.SeparateLineToInt(line, false, "|"));
        listOfBeforePages.Add(newOrder.before);
        listOfAfterPages.Add(newOrder.after);
        orderRules.Add(newOrder);
        orderRulesAsString.Add(line);
    }
    else if (line.Contains(","))
    {
        // Process line and add to updates
        pageUpdates.Add(FileHelper.SeparateLineToInt(line, false, ","));
    }
    else
    {
        // Empty line/do nothing
    }
}

ConsoleHelper.WriteInline("Input has ");
ConsoleHelper.WriteInline(orderRules.Count.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.WriteInline(" order rules and has ");
ConsoleHelper.WriteInline(pageUpdates.Count.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.Write(" invalid update lines.");
ConsoleHelper.WriteSeparator();

int totalSum = 0;
int validUpdates = 0;

foreach (List<int> pageUpdate in pageUpdates)
{
    ConsoleHelper.WriteInline("Processing page updates ");
    ConsoleHelper.Write(ConsoleHelper.IntListAsString(pageUpdate), ConsoleColor.Cyan, true);

    for (int pnIndex = 0; pnIndex < pageUpdate.Count; pnIndex++)
    {
        int pageNumber = pageUpdate[pnIndex];
        List<int> beforeNumList = GetListBeforeIndex(pageUpdate.IndexOf(pageNumber), pageUpdate);
        List<int> afterNumList = GetListAfterIndex(pageUpdate.IndexOf(pageNumber), pageUpdate);

        if (beforeNumList.Count != 0)
        {
            ConsoleHelper.WriteInline("    " + ConsoleHelper.IntListAsString(beforeNumList), ConsoleColor.DarkMagenta, true);
        }
        else
        {
            ConsoleHelper.WriteInline("   ");
        }

        ConsoleHelper.WriteInline(" " + pageNumber.ToString(), ConsoleColor.Magenta, true);

        if (afterNumList.Count != 0)
        {
            ConsoleHelper.WriteInline(" " + ConsoleHelper.IntListAsString(afterNumList), ConsoleColor.DarkMagenta, true);
        }

        int invalidBeforeCheck = VerifyBefore(pageNumber, beforeNumList);

        if (invalidBeforeCheck != 1)
        {
            ConsoleHelper.WriteInline(" ==> ", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteInline(pageNumber.ToString(), ConsoleColor.Cyan, true);
            ConsoleHelper.Write($" violated rule ({pageNumber}|{invalidBeforeCheck})", ConsoleColor.DarkRed, true);
            int indexOfInvalid = pageUpdate.IndexOf(invalidBeforeCheck);
            pageUpdate[indexOfInvalid] = pageNumber;
            pageUpdate[pnIndex] = invalidBeforeCheck;
            pnIndex = -1;
            continue;
        }

        int invalidAfterCheck = VerifyAfter(pageNumber, afterNumList);
        if (invalidAfterCheck != 1)
        {
            ConsoleHelper.WriteInline(" ==> ", ConsoleColor.DarkYellow);
            ConsoleHelper.WriteInline(pageNumber.ToString(), ConsoleColor.Cyan, true);
            ConsoleHelper.Write($" violated rule ({invalidAfterCheck}|{pageNumber})", ConsoleColor.DarkRed, true);
            int indexOfInvalid = pageUpdate.IndexOf(invalidAfterCheck);
            pageUpdate[indexOfInvalid] = pageNumber;
            pageUpdate[pnIndex] = invalidAfterCheck;
            pnIndex = -1;
            continue;
        }
        
        ConsoleHelper.GoToNextLine();
    }

        int middlePageNum = pageUpdate[pageUpdate.Count / 2];
        ConsoleHelper.WriteInline(" Middle number: ");
        ConsoleHelper.WriteInline(middlePageNum.ToString(), ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(" +", ConsoleColor.DarkMagenta, true);
        ConsoleHelper.WriteInline(" totalSum", ConsoleColor.Magenta, true);
        ConsoleHelper.WriteInline(" = ", ConsoleColor.DarkMagenta, true);
        totalSum = totalSum + middlePageNum;
        validUpdates++;
        ConsoleHelper.Write(totalSum.ToString(), ConsoleColor.DarkGreen, true);
    }

ConsoleHelper.WriteSeparator();
ConsoleHelper.Write("Finished processing rules. ");
ConsoleHelper.WriteInline("There are ");
ConsoleHelper.WriteInline(validUpdates.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.WriteInline(" valid updates and the total sum is: ");
ConsoleHelper.Write(totalSum.ToString(), ConsoleColor.Green, true);

static List<int> GetListBeforeIndex(int index, List<int> list)
{
    List<int> result = new List<int>();
    for (int i = 0; i < index; i++)
    {
        result.Add(list[i]);
    }
    return result;
}

static List<int> GetListAfterIndex(int index, List<int> list)
{
    List<int> result = new List<int>();
    for (int i = index + 1; i < list.Count; i++)
    {
        result.Add(list[i]);
    }
    return result;
}

int VerifyBefore(int pageNumber, List<int> beforeList)
{
    if (beforeList.Count == 0)
    {
        return 1;
    }
    foreach (int i in beforeList)
    {
        if (orderRulesAsString.Contains($"{i}|{pageNumber}"))
        {
            // explicit rule exists, so other direcrtion is not possible
            continue;


        } else // number does not exists in 'before' placement, verify that there is no rule the other way around
        {
            // Not sure that rule exists, verify that it does not exists the other way
            if (orderRulesAsString.Contains($"{pageNumber}|{i}"))
            {
                // Rule in the other direction exists! Thus this is not valid!
                return i;
            }
            else
            {
                continue;
            }
        }
    }
    return 1;
}

int VerifyAfter(int pageNumber, List<int> afterList)
{
    if (afterList.Count == 0)
    {
        return 1;
    }
    foreach (int i in afterList)
    {
        if (orderRulesAsString.Contains($"{pageNumber}|{i}")) {
            // explicit rule exists, so other direcrtion is not possible
            continue;
        }
        else // number does not exists in 'after' placement, verify that there is no rule the other way around
        {
            // Not sure that rule exists, verify that it does not exists the other way
            if (orderRulesAsString.Contains($"{i}|{pageNumber}"))
            {
                // Rule in the other direction exists! Thus this is not valid!
                //ConsoleHelper.WriteInline($" violated rule ({i}|{pageNumber})! ", ConsoleColor.DarkRed, true);
                return i;
            }
            else
            {
                continue;
            }
        }
    }
    return 1;
}


class PageOrder
{
    public int before { get; private set; }
    public int after { get; private set; }

    PageOrder(int first, int second)
    {
        before = first;
        after = second;
    }

    public static PageOrder ListToOrder(List<int> orderList)
    {
        return new PageOrder(orderList[0], orderList[1]);
    }

}