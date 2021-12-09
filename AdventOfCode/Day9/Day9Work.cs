using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day9
{
    internal class Day9Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day9\Input.txt");

            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            int columnTotal = input[0].Length;
            int rowTotal = input.Length;
            List<int> lowNumbers = new();
            for (int r = 0; r < rowTotal; r++)
            {
                for (int c = 0; c < columnTotal; c++)
                {
                    int up = (r > 0) ? input[r - 1][c] - '0' : 10;
                    int down = (r < rowTotal - 1) ? input[r + 1][c] - '0' : 10;
                    int left = (c > 0) ? input[r][c - 1] - '0' : 10;
                    int right = (c < columnTotal - 1) ? input[r][c + 1] - '0' : 10;

                    int currentNumber = input[r][c] - '0';

                    if (currentNumber < up && currentNumber < down && currentNumber < left && currentNumber < right)
                    {
                        lowNumbers.Add(currentNumber + 1);
                    }
                }
            }

            Console.WriteLine($"Part 1 Answer: {lowNumbers.Sum()}");
        }

        private static void Part2(string[] input)
        {

        }
    }
}
