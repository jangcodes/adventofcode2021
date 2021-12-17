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

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day15\Input.txt");
            var row = input.Length;
            var expandedRowCount = row * 5;
            byte[,] newGrid = new byte[expandedRowCount, expandedRowCount];

            Dictionary<Position, int> shortestDistanceFromOrigin = new();

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < row; x++)
                {
                    var currrentValue = Convert.ToByte(input[y][x] - '0');
                    newGrid[y, x] = currrentValue;

                    for (int z = row; z < expandedRowCount; z += row)
                    {
                        currrentValue++;
                        if (currrentValue > 9) currrentValue = 1;
                        newGrid[y, x + z] = currrentValue;
                    }
                }

                for (int x = 0; x < expandedRowCount; x++)
                {
                    var currrentValue = newGrid[y, x];
                    newGrid[y, x] = currrentValue;
                    for (int z = row; z < expandedRowCount; z += row)
                    {
                        currrentValue++;
                        if (currrentValue > 9) currrentValue = 1;
                        newGrid[y + z, x] = currrentValue;
                    }
                }
            }

            var currentPosition = new Position(0, 0);
            shortestDistanceFromOrigin[currentPosition] = 0;

            List<KeyValuePair<Position, int>> existingList = new();
            existingList.Add(new(currentPosition, 0));
            existingList.Add(new(new Position(expandedRowCount, expandedRowCount), int.MaxValue));

            while (true)
            {
                var shortestUnvisitedPair = existingList.First();
                var shortedUnvisited = shortestUnvisitedPair.Key;

                Position n1 = new(shortedUnvisited.Y, shortedUnvisited.X + 1);
                Position n2 = new(shortedUnvisited.Y, shortedUnvisited.X - 1);
                Position n3 = new(shortedUnvisited.Y + 1, shortedUnvisited.X);
                Position n4 = new(shortedUnvisited.Y - 1, shortedUnvisited.X);

                Position[] allNeighbors = new Position[] { n1, n2, n3, n4 };

                foreach (var n in allNeighbors)
                {
                    if (n.X >= 0 && n.Y >= 0 && n.X < expandedRowCount && n.Y < expandedRowCount)
                    {
                        var newValue = shortestUnvisitedPair.Value + newGrid[n.Y, n.X];

                        if (shortestDistanceFromOrigin.ContainsKey(n))
                        {
                            if (shortestDistanceFromOrigin[n] > newValue)
                            {
                                shortestDistanceFromOrigin[n] = newValue;

                                AddToSortedList(existingList, new KeyValuePair<Position, int>(n, newValue));
                            }
                        }
                        else
                        {
                            shortestDistanceFromOrigin[n] = newValue;
                            AddToSortedList(existingList, new KeyValuePair<Position, int>(n, newValue));
                        }
                    }
                }

                if (shortedUnvisited.Y == expandedRowCount - 1 && shortedUnvisited.X == expandedRowCount - 1)
                {
                    break;
                }

                existingList.Remove(shortestUnvisitedPair);
            }

            Position part1 = new(row - 1, row - 1);
            Position part2 = new(expandedRowCount - 1, expandedRowCount - 1);

            Console.WriteLine($"Part 1 Answer: {shortestDistanceFromOrigin[part1]}");
            Console.WriteLine($"Part 2 Answer: {shortestDistanceFromOrigin[part2]}");
        }


        private static void AddToSortedList(List<KeyValuePair<Position, int>> existingList, KeyValuePair<Position, int> newItem)
        {
            if (existingList.Exists(x => x.Key == newItem.Key))
            {
                existingList.Remove(newItem);
            }

            var index = existingList.IndexOf(existingList.First(x => newItem.Value < x.Value));
            existingList.Insert(index, newItem);
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

        public static bool operator ==(Position lp, Position rp)
        {
            return lp.Y == rp.Y && lp.X == rp.X;
        }

        public static bool operator !=(Position lp, Position rp) => !(lp == rp);
    }
}
