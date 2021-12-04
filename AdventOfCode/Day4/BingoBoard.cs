using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day4
{
    internal class BingoBoard
    {
        public List<BingoNumber> BingoNumbers { get; set; } = new List<BingoNumber>();

        public bool BingoFound { get; set; }

        public int SumOfUnselected
        {
            get { return BingoNumbers.Where(x => !x.Selected).Select(x => x.Number).ToArray().Sum(); }
        }
    }
}
