using AoCHelpers;
// See https://aka.ms/new-console-template for more information

List<int> leftList = new List<int>();
List<int> rightList = new List<int>();
int totalDifferenceSum = 0;

// Open file
string[] inputIds = File.ReadAllLines("LocationIDList.txt");
Console.Write("Opened files, initialized lists. Processing file..");

// For each line
foreach (string line in inputIds)
{
    int leftLength = line.IndexOf("   ");
    string leftString = line.Substring(0, leftLength);
    string rightString = line.Substring(leftLength + 3);

    leftList.Add(int.Parse(leftString));
    rightList.Add(int.Parse(rightString));

    Console.Write(".");
}
Console.WriteLine($"\nFilled the lists! Elements left: {leftList.Count}, elements right: {rightList.Count}.");
if (leftList.Count != rightList.Count)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Lists are not equal length! Exiting application!");
    return;
}

leftList.Sort();
rightList.Sort();
Console.WriteLine($"Lists sorted! Processing differences.");

for (int i = 0; i < leftList.Count; i++) {
    int difference = Math.Abs(leftList[i] - rightList[i]);
    totalDifferenceSum += difference;
}
Console.WriteLine("Differences calculated!");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write("Total difference: ");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(totalDifferenceSum);
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine("Done with first!");

//------------------------------------------
ConsoleHelper.Write("Continue second exercise.", ConsoleColor.Blue);

ConsoleHelper.Write("Calculating similarity score..");
int similarityScore = 0;
foreach (int num in leftList)
{
    int rightListCount = rightList.FindAll(x => x == num).Count;
    similarityScore += rightListCount * num;
    ConsoleHelper.Write(".", Console.ForegroundColor, false, false);
}
ConsoleHelper.Write("\nSuccessfully calculated similarity score!");
ConsoleHelper.Write("Similarity Score: ", ConsoleColor.Cyan, false, false);
ConsoleHelper.Write(similarityScore.ToString(), ConsoleColor.Green, true);