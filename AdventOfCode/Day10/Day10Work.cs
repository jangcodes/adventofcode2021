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

            var incompleteList = Part1(input);
            Part2(incompleteList);
        }

        private static List<string> Part1(string[] input)
        {
            List<string> incomplete = new();

            int sum = 0;
            foreach (var line in input)
            {
                int illegalCharScore = CheckSyntax(line);

                if (illegalCharScore == 0)
                {
                    incomplete.Add(line);
                }
                else
                {
                    sum += illegalCharScore;
                }
            }

            Console.WriteLine($"Part 1 Answer: {sum}");

            return incomplete;
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

        private static void Part2(List<string> input)
        {

        }
    }
}
