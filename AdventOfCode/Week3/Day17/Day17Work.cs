using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day17
{
    internal class Day17Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day17\Input.txt");

            var xCoordinates = input[0].Replace("x=", "").Split("..");
            var yCoordinates = input[1].Replace("y=","").Split("..").Select(x => Convert.ToInt32(x)).ToArray();


            (int y, int x) startingPoint = new(0, 0);


            var n = Math.Abs(yCoordinates[0]) - 1;

            var result = (n * (n + 1)) / 2;

            Console.WriteLine($"Part 1 Answer: {result}");


        }
    }
}
