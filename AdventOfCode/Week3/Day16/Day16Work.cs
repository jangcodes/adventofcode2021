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

        private static void ProcessInput(int firstBitPosition, byte[] input)
        {
            Console.WriteLine("");

            int i = 0;
            int inputTotalLength = input.Length;
            int bitPosition = firstBitPosition;

            while (i < inputTotalLength - 1)
            {
                int workingData = input[i];
                int workingDataLength = 8;
                if (bitPosition > 1)
                {
                    i++;
                    workingData = (workingData << 8) | input[i];
                    workingDataLength += 8;
                }

                var version = (workingData >> (workingDataLength - bitPosition - 3)) & 7;
                Console.WriteLine($"Version: {version}");
                var typeId = (workingData >> (workingDataLength - bitPosition - 6)) & 7;
                Console.WriteLine($"TypeId: {typeId} ");

                bitPosition += 6;

                if (typeId == 4)
                {
                    var tempBitPosition = (workingDataLength > 8 ? bitPosition - 8 : bitPosition);
                    var literal = HandleLiteral(tempBitPosition, input[i..], out int inputIndex, out int numberOfLiteralFound);

                    i += inputIndex;

                    bitPosition = (bitPosition + (numberOfLiteralFound * 5)) % 8;

                    Console.WriteLine($"Literal: {literal}");
                }
                else
                {
                    var lengthTypeId = (workingData >> (workingDataLength - bitPosition - 1)) & 1;
                    bitPosition++;
                    if (lengthTypeId == 0)
                    {
                        int expectedDataLength = 15;

                        var dataLeft = workingDataLength - bitPosition;
                        var subPacketLength = workingData & ((1 << dataLeft) - 1);

                        expectedDataLength -= dataLeft;

                        if (expectedDataLength > 8)
                        {
                            i++;
                            subPacketLength = (subPacketLength << 8) | input[i];
                            expectedDataLength -= 8;
                        }

                        i++;
                        subPacketLength = (subPacketLength << expectedDataLength) | (input[i] >> 8 - expectedDataLength);

                        Console.WriteLine($"SubPacket Length: {subPacketLength}");

                        var numberOfBytes = (subPacketLength - (8 - expectedDataLength)) / 8;
                        var extra = (subPacketLength - (8 - expectedDataLength)) % 8;

                        if (extra > 0) numberOfBytes++;

                        bitPosition = expectedDataLength;

                        ProcessInput(bitPosition, input[i..(i + numberOfBytes + 1)]);

                        i += (i + numberOfBytes);
                    }
                }

                Console.WriteLine("");
            }
        }


        private static int HandleLiteral(int firstBitPosition, byte[] input, out int inputIndex, out int numberOfLiteralFound)
        {
            int literal = 0;

            int bitPosition = firstBitPosition;
            int i = 0;

            numberOfLiteralFound = 0;

            int workingData = input[0];
            int workingDataLength = 8 - bitPosition;
                        
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

                numberOfLiteralFound ++;
            }

            inputIndex = i;

            return literal;
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
