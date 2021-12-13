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
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day13\Example.txt");

            IEnumerable<int[]> coordinates = input
                .Where(x => x.Contains(','))
                .Select(x => x.Split(',').Select(y => Convert.ToInt32(y)).ToArray());

            var foldingInstruction = input
                .Where(x => x.Contains("fold along "))
                .Select(x => x.Replace("fold along ", ""))
                .Select(x => (c:x[0], v: Convert.ToInt32(x[2..])));

            int Xmax = coordinates.Select(x => x[0]).Max();
            int Ymax = coordinates.Select(x => x[1]).Max();

            int[,] grid = new int[Xmax + 1, Ymax + 1];

            foreach (var c in coordinates)
            {
                grid[c[0], c[1]]++;
            }

            foreach (var (foldAlong, coordinate) in foldingInstruction)
            {
                if (foldAlong == 'x')
                {
                    for (int i = coordinate + 1; i < grid.GetLength(0); i++)
                    {
                        int j = i - coordinate;

                        for (int y = 0; y < grid.GetLength(1); y++)
                        {
                            grid[j, y] += grid[i, y];
                            grid[i, y] = 0;
                        }
                    }
                }
            }

            PrintGrid(grid);
        }


        private static void PrintGrid(int [,] grid)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                for(int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
