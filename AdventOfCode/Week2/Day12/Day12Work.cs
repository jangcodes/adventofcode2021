using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day12
{
    internal class Day12Work
    {
        private static List<string[]> InputNoStart = new();

        private static int GlobalCounter = 0;

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day12\Input.txt");

            List<string[]> inputList = input.Select(x => x.Split('-').ToArray()).ToList();
            InputNoStart = inputList.Where(x => !x.Contains("start")).ToList();

            var startPoints = inputList.Where(x => x.Contains("start"));
            foreach(var startPoint in startPoints)
            {
                string firstPoint = startPoint.First(_ => _ != "start");
                FindCave(firstPoint, new string[] { firstPoint });
            }

            Console.WriteLine($"Part 1 Answer: {GlobalCounter}");
        }

        private static void FindCave(string caveName, string[] currentPath)
        {
            var nextCaves = InputNoStart.Where(x => x.Contains(caveName));

            foreach (var cave in nextCaves)
            {
                var nextPoint = cave.First(_ => _ != caveName);

                if (nextPoint == "end")
                {
                    GlobalCounter++;
                }
                else if (nextPoint.All(_ => char.IsUpper(_)) || !currentPath.Contains(nextPoint))
                {
                    FindCave(nextPoint, currentPath.Append(nextPoint).ToArray());
                }
             }
        }
    }
}
