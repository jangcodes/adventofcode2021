using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
    internal class Day10Work
    {
        private static readonly Dictionary<char, char> CompleteBrackets = new() { { '{', '}' }, { '(', ')' }, { '[', ']' }, { '<', '>' } };
        private static readonly Dictionary<char, int> IllegalCharScore = new() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private static readonly Dictionary<char, int> IncompleteCharScores = new() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day10\Input.txt");

            int totalIllegalCharScore = 0;
            List<long> incompleteScoreList = new();

            foreach (var line in input)
            {
                int illegalCharScore = 0;
                
                var OpenBrackets = CompleteBrackets.Select(c => c.Key);
                var CloseBrackets = CompleteBrackets.Select(c => c.Value);

                List<char> cleanedUpLine = new();
                foreach (var c in line)
                {
                    if (OpenBrackets.Contains(c))
                    {
                        cleanedUpLine.Add(c);
                    }
                    else if (CloseBrackets.Contains(c))
                    {
                        if (CompleteBrackets[cleanedUpLine.Last()] != c)
                        {
                            illegalCharScore = IllegalCharScore[c];
                            totalIllegalCharScore += illegalCharScore;
                            break;
                        }
                        cleanedUpLine.RemoveAt(cleanedUpLine.Count - 1);
                    }
                }

                if (illegalCharScore == 0)
                {
                    long incompleteScore = 0;
                    cleanedUpLine.Reverse();

                    foreach (var c in cleanedUpLine)
                        incompleteScore = (incompleteScore * 5) + IncompleteCharScores[c];

                    incompleteScoreList.Add(incompleteScore);
                }
            }

            incompleteScoreList.Sort();            

            Console.WriteLine($"Part 1 Answer: {totalIllegalCharScore}");
            Console.WriteLine($"Part 2 Answer: {incompleteScoreList[incompleteScoreList.Count / 2]}");
        }
    }
}
