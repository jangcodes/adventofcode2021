using System.Collections.Generic;

namespace AdventOfCode.Day4
{
    internal class BingoBoard
    {
        public List<BingoNumber> BingoNumbers { get; set; } = new List<BingoNumber>();

        public bool BingoFound { get; set; }
    }
}
