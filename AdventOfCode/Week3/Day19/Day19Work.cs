using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day19
{
    internal class Day19Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("========Day 19========");
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day19\Input.txt");
        }
    }
}
