using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    internal class Day3Part1Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("================================");
            Console.WriteLine("=============Day 3 Part 1=======");
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
            List<int> gammaRateBinary = new List<int>();

            double epsilonRate = 0;
            List<int> epsilonRateBinary = new List<int>();

            for (int i = 0; i < 12; i++)
            {
                int powNumber = 11 - i;
                if (countOfZero[i] == countOfOne[i])
                {
                    gammaRateBinary.Add(1);
                    epsilonRateBinary.Add(1);
                }
                else if (countOfZero[i] > countOfOne[i])
                {
                    epsilonRate = epsilonRate + Math.Pow(2, powNumber);
                    gammaRateBinary.Add(0);
                    epsilonRateBinary.Add(1);
                }
                else if (countOfZero[i] < countOfOne[i])
                {
                    gammaRate = gammaRate + Math.Pow(2, powNumber);
                    gammaRateBinary.Add(1);
                    epsilonRateBinary.Add(0);
                }
            }

            Console.WriteLine("Gamma Rate: " + string.Join("", gammaRateBinary));
            Console.WriteLine("Epsilon Rate: " + string.Join("", epsilonRateBinary));

            Console.WriteLine("Result: " + (gammaRate * epsilonRate));


            List<string> oxygenGenRat = input.ToList();
            List<string> co2GenRat = input.ToList();

            for (int i = 0; i < 12; i++)
            {
                if (oxygenGenRat.Count > 1)
                {
                    oxygenGenRat = oxygenGenRat.Where(x => Convert.ToInt32(x[i].ToString()) == gammaRateBinary[i]).ToList();
                }

                if (co2GenRat.Count > 1)
                {
                    co2GenRat = co2GenRat.Where(x => Convert.ToInt32(x[i].ToString()) == epsilonRateBinary[i]).ToList();
                }
            }

            Console.WriteLine("Oxygen: " + oxygenGenRat.First() + " " + GetDoubleFromStringBinary(oxygenGenRat.First()));
            Console.WriteLine("CO2: " + co2GenRat.First() + " " + GetDoubleFromStringBinary(co2GenRat.First()));
        }

        private static double GetDoubleFromStringBinary(string bin)
        {
            double result = 0;

            for (int i = 0; i < 12; i++)
            {
                int powNumber = 11 - i;
                if (bin[i] == '1')
                {
                    result += Math.Pow(2, powNumber);
                }
            }

            return result;
        }
    }
}
