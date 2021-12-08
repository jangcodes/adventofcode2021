using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day8
{
    internal class Day8Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day8\Input.txt");

            Part1(input);
        }

        private static void Part1(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                var secondPart = line[line.IndexOf('|')..].Split(" ");
                sum += secondPart.Count(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7);
            }
            Console.WriteLine($"Answer: {sum}");
        }
    }
}
