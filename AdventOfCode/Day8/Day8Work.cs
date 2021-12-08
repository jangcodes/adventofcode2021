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
            int[] listOfNumbers = new int[] { 2, 3, 4, 7 };
            int sum = input.Select(line => line[line.IndexOf('|')..].Split(" ")).Sum(x => x.Count(x => listOfNumbers.Contains(x.Length)));
            Console.WriteLine($"Answer: {sum}");
        }

        private static void Part2(string[] input)
        {
            int result = 0;

            foreach (string line in input)
            {
                var splitInput = line.Split(" | ");
                var signals = splitInput[0].Split(' ');

                string[] digits = new string[10];

                digits[1] = signals.Single(x => x.Length == 2);
                digits[4] = signals.Single(x => x.Length == 4);
                digits[7] = signals.Single(x => x.Length == 3);
                digits[8] = signals.Single(x => x.Length == 7);

                digits[6] = GetSixFromOne(signals, digits[1].ToCharArray());
                digits[9] = GetNineFromFour(signals, digits[4].ToCharArray());
                digits[0] = signals.Single(x => x.Length == 6 && x != digits[9] && x != digits[6]);

                digits[3] = GetThreeFromOne(signals, digits[1].ToCharArray());
                digits[5] = GetFiveFromFour(signals.Where(x => x != digits[3]).ToArray(), digits[4].ToCharArray());
                digits[2] = signals.Single(x => x.Length == 5 && x != digits[3] && x != digits[5]);

                for (int i = 0; i <= 9; i++)
                {
                    digits[i] = string.Concat(digits[i].OrderBy(c => c));
                }

                string currentNumber = "";
                foreach (var item in splitInput[1].Split(' '))
                {
                    var sortedString = string.Concat(item.OrderBy(c => c));
                    var findDigit = digits.ToList().IndexOf(sortedString).ToString();
                    currentNumber += findDigit;
                }

                result += Convert.ToInt32(currentNumber);
            }

            Console.WriteLine($"Part 2 Answer: {result}");
        }

        private static string GetFiveFromFour(string[] input, char[] four) => input.Single(x => x.Length == 5 && four.Count(y => x.Contains(y)) == 3);
        private static string GetThreeFromOne(string[] input, char[] one) => input.Single(x => x.Length == 5 && one.All(y => x.Contains(y)));
        private static string GetSixFromOne(string[] input, char[] one) => input.Single(x => x.Length == 6 && one.Count(y => x.Contains(y)) == 1);
        private static string GetNineFromFour(string[] input, char[] four) => input.Single(x => x.Length == 6 && four.All(y => x.Contains(y)));
    }
}
