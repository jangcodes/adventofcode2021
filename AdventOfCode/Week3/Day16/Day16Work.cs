using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day16
{
    class Day16Work
    {
        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day16\Example.txt");

            var test = Convert.FromHexString(input[0]);


            Console.WriteLine(Convert.ToString(test[0], toBase: 2));

            var testBit = (test[0] >> 5);

            Console.WriteLine(Convert.ToString(testBit, toBase: 2));


            var anotherTest = test[0] & testBit;

            Console.WriteLine(Convert.ToString(anotherTest, toBase: 2));


            Console.WriteLine("");
        }

    }
}
