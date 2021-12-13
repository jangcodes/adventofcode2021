using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day13
{
    internal class Day13Work
    {
        public static async Task Execute()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            string[] input = await File.ReadAllLinesAsync(@"Week2\Day13\Input.txt");

            var coordinates = input
                .Where(x => x.Contains(','))
                .Select(x => x.Split(',').Select(y => Convert.ToInt32(y)).ToArray());

            var foldingInstruction = input
                .Where(x => x.Contains("fold along "))
                .Select(x => x.Replace("fold along ", ""))
                .Select(x => (c: x[0], v: Convert.ToInt32(x[2..])));

            int lastX = coordinates.Select(x => x[0]).Max();
            int lastY = coordinates.Select(x => x[1]).Max();

            bool[,] grid = new bool[lastY + 1, lastX + 1];

            foreach (var c in coordinates) grid[c[1], c[0]] = true;

            bool showPart1Answer = true;
            foreach (var (foldAlong, coordinate) in foldingInstruction)
            {
                if (foldAlong == 'x')
                {
                    for (int i = coordinate + 1; i <= lastX; i++)
                    {
                        int x = (2 * coordinate) - i;
                        if (x >= 0)
                        {
                            for (int y = 0; y < grid.GetLength(0); y++)
                            {
                                grid[y, x] = grid[y, i] || grid[y, x];
                                grid[y, i] = false;
                            }
                        }
                    }
                    lastX = coordinate;
                }
                else
                {
                    for (int i = coordinate + 1; i <= lastY; i++)
                    {
                        int y = (2 * coordinate) - i;
                        if (y >= 0)
                        {
                            for (int x = 0; x < grid.GetLength(1); x++)
                            {
                                grid[y, x] = grid[i, x] || grid[y, x];
                                grid[i, x] = false;
                            }
                        }
                    }
                    lastY = coordinate;
                }

                if (showPart1Answer)
                {
                    int part1Answer = 0;
                    foreach (var item in grid) if (item) part1Answer++;
                    Console.WriteLine($"Part 1 Answer: {part1Answer}");
                    showPart1Answer = false;
                }
            }

            Console.WriteLine("Part 2 Answer:");
            PrintGrid(grid, lastY, lastX);

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }


        private static void PrintGrid(bool[,] grid, int lengthY, int lengthX)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    Console.Write(grid[y, x] ? '#' : ' ');
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
