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
        private static int lowest = 0;

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day15\Example.txt");

            var row = input.Length;
            var col = input[0].Length;

            grid = input.Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToList();

            for (int y = 0; y < row; y++)
            {
                lowest += grid[y][y];
               
                if (y < row - 1)
                {
                    lowest += grid[y + 1][y];
                }
            }

            int result = AnotherRecursive(0, 0, 0);

            Console.WriteLine($"Part 1 Answer: {result - grid[0][0] }");
        }


        private static async Task<int> AddPathRecursively(int y, int x)
        {
            int result = grid[y][x];

            if (y == grid.Count - 1 && x == grid[0].Length - 1)
            {
                return result;
            }

            if (y == grid.Count - 1)
            {
                return result += await AddPathRecursively(y, x + 1);
            }

            if (x == grid[0].Length - 1)
            {
                return result += await AddPathRecursively(y + 1, x);
            }

            var rightPath = AddPathRecursively(y, x + 1);
            var downPath = AddPathRecursively(y + 1, x);


            var rightPathSum = await rightPath;
            var downPathSum = await downPath;

            result += Math.Min(rightPathSum, downPathSum);

            return result;
        }


        private static int AnotherRecursive(int y, int x, int currentSum)
        {
            int result = grid[y][x];
            currentSum += result;


            if (currentSum >= lowest)
            {
                return lowest;
            }

            if (y == grid.Count - 1 && x == grid[0].Length - 1)
            {
                
                if(currentSum < lowest)
                {
                    lowest = currentSum;
                }

                return result;
            }

            if (y == grid.Count - 1)
            {
                return result += AnotherRecursive(y, x + 1, currentSum);
            }

            if (x == grid[0].Length - 1)
            {
                return result += AnotherRecursive(y + 1, x, currentSum);
            }

            var rightPath = AnotherRecursive(y, x + 1, currentSum);
            var downPath = AnotherRecursive(y + 1, x, currentSum);

            result += Math.Min(rightPath, downPath);

            return result;
        }

    }
}
