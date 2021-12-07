using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day6
{
    internal class Day6Part1
    {
        public static async Task Execute()
        {
            const ushort days = 256;
            const int bucketLimit = 1000000;
            string[] input = await File.ReadAllLinesAsync(@"Day6\Input.txt");

            // List<int> allFish = input[0].Split(',').Select(_ => Convert.ToInt32(_)).ToList();
            List<byte> allFish = new() { 1 };

            List<List<byte>> bucketOfFish = new() { allFish };

            for (int i = 1; i <= days; i++)
            {

                int numberOfBuckets = bucketOfFish.Count;
                List<Task> tasks = new List<Task>();
                for (int k = 0; k < numberOfBuckets; k++)
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        int numberOfFishInBucket = bucketOfFish[k].Count;

                        List<byte> newBucket = new();

                        List<List<byte>> bucketsOfBucket = new();

                        
                        for (int j = 0; j < numberOfFishInBucket; j++)
                        {


                            if (bucketOfFish[k][j] == 0)
                            {
                                if (bucketOfFish[k].Count > bucketLimit)
                                {
                                    if (newBucket.Count > bucketLimit)
                                    {
                                        bucketsOfBucket.Add(newBucket);
                                        newBucket = new();
                                    }

                                    newBucket.Add(8);
                                }
                                else
                                {
                                    bucketOfFish[k].Add(8); // Add Fish

                                }
                                bucketOfFish[k][j] = 7; // Reset Fish

                            }
                            bucketOfFish[k][j]--;

                            
                        }

                        if (newBucket.Any())
                        {
                            bucketOfFish.Add(newBucket);
                        }

                        if (bucketsOfBucket.Any())
                        {
                            bucketOfFish.AddRange(bucketsOfBucket);
                        }
                    });

                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray());

                Console.WriteLine($"Day {i}: {bucketOfFish.Count} buckets");
            }




            Console.WriteLine($"Answer: {bucketOfFish.Select(_ => _.Count).Sum()}");
        }
    }
}
