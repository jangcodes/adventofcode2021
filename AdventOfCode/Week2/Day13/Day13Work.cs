using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day13
{
    internal class Day13Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day13\Example.txt");

            IEnumerable<int[]> coordinates = input.Where(x => x.Contains(',')).Select(x => x.Split(',').Select(y => Convert.ToInt32(y)).ToArray());

            int Xmax = coordinates.Select(x => x[0]).Max();
            int Ymax = coordinates.Select(x => x[1]).Max();

            int[,] grid = new int[Xmax, Ymax];

            foreach (var c in coordinates)
            {
                grid[c[0], c[1]]++;
            }


        }
    }
}
