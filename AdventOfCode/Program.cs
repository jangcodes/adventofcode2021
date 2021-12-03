Console.WriteLine("================================");
Console.WriteLine("=============Day 1==============");
Console.WriteLine("================================");

string[]? textLineDay1 = await System.IO.File.ReadAllLinesAsync(@"Day1Input.txt");
int[]? numberLines = textLineDay1.Select(textLine => Convert.ToInt32(textLine)).ToArray();

int increasedCount = 0;

for (int i = 3; i < textLineDay1.Length; i++)
{
    int previousSum = numberLines[i - 1] + numberLines[i - 2] + numberLines[i - 3];
    int currentSum = numberLines[i] + numberLines[i - 1] + numberLines[i - 2];

    if (currentSum > previousSum)
    {
        increasedCount++;
    }
}

Console.WriteLine("Day 1 Answer: " + increasedCount);
Console.ReadKey();
Console.WriteLine();


string[]? textLineDay2 = await System.IO.File.ReadAllLinesAsync(@"Day2Input.txt");

int horizontalPosition = 0;
int depth = 0;
int aim = 0;

foreach (string line in textLineDay2)
{
    if (line.Contains("down"))
    {
        aim = aim + Convert.ToInt32(line.Replace("down ", ""));
    }
    else if (line.Contains("up"))
    {
        aim = aim - Convert.ToInt32(line.Replace("up ", ""));
    }
    else if (line.Contains("forward"))
    {
        var forwardAmount = Convert.ToInt32(line.Replace("forward ", ""));
        horizontalPosition = horizontalPosition + forwardAmount;
        depth = depth + (aim * forwardAmount);
    }
}

Console.WriteLine("================================");
Console.WriteLine("=============Day 2==============");
Console.WriteLine("================================");

Console.WriteLine("Forward Sum: " + horizontalPosition);
Console.WriteLine("Depth Sum: " + depth);
Console.WriteLine("Multiple: " + (depth * horizontalPosition));

Console.ReadKey();