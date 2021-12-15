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
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day14\Example.txt");

            var row = input.Length;
            var col = input[0].Length;

            List<int[]> grid = input.Select(x => x.Split().Select(y => Convert.ToInt32(y)).ToArray()).ToList();

        }
    }
}
