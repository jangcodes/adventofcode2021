using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day1
{
    internal class Day1Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("=============Day 1==============");

            string[]? input = await File.ReadAllLinesAsync(@"Day1\Input.txt");
            var numberLines = input.Select(textLine => Convert.ToInt32(textLine)).ToArray();

            int increasedCount = 0;

            for (int i = 3; i < input.Length; i++)
            {
                int previousSum = numberLines[i - 1] + numberLines[i - 2] + numberLines[i - 3];
                int currentSum = numberLines[i] + numberLines[i - 1] + numberLines[i - 2];

                if (currentSum > previousSum)
                {
                    increasedCount++;
                }
            }

            Console.WriteLine("Day 1 Answer: " + increasedCount);
        }
    }
}
