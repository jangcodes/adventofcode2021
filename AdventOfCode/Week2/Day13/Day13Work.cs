using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day13
{
    internal class Day13Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day13\Input.txt");

            IEnumerable<int[]> coordinates = input
                .Where(x => x.Contains(','))
                .Select(x => x.Split(',').Select(y => Convert.ToInt32(y)).ToArray());

            var foldingInstruction = input
                .Where(x => x.Contains("fold along "))
                .Select(x => x.Replace("fold along ", ""))
                .Select(x => (c: x[0], v: Convert.ToInt32(x[2..])));

            int Xmax = coordinates.Select(x => x[0]).Max();
            int Ymax = coordinates.Select(x => x[1]).Max();

            int[,] grid = new int[Ymax + 1, Xmax + 1];

            foreach (var c in coordinates)
            {
                grid[c[1], c[0]]++;
            }

            int lastX = 0;
            int lastY = 0;
            foreach (var (foldAlong, coordinate) in foldingInstruction)
            {
                if (foldAlong == 'x')
                {
                    for (int i = coordinate + 1; i < grid.GetLength(1); i++)
                    {
                        int j = i - coordinate;

                        if (coordinate - j >= 0)
                        {
                            for (int y = 0; y < grid.GetLength(0); y++)
                            {
                                grid[y, coordinate - j] += grid[y, i];
                                grid[y, i] = 0;
                            }
                        }

                        
                    }

                    lastX = coordinate;
                }
                else
                {
                    for (int i = coordinate + 1; i < grid.GetLength(0); i++)
                    {
                        int j = i - coordinate;

                        if (coordinate - j >= 0)
                        {
                            for (int x = 0; x < grid.GetLength(1); x++)
                            {
                                grid[coordinate - j, x] += grid[i, x];
                                grid[i, x] = 0;
                            }
                        }

                        
                    }

                    lastY = coordinate;
                }
            }

            PrintGrid(grid, lastY, lastX);

            //int count = 0;

            //foreach(var item in grid)
            //{
            //    if (item > 0) count++;
            //}

            //Console.WriteLine($"Part 1 Answer: {count}");
        }




        private static void PrintGrid(int[,] grid, int lengthY, int lengthX)
        {
            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    if (grid[y, x] > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(1);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(0);
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
