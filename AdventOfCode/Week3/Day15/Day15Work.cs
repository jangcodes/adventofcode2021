using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day15
{
    internal class Day15Work
    {
        private static List<int[]> grid = new List<int[]>();

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day15\Example.txt");

            var row = input.Length;
            var col = input[0].Length;

            grid = input.Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToList();

            int result = AddPathRecursively(0, 0);

            Console.WriteLine($"Part 1 Answer: {result - grid[0][0] }");
        }


        private static int AddPathRecursively(int y, int x)
        {
            int result = grid[y][x];

            if (y == grid.Count - 1 && x == grid[0].Length - 1)
            {
                return result;
            }

            if (y == grid.Count - 1)
            {
                return result += AddPathRecursively(y, x + 1);
            }

            if (x == grid[0].Length - 1)
            {
                return result += AddPathRecursively(y + 1, x);
            }

            var rightPathSum = AddPathRecursively(y, x + 1);
            var downPathSum = AddPathRecursively(y + 1, x);

            result += Math.Min(rightPathSum, downPathSum);

            return result;
        }
    }
}
