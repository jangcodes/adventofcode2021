using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day9
{
    internal class Day9Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day9\Input.txt");

            Part1(input);
            Part2(input);
        }


        private static void Part1(string[] input)
        {
            int columnTotal = input[0].Length;
            int rowTotal = input.Length;

            List<List<int>> grid = new();

            foreach (var item in input)
            {
                grid.Add(item.Select(s => Convert.ToInt32(s)).ToList());
            }


            List<int> lowNumbers = new();

            foreach (var row in grid)
            {
                foreach (var column in grid)
                {
                }
            }

        }

        private static void Part2(string[] input)
        {

        }
    }
}
