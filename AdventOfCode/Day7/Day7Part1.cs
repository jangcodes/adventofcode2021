using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day7
{
    internal class Day7Part1
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

            int sum = 0;
            foreach (var item in crabs)
                sum += Math.Abs(item - median);

            Console.WriteLine($"Part 1 Answer: {sum}");
        }

        private static void Part2(List<int> crabs)
        {
            int maxPosition = crabs.Max();

            List<int> eachPossibility = new ();
            for (int currentPosition = 0; currentPosition <= maxPosition; currentPosition++)
            {
                int sum = 0;
                foreach (var individualCrabPosition in crabs)
                {
                    var difference = Math.Abs(individualCrabPosition - currentPosition);
                    sum += ((difference * difference) + difference) / 2;
                }
                eachPossibility.Add(sum);
            }

            Console.WriteLine($"Part 2 Answer: {string.Join(',', eachPossibility.Min())}");
        }
    }
}
