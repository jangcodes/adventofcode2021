using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day14
{
    internal class Day14Work
    {
        private const int steps = 10;

        public static async Task Execute()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            string[] input = await File.ReadAllLinesAsync(@"Week2\Day14\Input.txt");

            var text = input[0];
            var instructions = input[2..].Select(x => x.Split(" -> "));

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
                List<string[]> instructionFound = new();

                foreach (var ins in instructions)
                {
                    if (possibleCombination.ContainsKey(ins[0]))
                    {
                        instructionFound.Add(ins);
                    }
                }

                List<int> removeCombo = new ();
                Dictionary<string, long> newCombination = new ();

                foreach (var ins in instructionFound)
                {
                    var currentCount = possibleCombination[ins[0]];

                    var newCombo1 = ins[0][0] + ins[1];
                    var newCombo2 = ins[1] + ins[0][1];

                    if (newCombination.ContainsKey(newCombo1))
                    {
                        newCombination[newCombo1] += currentCount;
                    }
                    else
                    {
                        newCombination.Add(newCombo1, currentCount);
                    }

                    if (newCombination.ContainsKey(newCombo2))
                    {
                        newCombination[newCombo2] += currentCount;
                    }
                    else
                    {
                        newCombination.Add(newCombo2, currentCount);
                    }


                    if (charCounter.ContainsKey(ins[1][0]))
                    {
                        charCounter[ins[1][0]] += currentCount;
                    }
                    else
                    {
                        charCounter.Add(ins[1][0], currentCount);                    
                    }


                    possibleCombination.Remove(ins[0]);

                }


                possibleCombination = possibleCombination.Concat(newCombination).ToDictionary(x => x.Key, x => x.Value);
                
            }

            var sortedOutcome = charCounter.OrderBy(x => x.Value);

            var smallest = sortedOutcome.First().Value;
            var largest = sortedOutcome.Last().Value;

            Console.WriteLine($"Part 1 Answer: {largest - smallest}");

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }


        public static IList<int> AllIndexOf(string text, string str)
        {
            IList<int> allIndexOf = new List<int>();
            int index = text.IndexOf(str);
            while (index != -1)
            {
                allIndexOf.Add(index);
                index = text.IndexOf(str, index + 1);
            }
            return allIndexOf;
        }
    }
}
