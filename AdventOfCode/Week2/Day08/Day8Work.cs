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
            int sum = input.Select(line => line[line.IndexOf('|')..].Split(" ")).Sum(x => x.Count(y => listOfNumbers.Contains(y.Length)));
            Console.WriteLine($"Answer: {sum}");
        }

        private static void Part2(string[] input)
        {
            int result = 0;

            foreach (string line in input)
            {
                var signals = line.Split(" | ").Select(si => si.Split(' ').Select(i => string.Concat(i.OrderBy(c => c)))).ToArray();

                string[] digits = new string[10];

                digits[1] = signals[0].Single(x => x.Length == 2); // #1
                digits[4] = signals[0].Single(x => x.Length == 4); // #4
                digits[7] = signals[0].Single(x => x.Length == 3); // #7
                digits[8] = signals[0].Single(x => x.Length == 7); // #8

                var sixNineZero = signals[0].Where(x => x.Length == 6);                         // #6, #9 and #0 have 6 characters
                digits[6] = sixNineZero.Single(x => digits[1].Count(y => x.Contains(y)) == 1);  // #6 is the only one that shares 1 character with #1
                digits[9] = sixNineZero.Single(x => digits[4].All(y => x.Contains(y)));         // #9 is the only one that contains all characters from #4
                digits[0] = sixNineZero.Single(x => x != digits[9] && x != digits[6]);          // If it's not #6 or #9, it is #0

                var threeFiveTwo = signals[0].Where(x => x.Length == 5);                                            // #3, #5 and #2 have 5 characters
                digits[3] = threeFiveTwo.Single(x => digits[1].All(y => x.Contains(y)));                            // #3 is the only one that contains all charcter from #1
                digits[5] = threeFiveTwo.Single(x => x != digits[3] && digits[4].Count(y => x.Contains(y)) == 3);   // Without #3, #5 is the only one that shares 3 character with #4
                digits[2] = threeFiveTwo.Single(x => x != digits[3] && x != digits[5]);                             // If it's not #3 or #5, it's 2 

                string currentNumber = "";
                foreach (var item in signals[1])
                    currentNumber += Array.IndexOf(digits, item);

                result += Convert.ToInt32(currentNumber);
            }

            Console.WriteLine($"Part 2 Answer: {result}");
        }
    }
}
