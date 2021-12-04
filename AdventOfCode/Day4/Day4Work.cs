using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
    internal class Day4Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("=============Day 4 Part 1=============");

            string[] input = await File.ReadAllLinesAsync(@"Day4\Input.txt");
            int[] bingoNumbers = input[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            List<BingoBoard> bingoBoards = new();
            List<BingoNumber> tempBingoBoard = new();

            int bingoNumberIndex = 0;


            for (int i = 1; i < input.Length; i++)
            {
                if (!string.IsNullOrEmpty(input[i]))
                {
                    var bingoRowWithEmpty = input[i].Split(' ')
                        .Where(x => !string.IsNullOrEmpty(x))
                        .Select(x => Convert.ToInt32(x));

                    foreach (var item in bingoRowWithEmpty)
                    {
                        tempBingoBoard.Add(new BingoNumber() { Index = bingoNumberIndex, Number = item, Selected = false });
                        bingoNumberIndex++;
                    }

                    if (tempBingoBoard.Count == 25)
                    {
                        bingoBoards.Add(new BingoBoard() { BingoNumbers = tempBingoBoard, BingoFound = false });
                        tempBingoBoard = new();
                        bingoNumberIndex = 0;
                    }
                }
            }

            int firstBingoNumber = 0;

            bool lastFound = false;

            foreach (var calledBingoNumber in bingoNumbers)
            {
                bool bingoFound = false;

                foreach (var bingoBoard in bingoBoards)
                {
                    var bingoEntryFound = bingoBoard.BingoNumbers.FirstOrDefault(x => x.Number == calledBingoNumber);
                    if (bingoEntryFound != null) bingoEntryFound.Selected = true;

                    bingoFound = FindBingo(bingoBoard.BingoNumbers);
                    if (bingoFound)
                    {
                        bingoBoard.BingoFound = true;
                        if (firstBingoNumber == 0)
                        {
                            firstBingoNumber = calledBingoNumber;
                            int sumOfUnselected = bingoBoard.BingoNumbers.Where(x => !x.Selected).Select(x => x.Number).ToArray().Sum();

                            PrintSingleBingoBoard(bingoBoard);

                            Console.WriteLine("Bingo Number: " + firstBingoNumber);
                            Console.WriteLine("Sum of Unselected: " + sumOfUnselected);
                            Console.WriteLine("Day 4 Part 1 Answer: " + (firstBingoNumber * sumOfUnselected));
                            Console.WriteLine();
                        }


                        if (bingoBoards.All(x => x.BingoFound) && !lastFound)
                        {
                            lastFound = true;

                            int sumOfUnselected = bingoBoard.BingoNumbers.Where(x => !x.Selected).Select(x => x.Number).ToArray().Sum();


                            Console.WriteLine("=============Day 4 Part 2=============");

                            PrintSingleBingoBoard(bingoBoard);

                            Console.WriteLine("Bingo Number: " + calledBingoNumber);
                            Console.WriteLine("Sum of Unselected: " + sumOfUnselected);
                            Console.WriteLine("Day 4 Part 2 Answer: " + (calledBingoNumber * sumOfUnselected));
                            Console.WriteLine();
                        }
                    }
                }
            }           
        }

        private static void PrintBingoBoards(List<BingoBoard> bingoBoards)
        {
            foreach (var bingoBoard in bingoBoards)
            {
                PrintSingleBingoBoard(bingoBoard);
                Console.WriteLine();
            }
        }

        private static void PrintSingleBingoBoard(BingoBoard bingoBoard)
        {
            int columnCounter = 0;

            foreach (var number in bingoBoard.BingoNumbers)
            {
                if (number.Number < 10) Console.Write(" ");

                if (number.Selected) Console.ForegroundColor = ConsoleColor.Red;

                Console.Write(number.Number + " ");
                Console.ResetColor();
                columnCounter++;

                if (columnCounter == 5)
                {
                    Console.WriteLine();
                    columnCounter = 0;
                }
            }
        }

        private static bool FindBingo(List<BingoNumber> bingoBoard)
        {
            // Check for Row
            for (int i = 0; i < bingoBoard.Count; i += 5)
            {
                var bingoRow = bingoBoard.GetRange(i, 5);
                bool found = bingoBoard.GetRange(i, 5).All(x => x.Selected);

                if (found) return true;
            }

            // Check for Column
            for (int i = 0; i < 5; i++)
            {
                BingoNumber[] column = new BingoNumber[] { bingoBoard[i], bingoBoard[i + 5], bingoBoard[i + 10], bingoBoard[i + 15], bingoBoard[i + 20] };

                bool found = column.All(x => x.Selected);

                if (found) return true;
            }

            // Check for Diag
            bool firstDiag = bingoBoard[0].Selected && bingoBoard[6].Selected && bingoBoard[12].Selected && bingoBoard[18].Selected && bingoBoard[24].Selected;
            if (firstDiag) return true;

            bool secondDiag = bingoBoard[4].Selected && bingoBoard[8].Selected && bingoBoard[12].Selected && bingoBoard[16].Selected && bingoBoard[20].Selected;
            if (secondDiag) return true;

            return false;
        }
    }
}
