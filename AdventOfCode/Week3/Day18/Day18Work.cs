using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day18
{
    internal class Day18Work
    {
        private const string PairRegexPattern = @"\[((\d+),(\d+))\]";

        public static async Task Execute()
        {
            Console.WriteLine("========Day 18========");
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day18\Input.txt");

            var snailNumber = input[0];
            for (int i = 1; i < input.Length; i++) snailNumber = ComputeSnailNumbers($"[{snailNumber},{input[i]}]");

            Console.WriteLine($"Part 1 Answer: {MagCalculator(snailNumber)}");

            long largestMag = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (i != j)
                    {
                        var result = ComputeSnailNumbers($"[{input[i]},{input[j]}]");
                        var magResult = MagCalculator(result);
                        if (magResult > largestMag) largestMag = magResult;
                    }
                }
            }

            Console.WriteLine($"Part 2 Answer: {largestMag}");
        }

        private static string ComputeSnailNumbers(string beforeCompute)
        {
            string data = beforeCompute;            
            Regex pairRegex = new(PairRegexPattern);

            while (true)
            {
                Match? number = Regex.Matches(data, @"\d+").FirstOrDefault(m => Convert.ToInt32(m.Value) > 9);
                Match? pair = null;

                var rawPairs = pairRegex.Matches(data);
                foreach(Match p in rawPairs)
                {
                    int openBracketCount = 0;
                    int closeBracketCount = 0;
                    for (int j = 0; j < p.Index; j++)
                    {
                        if (data[j] == '[') openBracketCount++;
                        if (data[j] == ']') closeBracketCount++;
                    }

                    int remainingOpened = openBracketCount - closeBracketCount;
                    if (remainingOpened >= 4)
                    {
                        pair = p;
                        break;
                    }
                }

                if (pair != null)
                {
                    string pairValue = pair.Value;
                    var leftPart = data.Substring(0, pair.Index);
                    var rightPart = data.Substring(pair.Index + pairValue.Length, data.Length - (pair.Index + pairValue.Length));

                    var leftNumberMatch = Regex.Match(leftPart, @"\d+", RegexOptions.RightToLeft);
                    var rightNumberMatch = Regex.Match(rightPart, @"\d+");

                    var (left, right) = GetNumberFromPair(pairValue);

                    if (rightNumberMatch.Success)
                    {
                        var originalRightNumber = Convert.ToInt32(rightNumberMatch.Value);
                        var newRightNumber = right + originalRightNumber;

                        var indexFound = rightNumberMatch.Index + pair.Index + pairValue.Length;

                        data = data.Remove(indexFound, originalRightNumber.ToString().Length);
                        data = data.Insert(indexFound, newRightNumber.ToString());
                    }

                    if (leftNumberMatch.Success)
                    {
                        var originalLeftNumber = Convert.ToInt32(leftNumberMatch.Value);
                        var newLeftNumber = left + originalLeftNumber;

                        var indexFound = leftNumberMatch.Index;

                        data = data.Remove(indexFound, originalLeftNumber.ToString().Length); 
                        data = data.Insert(indexFound, newLeftNumber.ToString());
                    }

                    pair = pairRegex.Match(data, pair.Index);

                    data = data.Remove(pair.Index, pairValue.Length);
                    data = data.Insert(pair.Index, "0");
                }
                else if (number != null)
                {
                    var over10 = Convert.ToDecimal(number.Value);
                    var left = Math.Floor(over10 / 2);
                    var right = Math.Ceiling(over10 / 2);

                    var newPair = $"[{left},{right}]";

                    data = data.Remove(number.Index, number.Value.ToString().Length); 
                    data = data.Insert(number.Index, newPair);
                }

                if (number == null && pair == null) break;
            }

            return data;
        }

        private static long MagCalculator(string snailNumber)
        {
            string currentSnailNumber = snailNumber;
            var matchedPair = Regex.Match(currentSnailNumber, PairRegexPattern);

            while (matchedPair.Success)
            {
                var (left, right) = GetNumberFromPair(matchedPair.Value);
                var mag = (left * 3) + (right * 2);
                currentSnailNumber = currentSnailNumber.Remove(matchedPair.Index, matchedPair.Length).Insert(matchedPair.Index, mag.ToString());
                matchedPair = Regex.Match(currentSnailNumber, PairRegexPattern);
            }

            return Convert.ToInt64(currentSnailNumber);
        }

        private static (long left, long right) GetNumberFromPair(string pair)
        {
            var comma = pair.IndexOf(',');

            long left = Convert.ToInt32(pair.Substring(1, comma - 1));
            long right = Convert.ToInt32(pair.Substring(comma + 1, pair.Length - comma - 2));

            return (left, right);
        }
    }
}
