using AdventOfCode.Day1;
using AdventOfCode.Day2;

Console.WriteLine("================================");
Console.WriteLine("=============Day 1==============");
Console.WriteLine("================================");

int day1Result = await Day1Work.Execute();

Console.WriteLine("Day 1 Answer: " + day1Result);

Console.WriteLine();

Console.WriteLine("================================");
Console.WriteLine("=============Day 2==============");
Console.WriteLine("================================");

int day2Result = await Day2Work.Execute(); 
Console.WriteLine("Day 2 Answer: " + day2Result);

Console.ReadKey();