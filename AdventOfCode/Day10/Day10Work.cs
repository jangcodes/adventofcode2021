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

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day10\Example.txt");

            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {

        }

        private static void CheckSyntaxRecursive(string line)
        {
            Dictionary<char, int> scoresPerChar = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };

            int totalScore = 0;

            var OpenBrackets = CompleteBrackets.Select(c => c.Key);
            var CloseBrackets = CompleteBrackets.Select(c => c.Value);
            if (line.Length > 1 && OpenBrackets.Contains(line[0]) && !CloseBrackets.Contains(line[1]))
            {
                CheckSyntaxRecursive(line[1..]);
            }

            var correctClosing = CompleteBrackets[line[0]];
            if (correctClosing != line[1])
            {

            }
        }

        private static void Part2(string[] input)
        {

        }

    }
}
