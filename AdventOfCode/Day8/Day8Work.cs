using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day8
{
    internal class Day8Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day8\Input.txt");

            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            int sum = 0;
            foreach (string line in input)
            {
                var secondPart = line[line.IndexOf('|')..].Split(" ");
                sum += secondPart.Count(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7);
            }
            Console.WriteLine($"Answer: {sum}");
        }

        private static void Part2(string[] input)
        {
            foreach (string line in input)
            {
                char[] singleDigit = new char[7] { '1', '1', '1', '1', '1', '1', '1' };

                var splitInput = line.Split(" | ");

                var signals = splitInput[0].Split(' ');
                var findDigitEight = signals.FirstOrDefault(x => x.Length == 7);
                if (findDigitEight != null)
                {
                    singleDigit = findDigitEight.ToArray();
                }

                DisplayDigit(singleDigit);
            }
        }

        private static void DisplayDigit(char[] s)
        {
            Console.WriteLine($" {s[0]}{s[0]}{s[0]}{s[0]} ");
            Console.WriteLine($"{s[1]}    {s[2]}");
            Console.WriteLine($"{s[1]}    {s[2]}");
            Console.WriteLine($" {s[3]}{s[3]}{s[3]}{s[3]} ");
            Console.WriteLine($"{s[4]}    {s[5]}");
            Console.WriteLine($"{s[4]}    {s[5]}");
            Console.WriteLine($" {s[6]}{s[6]}{s[6]}{s[6]} ");
        }
    }
}
