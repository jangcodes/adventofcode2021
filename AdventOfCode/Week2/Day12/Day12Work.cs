using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Week2.Day12
{
    internal class Day12Work
    {
        private static List<string[]> InputNoStart = new();

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week2\Day12\Input.txt");

            List<string[]> inputList = input.Select(x => x.Split('-').ToArray()).ToList();
            InputNoStart = inputList.Where(x => !x.Contains("start")).ToList();

            var startPoints = inputList.Where(x => x.Contains("start"));
            int possiblePaths = 0;
            foreach (var startPoint in startPoints)
            {
                string firstPoint = startPoint.First(_ => _ != "start");
                possiblePaths += FindCave(firstPoint, new string[] { firstPoint }, 1);
            }

            Console.WriteLine($"Part 1 Answer: {possiblePaths}");

            possiblePaths = 0;
            foreach (var startPoint in startPoints)
            {
                string firstPoint = startPoint.First(_ => _ != "start");
                possiblePaths += FindCave(firstPoint, new string[] { firstPoint }, 2);
            }
            
            Console.WriteLine($"Part 2 Answer: {possiblePaths}");
        }

        private static int FindCave(string caveName, string[] currentPath, int part)
        {
            int possiblePaths = 0;
            var nextCaves = InputNoStart.Where(x => x.Contains(caveName));

            var dupeAllowed = part == 2 && !currentPath.Where(x => x.All(_ => char.IsLower(_))).GroupBy(_ => _).Any(x => x.Count() > 1);

            foreach (var cave in nextCaves)
            {
                var nextPoint = cave.First(_ => _ != caveName);

                if (nextPoint == "end")
                {
                    possiblePaths++;
                }
                else if (dupeAllowed || nextPoint.All(_ => char.IsUpper(_)) || !currentPath.Contains(nextPoint))
                {
                    possiblePaths += FindCave(nextPoint, currentPath.Append(nextPoint).ToArray(), part);
                }
            }

            return possiblePaths;
        }
    }
}
