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
        public static async Task Execute()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            string[] input = await File.ReadAllLinesAsync(@"Week2\Day14\Example.txt");

            var text = input[0];
            var instructions = input[2..].Select(x => x.Split(" -> "));

            const int steps = 5;

            for (int s = 0; s < steps; s++)
            {
                Dictionary<int, string> applicableInstruction = new Dictionary<int, string>();

                foreach (var i in instructions)
                {
                    var allIndex = AllIndexOf(text, i[0]);

                    foreach (var a in allIndex)
                    {
                        applicableInstruction.Add(a, i[1]);
                    }
                }

                int offset = 0;
                foreach (var index in applicableInstruction.OrderBy(x => x.Key))
                { 
                    text = text.Insert(index.Key + 1 + offset, index.Value);
                    offset++;
                }
            }

            var grouped = text.ToCharArray().GroupBy(_ => _).OrderBy(x => x.Count());

            var top = grouped.First();
            var bottom = grouped.Last();

            

            Console.WriteLine($"Part 1 Answer: {bottom.Count() - top.Count()} ");

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
