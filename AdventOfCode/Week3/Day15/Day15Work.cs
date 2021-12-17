using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day15
{
    internal class Day15Work
    {
        // Got a hint from reddit to implement Dijkstra's Algorithm
        // https://www.youtube.com/watch?v=pVfj6mxhdMw

        private static List<int[]> grid = new();

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day15\Example.txt");

            var row = input.Length;

            byte[,] newGrid = new byte[row * 5, row * 5];

            grid = input.Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToList();

            HashSet<Position> unvisited = new();
            Dictionary<Position, int> shortestDistanceFromOrigin = new();


            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < row; x++)
                {
                    var currrentValue = Convert.ToByte(input[y][x] - '0');
                    newGrid[y, x] = currrentValue;

                    for (int z = row; z < row * 5; z += row)
                    {
                        currrentValue++;

                        if (currrentValue > 9) currrentValue = 1;

                        newGrid[y, x + z] = currrentValue;
                    }


                }

                for (int x = 0; x < row * 5; x++)
                {
                    var currrentValue = newGrid[y, x];

                    newGrid[y, x] = currrentValue;

                    for (int z = row; z < row * 5; z += row)
                    {
                        currrentValue++;
                        if (currrentValue > 9) currrentValue = 1;

                        newGrid[y + z, x] = currrentValue;
                    }
                }
            }


            for (int y = 0; y < row * 5; y++)
            {
                for (int x = 0; x < row * 5; x++)
                {
                    Position p = new(y, x);
                    // shortestDistanceFromOrigin.Add(p, int.MaxValue);
                    unvisited.Add(p);
                }
            }

            var currentPosition = new Position(0, 0);
            shortestDistanceFromOrigin[currentPosition] = 0;

            while (true)
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();


                var shortestUnvisitedPair =
                    (from sdf in shortestDistanceFromOrigin
                     join u in unvisited on sdf.Key equals u
                     select sdf).First();

                // var shortestUnvisitedPair = FindShortest(items);

                var shortedUnvisited = shortestUnvisitedPair.Key;

                Position n1 = new(shortedUnvisited.Y, shortedUnvisited.X + 1);
                Position n2 = new(shortedUnvisited.Y, shortedUnvisited.X - 1);
                Position n3 = new(shortedUnvisited.Y + 1, shortedUnvisited.X);
                Position n4 = new(shortedUnvisited.Y - 1, shortedUnvisited.X);

                Position[] allNeighbors = new Position[] { n1, n2, n3, n4 };

                foreach (var n in allNeighbors)
                {
                    if (n.X >= 0 && n.Y >= 0 && n.X < row * 5 && n.Y < row * 5)
                    {
                        var newValue = shortestUnvisitedPair.Value + newGrid[n.Y, n.X];

                        if (shortestDistanceFromOrigin.ContainsKey(n))
                        {
                            if (shortestDistanceFromOrigin[n] > newValue)
                            {
                                shortestDistanceFromOrigin[n] = newValue;
                            }
                        }
                        else
                        {
                            shortestDistanceFromOrigin[n] = newValue;
                        }
                    }
                }

                if (shortedUnvisited.Y == row * 5 - 1 && shortedUnvisited.X == row * 5 - 1)
                {
                    break;
                }

                var item = unvisited.First(x => x.X == shortedUnvisited.X && x.Y == shortedUnvisited.Y);
                unvisited.Remove(item);

                if (!unvisited.Any()) break;

                watch.Stop();
                Console.WriteLine($"Unvisited: {unvisited.Count()} ; Loop time: {watch.ElapsedMilliseconds} ms");
            }

            Position lastPoint = new(row * 5 - 1, row * 5 - 1);
            Console.WriteLine($"Part 1 Answer: {shortestDistanceFromOrigin[lastPoint]}");
        }

    }

    internal struct Position
    {
        public int Y { get; }

        public int X { get; }


        public Position(int y, int x)
        {
            Y = y;
            X = x;
        }
    }
}
