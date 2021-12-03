using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    internal class Day3Part2Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("================================");
            Console.WriteLine("=============Day 3 Part 2=======");
            Console.WriteLine("================================");

            string[]? input = await File.ReadAllLinesAsync(@"Day3\Input.txt");

            List<string> filteredListOx = input.ToList();
            List<string> filteredListCo = input.ToList();

            int charPosition = 0;
            while (charPosition < 12)
            {
                int oneCount = CountChar(filteredListOx, charPosition, '1');
                int zeroCount = CountChar(filteredListOx, charPosition, '0');

                if (oneCount >= zeroCount)
                {
                    filteredListOx = filteredListOx.Where(x => x[charPosition] == '1').ToList();
                }
                else if (oneCount < zeroCount)
                {
                    filteredListOx = filteredListOx.Where(x => x[charPosition] == '0').ToList();
                }

                charPosition++;

                if (filteredListOx.Count == 1)
                {
                    break;
                }
            }

            charPosition = 0;
            while (charPosition < 12)
            {
                int oneCount = CountChar(filteredListCo, charPosition, '1');
                int zeroCount = CountChar(filteredListCo, charPosition, '0');

                if (oneCount >= zeroCount)
                {
                    filteredListCo = filteredListCo.Where(x => x[charPosition] == '0').ToList();
                }
                else if (oneCount < zeroCount)
                {
                    filteredListCo = filteredListCo.Where(x => x[charPosition] == '1').ToList();
                }

                charPosition++;

                if (filteredListCo.Count == 1)
                {
                    break;
                }
            }


            Console.WriteLine("Result Oxygen: " + filteredListOx.First() + " " + GetDoubleFromStringBinary(filteredListOx.First()));
            Console.WriteLine("Result CO2: " + filteredListCo.First() + " " + GetDoubleFromStringBinary(filteredListCo.First()));
            Console.WriteLine("Final Answer: " + (GetDoubleFromStringBinary(filteredListOx.First()) * GetDoubleFromStringBinary(filteredListCo.First())));

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
