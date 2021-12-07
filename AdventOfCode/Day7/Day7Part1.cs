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

            var most = horizontalPosition.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();


            int sum = 0;

            foreach(var item in horizontalPosition)
            {
                sum += Math.Abs(item - most);
            }


            Console.WriteLine($"Answer: {sum}");


        }
    }
}
