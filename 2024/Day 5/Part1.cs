using AoCHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024_5
{
    internal class Part1
    {
        public Part1()
        {

            ConsoleHelper.SetDayStart(5);
            ConsoleHelper.SetSectionStart("Part 1");

            // ---------------------------------------------
            List<string> inputLines = FileHelper.GetLinesFromFile("input.txt");

            List<PageOrder> orderRules = new List<PageOrder>();
            List<string> orderRulesAsString = new List<string>();
            List<List<int>> pageUpdates = new List<List<int>>();
            List<List<int>> invalidUpdates = new List<List<int>>();

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
            ConsoleHelper.Write(" update lines.");
            ConsoleHelper.WriteSeparator();

            int totalSum = 0;
            int validUpdates = 0;

            // Loop through pagenums
            //   VerifyBeforeValid(num, ListBefore, rules)
            //   VerifyAfterValid(num, ListAfter, rules)
            //   If either is false, reject update (break out of nums loop & set isValidUpdate to false), continue loop otherwise
            // All nums checked without invalid (check isValidUpdate)
            // Obtain middle number
            // Add middle number to total sum
            foreach (List<int> pageUpdate in pageUpdates)
            {
                bool isValidUpdate = true;
                ConsoleHelper.WriteInline("Verifying page updates ");
                ConsoleHelper.Write(ConsoleHelper.IntListAsString(pageUpdate), ConsoleColor.Cyan, true);

                foreach (int pageNumber in pageUpdate)
                {
                    List<int> beforeNumList = GetListBeforeIndex(pageUpdate.IndexOf(pageNumber), pageUpdate);
                    List<int> afterNumList = GetListAfterIndex(pageUpdate.IndexOf(pageNumber), pageUpdate);

                    //if (beforeNumList.Count != 0) {
                    //    ConsoleHelper.WriteInline("    " + ConsoleHelper.IntListAsString(beforeNumList), ConsoleColor.DarkMagenta, true);
                    //} else
                    //{
                    //    ConsoleHelper.WriteInline("   ");
                    //}

                    //ConsoleHelper.WriteInline(" " + pageNumber.ToString(), ConsoleColor.Magenta, true);

                    //if (afterNumList.Count != 0)
                    //{
                    //    ConsoleHelper.WriteInline(" " + ConsoleHelper.IntListAsString(afterNumList), ConsoleColor.DarkMagenta, true);
                    //}

                    if (VerifyBefore(pageNumber, beforeNumList) && VerifyAfter(pageNumber, afterNumList))
                    {
                        // Current num is valid, go to next
                        //ConsoleHelper.WriteInline(" ==> ", ConsoleColor.DarkYellow);
                        //ConsoleHelper.Write("Valid!", ConsoleColor.DarkGreen, true);
                        continue;
                    }
                    else
                    {
                        // Num is invalid, stop loop
                        isValidUpdate = false;
                        ConsoleHelper.WriteInline(" ==> ", ConsoleColor.DarkYellow);
                        ConsoleHelper.Write("Invalid!", ConsoleColor.DarkRed, true);
                        invalidUpdates.Add(pageUpdate);
                        break;
                    }
                }

                if (isValidUpdate)
                {
                    ConsoleHelper.WriteInline(" ==> ", ConsoleColor.DarkYellow);
                    ConsoleHelper.WriteInline("Valid!", ConsoleColor.DarkGreen, true);
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

                ConsoleHelper.GoToNextLine();
            }

            ConsoleHelper.WriteSeparator();
            ConsoleHelper.Write("Finished processing rules. ");
            ConsoleHelper.WriteInline("There are ");
            ConsoleHelper.WriteInline(validUpdates.ToString(), ConsoleColor.Cyan, true);
            ConsoleHelper.WriteInline(" valid updates and the total sum is: ");
            ConsoleHelper.Write(totalSum.ToString(), ConsoleColor.Green, true);

            //-----------------------------------------------
            ConsoleHelper.SetSectionStart("Part 2");
            ConsoleHelper.WriteInline("There are ");
            ConsoleHelper.WriteInline(invalidUpdates.Count.ToString(), ConsoleColor.Cyan, true);
            ConsoleHelper.Write(" invalid updates to process.");

            foreach (List<int> invalidUpdate in invalidUpdates)
            {
                ConsoleHelper.Write(ConsoleHelper.IntListAsString(invalidUpdate), ConsoleColor.DarkMagenta, true);
            }


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

            bool VerifyBefore(int pageNumber, List<int> beforeList)
            {
                if (beforeList.Count == 0)
                {
                    return true;
                }
                foreach (int i in beforeList)
                {
                    if (orderRulesAsString.Contains($"{i}|{pageNumber}"))
                    {
                        // explicit rule exists, so other direcrtion is not possible
                        continue;


                    }
                    else // number does not exists in 'before' placement, verify that there is no rule the other way around
                    {
                        // Not sure that rule exists, verify that it does not exists the other way
                        if (VerifyAfter(pageNumber, new List<int> { i }))
                        {
                            // Rule in the other direction exists! Thus this is not valid!
                            ConsoleHelper.WriteInline($" violated rule ({pageNumber}|{i})! ", ConsoleColor.DarkRed, true);
                            return false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return true;
            }

            bool VerifyAfter(int pageNumber, List<int> afterList)
            {
                if (afterList.Count == 0)
                {
                    return true;
                }
                foreach (int i in afterList)
                {
                    if (orderRulesAsString.Contains($"{pageNumber}|{i}"))
                    {
                        // explicit rule exists, so other direcrtion is not possible
                        continue;
                    }
                    else // number does not exists in 'after' placement, verify that there is no rule the other way around
                    {
                        // Not sure that rule exists, verify that it does not exists the other way
                        if (VerifyBefore(pageNumber, new List<int> { i }))
                        {
                            // Rule in the other direction exists! Thus this is not valid!
                            ConsoleHelper.WriteInline($" violated rule ({i}|{pageNumber})! ", ConsoleColor.DarkRed, true);
                            return false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return true;
            }
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
    }
}
