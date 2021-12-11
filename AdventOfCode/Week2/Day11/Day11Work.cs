using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day11
{
    internal class Day11Work
    {
        private const int Iteration = 195;
        private static List<List<int>> Grid = new();

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day11\Input.txt");
            Grid = input.Select(x => x.Select(y => y - '0').ToList()).ToList();

            int part1Answer = 0;

            for (int i = 0; i < Iteration; i++)
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
                            part1Answer++;
                        }
                    }
                }


                //Console.WriteLine("-------------------");
                //foreach (var line in Grid)
                //{
                //    foreach (var c in line)
                //    {
                //        if (c == 0) Console.ForegroundColor = ConsoleColor.Red;
                //        Console.Write(c);
                //        Console.ResetColor();
                //    }
                //    Console.WriteLine();
                //}
                //Console.WriteLine("-------------------");
            }

            Console.WriteLine($"Part 1 Answer: {part1Answer}");
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
