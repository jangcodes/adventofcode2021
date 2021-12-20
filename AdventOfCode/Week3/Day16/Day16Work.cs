using System;
using System.Collections;
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

            var inputInBytes = Convert.FromHexString("38006F45291200");

            ProcessInput(0, inputInBytes);
        }

        private static void ProcessInput(int firstByteLength, byte[] input)
        {
            int workingData = input[0];
            if (firstByteLength > 0)
            {
                workingData = input[0] & (1 << firstByteLength) - 1;
                workingData = (workingData << 8) | input[1];
            }

            var version = workingData >> 5;
            Console.WriteLine($"Version: {version}");

            var typeId = (workingData >> 2) & 7;
            Console.WriteLine($"TypeId: {typeId} ");

            if (typeId == 4)
            {
                byte result = BitConverter.GetBytes(input[0] & 3).First();
                HandleLiteral(result, input);
            }
            else
            {
                var lengthTypeId = (workingData >> 1) & 1;
                if (lengthTypeId == 0)
                {
                    var subPacketLength = workingData & 1;
                    subPacketLength = (subPacketLength << 8) | input[1];
                    subPacketLength = (subPacketLength << 6) | (input[2] >> 2);

                    var leftOverBits = input[2] & ((1 << 2) - 1);
                }
            }

            Console.WriteLine("");
        }


        private static void HandleLiteral(byte leftOver, byte[] input)
        {
            int literal = 0;
            int workingData = leftOver;
            int workingDataLength = 2;
            int i = 0;
            bool moreNumber = true;

            while(moreNumber)
            {
                if (workingDataLength < 5)
                {
                    i++;
                    workingData = (workingData << 8) | input[i];
                    workingDataLength += 8;
                }

                var fiveBits = workingData >> (workingDataLength - 5);

                moreNumber = GetNumber(fiveBits, out int output);
                literal = (literal << 4) | output;
                workingData &= (1 << (workingDataLength - 5)) - 1;
                workingDataLength -= 5;
            }
        }

        // Function to extract k bits from p index
        // and returns the extracted value as integer
        public static int BitExtracted(byte number, int numberOfBits, int index) => ((1 << numberOfBits) - 1) & (number >> index);

        private static bool GetNumber(int fiveBits, out int output)
        {
            bool moreNumber = Convert.ToBoolean((fiveBits >> 4) & 1);
            output = fiveBits & 15;
            return moreNumber;
        }
    }
}
