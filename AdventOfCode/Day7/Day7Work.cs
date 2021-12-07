using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day7
{
    internal class Day7Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day7\Input.txt");
            List<int> crabsPositions = input[0].Split(',').Select(_ => Convert.ToInt32(_)).ToList();
            crabsPositions.Sort();

            Part1(crabsPositions);
            Part2(crabsPositions);
        }

        private static void Part1(List<int> crabs)
        {
            var median = crabs[crabs.Count / 2];
            int sum = crabs.Sum(c => Math.Abs(c - median));
            Console.WriteLine($"Part 1 Answer: {sum}");
        }

        private static void Part2(IEnumerable<int> crabs)
        {
            var avgPosition = crabs.Average();

            var lowResult = Calculation(crabs, Math.Floor(avgPosition));
            var highResult = Calculation(crabs, Math.Ceiling(avgPosition));

            var finalResult = Math.Min(highResult, lowResult);
            Console.WriteLine($"Part 2 Answer: {finalResult}");
        }

        private static double Calculation(IEnumerable<int> crabs, double position)
        {
            return crabs.Select(c => Math.Abs(c - position)).Sum(n => ((n * n) + n) / 2);
        }
    }
}
