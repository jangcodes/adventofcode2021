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
            Console.WriteLine("=============Day 3 Part 2=============");

            string[]? input = await File.ReadAllLinesAsync(@"Day3\Input.txt");

            List<string> filteredListOx = input.ToList();
            
            for (int i = 0; i < 12; i++)
            {
                int oneCount = filteredListOx.Count(x => x[i] == '1');
                int zeroCount = filteredListOx.Count(x => x[i] == '0');

                if (oneCount >= zeroCount)
                {
                    filteredListOx = FilterList(filteredListOx, i, '1');
                }
                else if (oneCount < zeroCount)
                {
                    filteredListOx = FilterList(filteredListOx, i, '0');
                }
            }

            List<string> filteredListCo = input.ToList();
            for (int i = 0; i < 12; i++)
            {
                int oneCount = filteredListCo.Count(x => x[i] == '1');
                int zeroCount = filteredListCo.Count(x => x[i] == '0');

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
            Console.WriteLine("Final Answer: " + (Convert.ToInt32(filteredListOx.First(), 2) * Convert.ToInt32(filteredListCo.First(), 2)));
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
    }
}
