using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day14
{
    internal class Day14Work
    {
        public static async Task Execute()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            string[] input = await File.ReadAllLinesAsync(@"Week2\Day14\Input.txt");

            var text = input[0];
            var instructions = input[2..].Select(x => x.Split(" -> "));

            Console.WriteLine($"Part 1 Answer: {Compute(text, instructions, 10)}");
            Console.WriteLine($"Part 2 Answer: {Compute(text, instructions, 40)}");

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        private static long Compute(string text, IEnumerable<string[]> instructions, int steps)
        {
            Dictionary<string, long> possibleCombination = new();

            Dictionary<char, long> charCounter = text.ToCharArray().GroupBy(_ => _).ToDictionary(x => x.Key, x => Convert.ToInt64(x.Count()));

            for (int i = 0; i < text.Length - 1; i++)
            {
                string combo = text.Substring(i, 2);

                if (possibleCombination.ContainsKey(combo))
                {
                    possibleCombination[combo]++;
                }
                else
                {
                    possibleCombination.Add(combo, 1L);
                }
            }

            for (int s = 0; s < steps; s++)
            {
                var pairFound = instructions.Where(x => possibleCombination.ContainsKey(x[0]));

                List<int> removeCombo = new();
                Dictionary<string, long> newCombination = new();

                foreach (var pair in pairFound)
                {
                    var currentCount = possibleCombination[pair[0]];
                    string[] insCombo = { pair[0][0] + pair[1], pair[1] + pair[0][1] };

                    foreach(var c in insCombo)
                    {
                        if (newCombination.ContainsKey(c))
                        {
                            newCombination[c] += currentCount;
                        }
                        else
                        {
                            newCombination.Add(c, currentCount);
                        }
                    }

                    if (charCounter.ContainsKey(pair[1][0]))
                    {
                        charCounter[pair[1][0]] += currentCount;
                    }
                    else
                    {
                        charCounter.Add(pair[1][0], currentCount);
                    }

                    possibleCombination.Remove(pair[0]);
                }

                possibleCombination = possibleCombination.Concat(newCombination).ToDictionary(x => x.Key, x => x.Value);
            }

            var sortedOutcome = charCounter.Select(x => x.Value);
            return sortedOutcome.Max() - sortedOutcome.Min();
        }
    }
}
