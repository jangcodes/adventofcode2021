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
            const int days = 80;
            string[] input = await File.ReadAllLinesAsync(@"Day6\Input.txt");

            List<int> allFish = input[0].Split(',').Select(_ => Convert.ToInt32(_)).ToList();
            int numberOfFish = allFish.Count;

            for (int i = 1; i <= days; i++)
            {
                for (int j = 0; j < numberOfFish; j++)
                {
                    if (allFish[j] == 0)
                    {
                        allFish.Add(8); // Add Fish
                        allFish[j] = 7; // Reset Fish
                    }
                    allFish[j]--;
                }
                numberOfFish = allFish.Count;                
            }

            Console.WriteLine($"Answer: {numberOfFish}");
        }
    }
}
