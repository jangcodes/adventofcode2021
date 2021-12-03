using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    internal class Day3Part1Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("================================");
            Console.WriteLine("=============Day 2==============");
            Console.WriteLine("================================");

            string[]? input = await File.ReadAllLinesAsync(@"Day3\Input.txt");

            int[] countOfZero = new int[12];
            int[] countOfOne = new int[12];

            foreach (string line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '0')
                    {
                        countOfZero[i]++;
                    }
                    else if (line[i] == '1')
                    {
                        countOfOne[i]++;
                    }
                }
            }

            Console.Write("Zero Counts: ");
            foreach (var i in countOfZero)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            Console.Write("One Counts: ");
            foreach (var i in countOfOne)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            double gammaRate = 0;
            double epsilonRate = 0;

            for (int i = 0; i < 12; i++)
            {
                int powNumber = 11 - i;
                if (countOfZero[i] > countOfOne[i])
                {
                    epsilonRate = epsilonRate + Math.Pow(2, powNumber);
                }
                else if (countOfZero[i] < countOfOne[i])
                {
                    gammaRate = gammaRate + Math.Pow(2, powNumber);
                }
            }

            Console.WriteLine("Gamma Rate: " + gammaRate);
            Console.WriteLine("Epsilon Rate: " + epsilonRate);

            Console.WriteLine("Result: " + (gammaRate * epsilonRate));
        }
    }
}
