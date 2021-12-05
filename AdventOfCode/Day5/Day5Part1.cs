using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day5
{
    internal class Day5Part1
    {
        public static async Task Execute()

        {
            string[] input = await File.ReadAllLinesAsync(@"Day5\Input.txt");

            int[,] grid = new int[1000, 1000];

            foreach (var row in input)
            {
                var lineCoordinates = row.Split(" -> ");

                var firstCoordinate = lineCoordinates[0].Split(",").Select(_ => Convert.ToInt32(_)).ToArray();
                var firstXValue = firstCoordinate[0];
                var firstYValue = firstCoordinate[1];

                var secondCoordinate = lineCoordinates[1].Split(",").Select(_ => Convert.ToInt32(_)).ToArray();
                var secondXValue = secondCoordinate[0];
                var secondYValue = secondCoordinate[1];

                if (firstXValue == secondXValue)
                {
                    int biggerValue = 0;
                    int smallerValue = 0;

                    if (firstYValue > secondYValue)
                    {
                        biggerValue = firstYValue;
                        smallerValue = secondYValue;
                    }
                    else
                    {
                        biggerValue = secondYValue;
                        smallerValue = firstYValue;
                    }

                    for (int i = smallerValue; i <= biggerValue; i++)
                    {
                        grid[firstXValue, i]++;
                    }
                }
                else if (firstYValue == secondYValue)
                {
                    int biggerValue = 0;
                    int smallerValue = 0;

                    if (firstXValue > secondXValue)
                    {
                        biggerValue = firstXValue;
                        smallerValue = secondXValue;
                    }
                    else
                    {
                        biggerValue = secondXValue;
                        smallerValue = firstXValue;
                    }

                    for (int i = smallerValue; i <= biggerValue; i++)
                    {
                        grid[i, firstCoordinate[1]]++;
                    }
                }
            }

            int overlapCount = 0;
            for (int row = 0; row < 1000; row++)
            {
                for (int col = 0; col < 1000; col++)
                {
                    if (grid[row, col] > 1)
                    {
                        overlapCount++;
                    }
                }
            }

            Console.WriteLine("Answer: " + overlapCount);
        }
    }
}
