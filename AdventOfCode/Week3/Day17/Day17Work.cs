using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day17
{
    internal class Day17Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("========Day 17========");
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day17\Input.txt");

            var xCoordinates = input[0].Replace("x=", "").Split("..").Select(x => Convert.ToInt32(x)).ToArray();
            var yCoordinates = input[1].Replace("y=", "").Split("..").Select(x => Convert.ToInt32(x)).ToArray();

            var maxY = Math.Abs(yCoordinates[0]) - 1;
            var maxYHeight = maxY * (maxY + 1) / 2;
            Console.WriteLine($"Part 1 Answer: {maxYHeight}");

            var distance = 0;
            var minX = 0;
            while (distance <= xCoordinates[0])
            {
                minX++;
                distance = minX * (minX + 1) / 2;
            }

            List<(int x, int counter)> validXCoordinates = new();
            for (int x = minX; x <= xCoordinates[1]; x++)
            {
                int xPos = 0;
                int xVelocity = x;
                int counter = 0;

                while (xVelocity > 0)
                {
                    counter++;
                    xPos += xVelocity;

                    if (xPos > xCoordinates[1]) break;
                    if (xPos >= xCoordinates[0] && xPos <= xCoordinates[1]) validXCoordinates.Add(new(x, counter));

                    xVelocity--;
                }
            }


            List<(int y, int counter)> validYCoordinates = new();
            for (int y = yCoordinates[0]; y <= maxY; y++)
            {
                int yPos = 0;
                int yVelocity = y;
                int counter = 0;

                while (yPos >= yCoordinates[0])
                {
                    counter++;
                    yPos += yVelocity;

                    if (yPos >= yCoordinates[0] && yPos <= yCoordinates[1]) validYCoordinates.Add(new(y, counter));

                    yVelocity--;
                }
            }

            var joinList = (from x in validXCoordinates
                            join y in validYCoordinates on x.counter equals y.counter
                            select new { x.x, y.y }).ToList();

            var maxXList = from x in validXCoordinates
                           where x.counter == x.x
                           select x;

            foreach (var (x, counter) in maxXList)
            {
                joinList.AddRange(validYCoordinates.Where(_ => _.counter > counter).Select(_ => new { x, _.y }));
            }

            joinList = joinList.Distinct().ToList();

            Console.WriteLine($"Part 2 Answer: {joinList.Count}");

        }
    }
}
