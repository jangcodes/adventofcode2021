using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day11
{
    internal class Day11Work
    {
        private const int Part1Iteration = 100;
        private static List<List<int>> Grid = new();

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day11\Input.txt");
            Grid = input.Select(x => x.Select(y => y - '0').ToList()).ToList();

            int iterationCounter = 0;
            int part1AnswerCounter = 0;
            int part1FinalAnswer = 0;

            while (true)
            {
                for (int r = 0; r < input.Length; r++)
                {
                    for (int c = 0; c < input[r].Length; c++)
                    {
                        MyResursiveFunction(r, c);
                    }
                }

                for (int r = 0; r < input.Length; r++)
                {
                    for (int c = 0; c < input[r].Length; c++)
                    {
                        if (Grid[r][c] > 9)
                        {
                            Grid[r][c] = 0;
                            part1AnswerCounter++;
                        }
                    }
                }

                iterationCounter++;

                if (Grid.All(x => x.All(y => y == 0)))
                {
                    break;
                }

                if (Part1Iteration == iterationCounter)
                {
                    part1FinalAnswer = part1AnswerCounter;
                }
            }

            Console.WriteLine($"Part 1 Answer: {part1FinalAnswer}");
            Console.WriteLine($"Part 2 Answer: {iterationCounter}");
        }

        private static void MyResursiveFunction(int r, int c)
        {
            if (Grid[r][c] <= 10) Grid[r][c]++;

            if (Grid[r][c] == 10)
            {
                if (r > 0)
                {
                    if (c > 0) MyResursiveFunction(r - 1, c - 1);

                    if (c < 9) MyResursiveFunction(r - 1, c + 1);

                    MyResursiveFunction(r - 1, c);
                }

                if (r < 9)
                {
                    if (c > 0) MyResursiveFunction(r + 1, c - 1);

                    if (c < 9) MyResursiveFunction(r + 1, c + 1);

                    MyResursiveFunction(r + 1, c);
                }

                if (c > 0) MyResursiveFunction(r, c - 1);

                if (c < 9) MyResursiveFunction(r, c + 1);
            }
        }
    }
}
