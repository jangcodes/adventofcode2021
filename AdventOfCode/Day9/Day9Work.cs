using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day9
{
    internal class Day9Work
    {
        private static string[] input;
        private static int RowTotal => input.Length;
        private static int ColTotal => input[0].Length;
        private static readonly List<(int x, int y)> usedCoordinates = new();

        public static async Task Execute()
        {
            input = await File.ReadAllLinesAsync(@"Day9\Input.txt");

            var allLowPoints = Part1();
            Part2(allLowPoints);
        }

        private static (int up, int down, int left, int right) GetSurroundingNumbers(int x, int y)
        {
            int up = (y > 0) ? input[y - 1][x] - '0' : 10;
            int down = (y < RowTotal - 1) ? input[y + 1][x] - '0' : 10;
            int left = (x > 0) ? input[y][x - 1] - '0' : 10;
            int right = (x < ColTotal - 1) ? input[y][x + 1] - '0' : 10;

            return (up, down, left, right);
        }

        private static List<(int x, int y)> Part1()
        {
            int lowNumbersTotal = 0;
            List<(int x, int y)> lowPoints = new();

            for (int y = 0; y < RowTotal; y++)
            {
                for (int x = 0; x < ColTotal; x++)
                {
                    var (up, down, left, right) = GetSurroundingNumbers(x, y);

                    int currentNumber = input[y][x] - '0';

                    if (currentNumber < up && currentNumber < down && currentNumber < left && currentNumber < right)
                    {
                        lowNumbersTotal += (currentNumber + 1);
                        lowPoints.Add((x, y));
                    }
                }
            }

            Console.WriteLine($"Part 1 Answer: {lowNumbersTotal}");

            return lowPoints;
        }

        private static void Part2(List<(int x, int y)> lowPoints)
        {
            List<int> basinPoints = new();

            foreach (var (x, y) in lowPoints)
            {
                basinPoints.Add(CountBasinRecursive(x, y));
            }

            basinPoints.Sort();
            basinPoints.Reverse();
            
            Console.WriteLine($"Part 2 Answer: {basinPoints.Take(3).Aggregate((i, j) => i * j)}");
        }

        private static int CountBasinRecursive(int x, int y)
        {
            if (usedCoordinates.Any(c => c == (x, y))) return 0;

            int count = 1;
            var (up, down, left, right) = GetSurroundingNumbers(x, y);

            int currentNumber = input[y][x] - '0';

            if (up < 9 && up > currentNumber)
            {
                count += CountBasinRecursive(x, y - 1);
            }

            if (down < 9 && down > currentNumber)
            {
                count += CountBasinRecursive(x, y + 1);
            }

            if (left < 9 && left > currentNumber)
            {
                count += CountBasinRecursive(x - 1, y);
            }

            if (right < 9 && right > currentNumber)
            {
                count += CountBasinRecursive(x + 1, y);
            }

            usedCoordinates.Add((x, y));

            return count;
        }

    }
}
