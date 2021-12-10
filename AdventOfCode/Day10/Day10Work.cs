using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
    internal class Day10Work
    {
        private static readonly Dictionary<char, char> CompleteBrackets = new() { { '{', '}' }, { '(', ')' }, { '[', ']' }, { '<', '>' } };
        private static readonly Dictionary<char, int> scoresPerChar = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day10\Input.txt");

            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            int sum = 0;
            foreach (var line in input)
            {
                sum += CheckSyntax(line);
            }

            Console.WriteLine($"Part 1 Answer: {sum}");
        }

        private static int CheckSyntax(string line)
        {
            var OpenBrackets = CompleteBrackets.Select(c => c.Key);
            var CloseBrackets = CompleteBrackets.Select(c => c.Value);

            List<char> tempSolution = new();
            foreach (var c in line)
            {
                if (OpenBrackets.Contains(c))
                {
                    tempSolution.Add(c);
                }
                else if (CloseBrackets.Contains(c))
                {
                    if (CompleteBrackets[tempSolution.Last()] == c)
                    {
                        tempSolution.RemoveAt(tempSolution.Count - 1);
                    }
                    else
                    {
                        return scoresPerChar[c];
                    }

                }
            }

            return 0;
        }

        private static void Part2(string[] input)
        {

        }

    }
}
