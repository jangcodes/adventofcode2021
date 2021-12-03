using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;
using System;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Day1Work.Execute();
            Console.WriteLine();

            await Day2Work.Execute();
            Console.WriteLine();

            await Day3Part1Work.Execute();
            Console.WriteLine();

            await Day3Part2Work.Execute();
            Console.ReadKey();
        }
    }
}
