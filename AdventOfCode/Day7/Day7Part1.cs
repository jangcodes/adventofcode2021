using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7
{
    internal class Day7Part1
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day7\Input.txt");
            List<int> horizontalPosition = input[0].Split(',').Select(_ => Convert.ToInt32(_)).ToList();
            horizontalPosition.Sort();
            var median = horizontalPosition[horizontalPosition.Count / 2];

            int sum = 0;
            foreach(var item in horizontalPosition)
                sum += Math.Abs(item - median);

            Console.WriteLine($"Answer: {sum}");
        }
    }
}
