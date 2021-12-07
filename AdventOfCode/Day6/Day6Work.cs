using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6
{
    internal class Day6Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Day6\Input.txt");

            var allFish = input[0].Split(',').Select(_ => Convert.ToInt32(_));

            var firstAnswer = CountFish(allFish, 80);
            var secondAnswer = CountFish(allFish, 256);

            //decimal oneResult = SimulateSingleFish(1, 256);
            //decimal twoResult = SimulateSingleFish(2, 256);
            //decimal threeResult = SimulateSingleFish(3, 256);
            //decimal fourResult = SimulateSingleFish(4, 256);
            //decimal fiveResult = SimulateSingleFish(5, 256);

            //var finalResult = (allFish.Count(_ => _ == 1) * oneResult) +
            //    (allFish.Count(_ => _ == 2) * twoResult) +
            //    (allFish.Count(_ => _ == 3) * threeResult) +
            //    (allFish.Count(_ => _ == 4) * fourResult) +
            //    (allFish.Count(_ => _ == 5) * fiveResult);

            Console.WriteLine($"Final Answer: {firstAnswer}");
            Console.WriteLine($"Final Answer: {secondAnswer}");
        }

        private static long CountFish(IEnumerable<int> input, int generations)
        {
            var fish = new long[9];
            foreach (var age in input)
                fish[age]++;

            for (var i = 0; i < generations; ++i)
                fish[(i + 7) % 9] += fish[i % 9];

            return fish.Sum();
        }

        private static decimal SimulateSingleFish(byte startDay, int totalDays)
        {
            const int splitDays = 200;
            
            List<byte> allFish = new() { startDay };
            int numberOfFish = allFish.Count();
            var result = ProcessFish(allFish, splitDays);

            var splitPieces = Chunk(result, 1000000);

            int restOfDays = totalDays - splitDays;

            List<Task> tasks = new List<Task>();

            List<List<byte>> taskResult = new List<List<byte>>();

            foreach (var item in splitPieces)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    var result = ProcessFish(item, restOfDays);
                    taskResult.Add(result);
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            return taskResult.Select(x => Convert.ToDecimal(x.Count)).Sum();
        }

        private static IEnumerable<IEnumerable<T>> Chunk<T>(IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }

        private static List<byte> ProcessFish(IEnumerable<byte> initialFish, int days)
        {
            List<byte> resultFish = initialFish.ToList();

            int numberOfFish = initialFish.Count();

            for (int i = 1; i <= days; i++)
            {
                for (int j = 0; j < numberOfFish; j++)
                {
                    if (resultFish[j] == 0)
                    {
                        resultFish.Add(8); // Add Fish
                        resultFish[j] = 7; // Reset Fish
                    }
                    resultFish[j]--;
                }

                numberOfFish = resultFish.Count();

                // Console.WriteLine($"Day {i}: {numberOfFish}");
            }

            return resultFish;
        }
    }
}
