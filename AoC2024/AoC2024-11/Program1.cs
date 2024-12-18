using AoCHelpers;

namespace AoC2024_11
{
    public class Part1
    {

        public Part1()
        {
            ConsoleHelper.SetDayStart(11);
            ConsoleHelper.SetSectionStart("Part 1");

            // ---------------------------------------------
            List<string> inputLines = FileHelper.GetLinesFromFile("input.txt");

            List<int> initialArrangement = FileHelper.SeparateLineToInt(inputLines[0], false);
            Int128 numberOfStones = initialArrangement.Count;
            int blinks = 0;
            List<Int128> newArrangement = new List<Int128>();
            foreach (int i in initialArrangement)
            {
                newArrangement.Add((Int128)i);
            }

            ConsoleHelper.WriteInline("Number of stones at start: ");
            ConsoleHelper.Write(numberOfStones.ToString(), ConsoleColor.Cyan, true);
            ConsoleHelper.WriteSeparator();
            ConsoleHelper.Write("Initial arrangement:");
            ConsoleHelper.Write(ListAsString(newArrangement) + "\n", ConsoleColor.Magenta, true);

            while (blinks < 25)
            {
                blinks++;
                newArrangement = Blink(newArrangement);
                ConsoleHelper.WriteInline("After blink ");
                ConsoleHelper.WriteInline(blinks.ToString(), ConsoleColor.Cyan, true);
                ConsoleHelper.WriteInline(" (");
                ConsoleHelper.WriteInline(newArrangement.Count.ToString(), ConsoleColor.DarkMagenta, true);
                ConsoleHelper.WriteInline(")");

                if (blinks < 5 || (blinks % 10) == 0)
                {
                    ConsoleHelper.Write(":");
                    ConsoleHelper.Write(ListAsString(newArrangement) + "\n", ConsoleColor.Magenta, true);
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
            ConsoleHelper.Write(newArrangement.Count.ToString(), ConsoleColor.Green, true);
            ConsoleHelper.WriteSeparator();

            static List<Int128> Blink(List<Int128> stones)
            {
                List<Int128> newStones = new List<Int128>();
                for (int s = 0; s < stones.Count; s++)
                {
                    Int128 numberOnStone = stones[s];
                    if (CheckRule1(numberOnStone))
                    {
                        // Rule 1
                        //  Current is 0, replace with 1
                        newStones.Add(1);
                    }
                    else if (CheckRule2(numberOnStone))
                    {
                        // Rule 2
                        //  Current is even digits, replace with
                        //    split current in 2 halves
                        //    replace current with first half
                        //    add second half to next position, skip next (s++)
                        Int128[] split = SplitNumber(numberOnStone);
                        newStones.Add(split[0]);
                        newStones.Add(split[1]);
                    }
                    else
                    {
                        // Rule 3
                        //  Multiply current with 2024
                        //  Replace current with mulplication
                        newStones.Add(numberOnStone * 2024);
                    }
                }
                return newStones;
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

            static string ListAsString(List<Int128> list)
            {
                string result = "";
                foreach (Int128 n in list)
                {
                    result = result + n.ToString() + " ";
                }
                return result.Remove(result.Length - 1, 1);
            }
        }
    }
}