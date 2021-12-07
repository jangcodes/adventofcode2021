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

            List<byte> allFish = input[0].Split(',').Select(_ => Convert.ToByte(_)).ToList();

            decimal oneResult = SimulateSingleFish(1, 256, 200);
            decimal twoResult = SimulateSingleFish(2, 256, 200);
            decimal threeResult = SimulateSingleFish(3, 256, 200);
            decimal fourResult = SimulateSingleFish(4, 256, 200);
            decimal fiveResult = SimulateSingleFish(5, 256, 200);

            var finalResult = (allFish.Count(_ => _ == 1) * oneResult) +
                (allFish.Count(_ => _ == 2) * twoResult) +
                (allFish.Count(_ => _ == 3) * threeResult) +
                (allFish.Count(_ => _ == 4) * fourResult) +
                (allFish.Count(_ => _ == 5) * fiveResult);

            Console.WriteLine($"Final Answer: {finalResult}");
        }

        private static decimal SimulateSingleFish(byte startDay, int totalDays, int splitDays)
        {
            List<byte> allFish = new() { startDay };

            int numberOfFish = allFish.Count();

            var result = ProcessFish(allFish, splitDays);

            Console.WriteLine($"Answer: {result.Count}");

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
