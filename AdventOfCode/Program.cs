string[]? textLine = await System.IO.File.ReadAllLinesAsync(@"Day1Input.txt");
int[]? numberLines = textLine.Select(textLine => Convert.ToInt32(textLine)).ToArray();

int increasedCount = 0;

for (int i = 3; i < textLine.Length; i++)
{
    int previousSum = numberLines[i - 1] + numberLines[i - 2] + numberLines[i - 3];
    int currentSum = numberLines[i] + numberLines[i - 1] + numberLines[i - 2];

    if (currentSum > previousSum)
    {
        increasedCount++;
    }
}

Console.WriteLine(increasedCount);
Console.ReadLine();