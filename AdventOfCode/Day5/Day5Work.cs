using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day5
{
    internal class Day5Work
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
                    (int start, int end) = (firstYValue > secondYValue) ? (secondYValue, firstYValue) : (firstYValue, secondYValue);
                    for (int i = start; i <= end; i++)
                    {
                        grid[firstXValue, i]++;
                    }
                }
                else if (firstYValue == secondYValue)
                {
                    (int start, int end) = (firstXValue > secondXValue) ? (secondXValue, firstXValue) : (firstXValue, secondXValue);
                    for (int i = start; i <= end; i++)
                    {
                        grid[i, firstYValue]++;
                    }
                }
                else
                {
                    // Day 5 Part 2
                    (int x, int y) startingPoint = (firstXValue, firstYValue);
                    (int x, int y) endPoint = (secondXValue, secondYValue);
                    if (secondXValue < firstXValue)
                    {
                        startingPoint = (secondXValue, secondYValue);
                        endPoint = (firstXValue, firstYValue);
                    }

                    bool goingUp = startingPoint.y > endPoint.y;
                    int yCounter = startingPoint.y;

                    for (int i = startingPoint.x; i <= endPoint.x; i++)
                    {
                        grid[i, yCounter]++;

                        if (goingUp)
                        {
                            yCounter--;
                        }
                        else
                        {
                            yCounter++;
                        }
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
