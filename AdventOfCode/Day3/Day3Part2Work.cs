using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    internal class Day3Part2Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("=============Day 3 Part 2=============");
            Console.WriteLine("======================================");

            string[]? input = await File.ReadAllLinesAsync(@"Day3\Input.txt");

            List<string> filteredListOx = input.ToList();
            List<string> filteredListCo = input.ToList();

            for (int i = 0; i < 12; i++)
            {
                int oneCount = CountChar(filteredListOx, i, '1');
                int zeroCount = CountChar(filteredListOx, i, '0');

                if (oneCount >= zeroCount)
                {
                    filteredListOx = FilterList(filteredListOx, i, '1');
                }
                else if (oneCount < zeroCount)
                {
                    filteredListOx = FilterList(filteredListOx, i, '0');
                }
            }

            for (int i = 0; i < 12; i++)
            {
                int oneCount = CountChar(filteredListCo, i, '1');
                int zeroCount = CountChar(filteredListCo, i, '0');

                if (oneCount >= zeroCount)
                {
                    filteredListCo = FilterList(filteredListCo, i, '0');
                }
                else if (oneCount < zeroCount)
                {
                    filteredListCo = FilterList(filteredListCo, i, '1');
                }
            }

            Console.WriteLine("Result Oxygen: " + filteredListOx.First());
            Console.WriteLine("Result CO2: " + filteredListCo.First());
            Console.WriteLine("Final Answer: " + (GetDoubleFromStringBinary(filteredListOx.First()) * GetDoubleFromStringBinary(filteredListCo.First())));
        }


        private static List<string> FilterList(List<string> unfilteredList, int position, char ch)
        {
            List<string> result = unfilteredList;

            if (unfilteredList.Count > 1)
            {
                result = unfilteredList.Where(x => x[position] == ch).ToList();
            }

            return result;
        }

        private static int CountChar(List<string> input, int position, char ch)
        {
            int count = 0;

            foreach (string line in input)
            {
                if (line[position] == ch)
                {
                    count++;
                }
            }

            return count;
        }

        private static double GetDoubleFromStringBinary(string bin)
        {
            double result = 0;

            for (int i = 0; i < 12; i++)
            {
                int powNumber = 11 - i;
                if (bin[i] == '1')
                {
                    result += Math.Pow(2, powNumber);
                }
            }

            return result;
        }
    }
}
