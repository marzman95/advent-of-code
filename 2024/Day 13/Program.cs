using AoCHelpers;

ConsoleHelper.SetDayStart(13);
ConsoleHelper.SetSectionStart("Part 1");

// ---------------------------------------------
List<string> inputLines = FileHelper.GetLinesFromFile("input.txt");
List<ClawMachine> machines = new List<ClawMachine>();

Dictionary<string, int> buttonAMove = new Dictionary<string, int>();
Dictionary<string, int> buttonBMove = new Dictionary<string, int>();
PrizePosition prizePosition = new PrizePosition();


foreach (string line in inputLines)
{
    if (line.Contains("Button A:"))
    {
        string movementDefinitionX = line.Substring(line.IndexOf("X+"), line.IndexOf(", Y+") - line.IndexOf("X+"));
        string movementDefinitionY = line.Substring(line.IndexOf("Y+"));
        movementDefinitionX = movementDefinitionX.Replace("X+", "");
        movementDefinitionY = movementDefinitionY.Replace("Y+", "");
        int moveX = int.Parse(movementDefinitionX);
        int moveY = int.Parse(movementDefinitionY);
        buttonAMove.Add("X", moveX);
        buttonAMove.Add("Y", moveY);

    }
    if (line.Contains("Button B:"))
    {
        string movementDefinitionX = line.Substring(line.IndexOf("X+"), line.IndexOf(", Y+") - line.IndexOf("X+"));
        string movementDefinitionY = line.Substring(line.IndexOf("Y+"));
        movementDefinitionX = movementDefinitionX.Replace("X+", "");
        movementDefinitionY = movementDefinitionY.Replace("Y+", "");
        int moveX = int.Parse(movementDefinitionX);
        int moveY = int.Parse(movementDefinitionY);
        buttonBMove.Add("X", moveX);
        buttonBMove.Add("Y", moveY);
    }
    if (line.Contains("Prize:"))
    {
        string movementDefinitionX = line.Substring(line.IndexOf("X="), line.IndexOf(", Y=") - line.IndexOf("X="));
        string movementDefinitionY = line.Substring(line.IndexOf("Y="));
        movementDefinitionX = movementDefinitionX.Replace("X=", "");
        movementDefinitionY = movementDefinitionY.Replace("Y=", "");
        int locX = int.Parse(movementDefinitionX);
        int locY = int.Parse(movementDefinitionY);
        prizePosition = new PrizePosition(locX + 10000000000000L, locY + 10000000000000L);
    }
    if (line == "")
    {
        machines.Add(new ClawMachine(buttonAMove, buttonBMove, prizePosition));
        buttonAMove = new Dictionary<string, int>();
        buttonBMove = new Dictionary<string, int>();
        prizePosition = new PrizePosition();
    }
}

ConsoleHelper.WriteInline("Found ");
ConsoleHelper.WriteInline(machines.Count.ToString(), ConsoleColor.Cyan, true);
ConsoleHelper.Write(" claw machines!");
ConsoleHelper.WriteSeparator();

long totalTokens = 0;

foreach (ClawMachine cm in machines)
{
    // Do the maths
    /*
     * Find minimal tokenSum where
     *   For each A-press, cost of 3 tokens:
     *     tokenSum = a * 3, where a <= 100
     *   For each B-press: cost of 1 token:
     *     tokenSum = b, where b <= 100
     * add tokenSum to totalTokens
     * 
     * prize.X = a(A.X) + b(B.X) = am + bn
     *  
     * prize.Y = a(A.Y) + b(B.Y)
     *   (a + b)
     */
    long tokens = cm.TokensOfMachine();
    ConsoleHelper.WriteInline("Machine can be solved with ");
    ConsoleHelper.WriteInline(tokens.ToString(), ConsoleColor.Magenta, true);
    ConsoleHelper.Write(" tokens");
    totalTokens = totalTokens + tokens;
}

ConsoleHelper.WriteSeparator();
ConsoleHelper.WriteInline("Total minimal tokens: ");
ConsoleHelper.WriteInline(totalTokens.ToString(), ConsoleColor.Green, true);


class ClawMachine
{
    Dictionary<string, int> MovementA;
    Dictionary<string, int> MovementB;
    PrizePosition Prize;

    public ClawMachine (Dictionary<string, int> moveA, Dictionary<string, int> moveB, PrizePosition prize)
    {
        this.MovementA = moveA;
        this.MovementB = moveB;
        this.Prize = prize;
    }

    public long TokensOfMachine()
    {
        int moveAX = MovementA["X"];
        int moveAY = MovementA["Y"];
        int moveBX = MovementB["X"];
        int moveBY = MovementB["Y"];

        long top = moveAX * Prize.Y - moveAY * Prize.X;
        long bottom = (long)moveAX * moveBY - (long)moveBX * moveAY;

        if (top % bottom == 0)
        {
            long b = top / bottom;
            top = Prize.Y - b * moveBY;
            if (top % moveAY == 0)
            {
                long a = top / moveAY;
                return 3 * a + b;
            } else
            {
                return 0;
            }
        } else
        {
            return 0;
        }
    }
}

class PrizePosition
{
    public long X = 0;
    public long Y = 0;

    public PrizePosition() { }

    public PrizePosition(long x, long y)
    {
        X = x;
        Y = y;
    }
}