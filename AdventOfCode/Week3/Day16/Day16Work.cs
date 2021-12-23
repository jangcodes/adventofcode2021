using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Week3.Day16
{
    internal class Day16Work
    {

        private static int versionTotal = 0;

        public static async Task Execute()
        {
            string[] input = await File.ReadAllLinesAsync(@"Week3\Day16\Input.txt");

            var inputInBytes = Convert.FromHexString(input[0]);

            var result = ProcessInput(0, inputInBytes, 0, out int _, out int _);

            Console.WriteLine($"Part 1 Answer: {versionTotal}");
            Console.WriteLine($"Part 2 Answer: {result.Last()}");
        }

        private static List<long> ProcessInput(int firstBitPosition, byte[] input, int numberOfSubpackets, out int numberOfBytesUsed, out int lastBitPosition)
        {
            int i = 0;
            int inputTotalLength = input.Length;
            int bitPosition = firstBitPosition;
            int subPacketCount = 0;

            List<long> literalList = new();

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
                versionTotal += version;
                var typeId = (workingData >> (workingDataLength - bitPosition - 6)) & 7;

                bitPosition += 6;

                if (typeId == 4)
                {
                    var tempBitPosition = (workingDataLength > 8 ? bitPosition - 8 : bitPosition);
                    var literal = HandleLiteral(tempBitPosition, input[i..], out int inputIndex, out int numberOfLiteralFound);

                    literalList.Add(literal);

                    i += inputIndex;
                    bitPosition = (bitPosition + (numberOfLiteralFound * 5)) % 8;

                    if (bitPosition == 0) i++;
                }
                else
                {
                    var lengthTypeId = (workingData >> (workingDataLength - bitPosition - 1)) & 1;
                    bitPosition++;

                    int expectedDataLength = 15;
                    if (lengthTypeId == 0) expectedDataLength = 15;
                    else if (lengthTypeId == 1) expectedDataLength = 11;

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


                    bitPosition = expectedDataLength % 8;

                    if (bitPosition == 0)
                    {
                        i++;
                        expectedDataLength = 0;
                    }

                    List<long> computeList = new List<long>();

                    if (lengthTypeId == 0)
                    {
                        var numberOfBytes = (subPacketLength - (8 - expectedDataLength)) / 8;
                        var extra = (subPacketLength - (8 - expectedDataLength)) % 8;

                        if (extra > 0) numberOfBytes++;

                        var inputResult = ProcessInput(bitPosition, input[i..(i + numberOfBytes + 1)], 0, out int _, out int lastBit);
                        computeList.AddRange(inputResult);

                        if (extra == 0) i++;

                        i += numberOfBytes;
                        bitPosition = lastBit;
                    }
                    else if (lengthTypeId == 1)
                    {
                        for (int c = 0; c < subPacketLength; c++)
                        {
                            var inputResult = ProcessInput(bitPosition, input[i..], 1, out int bytesUsed, out int lastBit);
                            computeList.AddRange(inputResult);

                            i += bytesUsed;

                            bitPosition = lastBit % 8;
                        }
                    }

                    literalList.Add(Compute(computeList, typeId));
                }

                subPacketCount++;

                if (numberOfSubpackets > 0 && subPacketCount == numberOfSubpackets)
                {
                    break;
                }
            }

            numberOfBytesUsed = i;

            lastBitPosition = bitPosition;

            return literalList;
        }

        private static long Compute(List<long> values, int type)
        {
            return type switch
            {
                0 => values.Sum(),
                1 => values.Aggregate((a, x) => a * x),
                2 => values.Min(),
                3 => values.Max(),
                5 => values[0] > values[1] ? 1 : 0,
                6 => values[0] < values[1] ? 1 : 0,
                7 => values[0] == values[1] ? 1 : 0,
                _ => 0,
            };
        }


        private static long HandleLiteral(int firstBitPosition, byte[] input, out int inputIndex, out int numberOfLiteralFound)
        {
            long literal = 0;

            int bitPosition = firstBitPosition;
            int i = 0;

            numberOfLiteralFound = 0;

            int workingData = input[0];
            int workingDataLength = 8 - bitPosition;

            bool moreNumber = true;
            while (moreNumber)
            {
                if (workingDataLength < 5)
                {
                    i++;
                    workingData = (workingData << 8) | input[i];
                    workingDataLength += 8;
                }

                var fiveBits = workingData >> (workingDataLength - 5);

                moreNumber = GetNumber(fiveBits, out long output);
                literal = (literal << 4) | output;
                workingData &= (1 << (workingDataLength - 5)) - 1;
                workingDataLength -= 5;

                numberOfLiteralFound++;
            }

            inputIndex = i;

            return literal;
        }

        private static bool GetNumber(int fiveBits, out long output)
        {
            bool moreNumber = Convert.ToBoolean((fiveBits >> 4) & 1);
            output = fiveBits & 15;
            return moreNumber;
        }
    }
}
