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

        private static List<int[]> grid = new List<int[]>();

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day15\Input.txt");

            var row = input.Length;
            var col = input[0].Length;

            grid = input.Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToList();

            List<Position> unvisited = new();
            Dictionary<Position, int> shortestDistanceFromOrigin = new();

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    Position p = new(y, x);
                    shortestDistanceFromOrigin.Add(p, int.MaxValue);
                    unvisited.Add(p);
                }
            }

            var currentPosition = new Position(0, 0);
            shortestDistanceFromOrigin[currentPosition] = 0;

            while (true)
            {

                var items =
                    from sdf in shortestDistanceFromOrigin
                    join u in unvisited on sdf.Key equals u
                    select sdf;

                var shortestUnvisitedPair = items.OrderBy(x => x.Value).First();

                var shortedUnvisited = shortestUnvisitedPair.Key;

                Position n1 = new(shortedUnvisited.Y, shortedUnvisited.X + 1);
                Position n2 = new(shortedUnvisited.Y, shortedUnvisited.X - 1);
                Position n3 = new(shortedUnvisited.Y + 1, shortedUnvisited.X);
                Position n4 = new(shortedUnvisited.Y - 1, shortedUnvisited.X);

                Position[] allNeighbors = new Position[] { n1, n2, n3, n4 };

                foreach (var n in allNeighbors)
                {
                    if (shortestDistanceFromOrigin.ContainsKey(n))
                    {
                        var newValue = shortestUnvisitedPair.Value + grid[n.Y][n.X];

                        if (shortestDistanceFromOrigin[n] > newValue)
                        {
                            shortestDistanceFromOrigin[n] = newValue;
                        }
                    }
                }

                if (shortedUnvisited.Y == row - 1 && shortedUnvisited.X == col - 1)
                {
                    break;
                }

                var item = unvisited.First(x => x.X == shortedUnvisited.X && x.Y == shortedUnvisited.Y);
                unvisited.Remove(item);

                if (!unvisited.Any()) break;

            }

            Position lastPoint = new(row - 1, col - 1);
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
