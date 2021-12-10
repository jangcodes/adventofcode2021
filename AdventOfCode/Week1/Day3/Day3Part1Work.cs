using System;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    internal class Day3Part1Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("=============Day 3 Part 1=============");

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

            string gammaRateBinary = "";
            string epsilonRateBinary = "";

            for (int i = 0; i < 12; i++)
            {
                if (countOfZero[i] > countOfOne[i])
                {
                    gammaRateBinary += "0";
                    epsilonRateBinary += "1";
                }
                else if (countOfZero[i] < countOfOne[i])
                {
                    gammaRateBinary += "1";
                    epsilonRateBinary += "0";
                }
            }

            Console.WriteLine("Gamma Rate: " + gammaRateBinary);
            Console.WriteLine("Epsilon Rate: " + epsilonRateBinary);
            Console.WriteLine("Result: " + (Convert.ToInt32(gammaRateBinary, 2) * Convert.ToInt32(epsilonRateBinary, 2)));
        }
    }
}
