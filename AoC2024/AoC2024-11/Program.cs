using AoCHelpers;
using System.Security.Cryptography;

ConsoleHelper.SetDayStart(11);
ConsoleHelper.SetSectionStart("Part 2");

// ---------------------------------------------
List<string> inputLines = FileHelper.GetLinesFromFile("input.txt");

List<int> initialArrangement = FileHelper.SeparateLineToInt(inputLines[0], false);
int numberOfStones = initialArrangement.Count;
int blinks = 0;
Dictionary<Int128, Int128> stoneArrangement = new Dictionary<Int128, Int128>();

for(int i = 0; i < initialArrangement.Count; i++)
{
    stoneArrangement.Add(initialArrangement[i], 1);
}

ConsoleHelper.WriteInline("Number of stones at start: ");
ConsoleHelper.Write(numberOfStones.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.WriteSeparator();
ConsoleHelper.Write("Initial arrangement:");
ConsoleHelper.Write(DictionaryToString(stoneArrangement) + "\n", ConsoleColor.Magenta, true);

while (blinks < 75)
{
    blinks++;
    stoneArrangement = Blink(stoneArrangement);
    ConsoleHelper.WriteInline("After blink ");
    ConsoleHelper.WriteInline(blinks.ToString(), ConsoleColor.Cyan, true);
    ConsoleHelper.WriteInline(" (");
    ConsoleHelper.WriteInline(CountStones(stoneArrangement).ToString(), ConsoleColor.DarkMagenta, true);
    ConsoleHelper.WriteInline(")");

    if (blinks < 5)
    {
        ConsoleHelper.Write(":");
        ConsoleHelper.Write(DictionaryToString(stoneArrangement) + "\n", ConsoleColor.Magenta, true);
    }
    else
    {
        ConsoleHelper.Write("\n");
    }
}

ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteInline("Amount of stone after ");
ConsoleHelper.WriteInline(blinks.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.WriteInline(" blinks: ");
ConsoleHelper.Write(CountStones(stoneArrangement).ToString(), ConsoleColor.Green, true); // Change to sum all values
ConsoleHelper.WriteSeparator();

static Dictionary<Int128, Int128> Blink(Dictionary<Int128, Int128> stones)
{
    Dictionary<Int128, Int128> newArrangement = new Dictionary<Int128, Int128>();
    // Adjust to process dictionary with keys as counters
    // If some values (0, 1, 2, 20, 24, 40, 80, 2024, 4048, 8096), process them directly
    // otherwise, use old processing
    foreach (KeyValuePair<Int128, Int128> stoneSet in stones)
    {
        Int128 numberOnStone = stoneSet.Key;
        if (numberOnStone == 0)
        {
            if (newArrangement.ContainsKey(1))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[1];
                newArrangement[1] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(1, stoneSet.Value);
            }

        } else if (numberOnStone == 1)
        {
            // Number 1 --> 1 * 2024 (odd length) = 2024
            if (newArrangement.ContainsKey(2024))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[2024];
                newArrangement[2024] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(2024, stoneSet.Value);
            }

        } else if (numberOnStone == 2)
        {
            // Number 2 --> 2 * 2024 (odd length) = 4048
            if (newArrangement.ContainsKey(4048))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[4048];
                newArrangement[4048] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(4048, stoneSet.Value);
            }

        } else if (numberOnStone == 20)
        {
            // Number 20 --> split amongst 2's and 0's
            // Add the new 2's
            if (newArrangement.ContainsKey(2))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[2];
                newArrangement[2] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(2, stoneSet.Value);
            }
            // Add the new 0's
            if (newArrangement.ContainsKey(0))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[0];
                newArrangement[0] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(0, stoneSet.Value);
            }

        } else if (numberOnStone == 24)
        {
            // Number 24 --> split amongst 2's and 4's
            // Add the new 2's
            if (newArrangement.ContainsKey(2))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[2];
                newArrangement[2] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(2, stoneSet.Value);
            }
            // Add the new 4's
            if (newArrangement.ContainsKey(4))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[4];
                newArrangement[4] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(4, stoneSet.Value);
            }

        } else if (numberOnStone == 40)
        {
            // Number 40 --> split amongst 4's and 0's
            // Add the new 4's
            if (newArrangement.ContainsKey(4))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[4];
                newArrangement[4] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(4, stoneSet.Value);
            }
            // Add the new 0's
            if (newArrangement.ContainsKey(0))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[0];
                newArrangement[0] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(0, stoneSet.Value);
            }

        } else if (numberOnStone == 80)
        {
            // Number 80 --> split amongst 8's and 0's
            // Add the new 8's
            if (newArrangement.ContainsKey(8))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[8];
                newArrangement[8] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(8, stoneSet.Value);
            }
            // Add the new 0's
            if (newArrangement.ContainsKey(0))
            {
                // Add amount to existing
                Int128 oldAmount = newArrangement[0];
                newArrangement[0] = stoneSet.Value + oldAmount;
            }
            else
            {
                // Add new number with current existing amount
                newArrangement.Add(0, stoneSet.Value);
            }

        } else if (CheckRule2(numberOnStone)) // Process the second rule if it is not a common number
        {
            Int128[] split = SplitNumber(numberOnStone);
            Int128 splitLeft = split[0];
            Int128 splitRight = split[1];
            if (newArrangement.ContainsKey(splitLeft))
            {
                Int128 oldAmount = newArrangement[splitLeft];
                newArrangement[splitLeft] = stoneSet.Value + oldAmount;
            } else
            {
                newArrangement.Add(splitLeft, stoneSet.Value);
            }
            if (newArrangement.ContainsKey(splitRight))
            {
                Int128 oldAmount = newArrangement[splitRight];
                newArrangement[splitRight] = stoneSet.Value + oldAmount;
            }
            else
            {
                newArrangement.Add(splitRight, stoneSet.Value);
            }
        } else // Process odd-length number, not 0, 1 or 2
        {
            Int128 newNumber = numberOnStone * 2024;
            if (newArrangement.ContainsKey(newNumber))
            {
                Int128 oldAmount = newArrangement[newNumber];
                newArrangement[newNumber] = stoneSet.Value + oldAmount;
            } else
            {
                newArrangement.Add(newNumber, stoneSet.Value);
            }
        }
    }
    return newArrangement;
}

static bool CheckRule1(Int128 engravedNumber)
{
    return engravedNumber == 0;
}

static bool CheckRule2(Int128 engravedNumber)
{
    string numberAsString = engravedNumber.ToString();
    return numberAsString.Length % 2 == 0;
}

static Int128[] SplitNumber(Int128 engravedNumber)
{
    string numberAsString = engravedNumber.ToString();
    int length = numberAsString.Length;
    string part1 = numberAsString.Substring(0, length / 2);
    string part2 = numberAsString.Substring(length / 2, length / 2);
    Int128[] split = [int.Parse(part1), int.Parse(part2)];
    return split;
}

static string DictionaryToString(Dictionary<Int128, Int128> list)
{
    string result = "";
    foreach (KeyValuePair<Int128, Int128> pair in list)
    {
        for (int i = 0; i < pair.Value; i++)
        {
            result = result + pair.Key.ToString() + " ";
        }
    }
    return result.Remove(result.Length - 1, 1);
}

static Int128 CountStones(Dictionary<Int128, Int128> list)
{
    Int128 count = 0;
    Dictionary<Int128, Int128>.ValueCollection values = list.Values;
    foreach (Int128 value in values) {
        count = count + value;
    }
    return count;
}