namespace AdventOfCode.Day2
{
    internal class Day2Work
    {
        public static async Task Execute()
        {
            Console.WriteLine("================================");
            Console.WriteLine("=============Day 2==============");
            Console.WriteLine("================================");

            string[]? textLineDay2 = await File.ReadAllLinesAsync(@"Day2\Input.txt");

            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (string line in textLineDay2)
            {
                if (line.Contains("down"))
                {
                    aim += ExtractIntFromInput(line, "down ");
                }
                else if (line.Contains("up"))
                {
                    aim -= ExtractIntFromInput(line, "up ");
                }
                else if (line.Contains("forward"))
                {
                    var forwardAmount = ExtractIntFromInput(line, "forward ");
                    horizontalPosition += forwardAmount;
                    depth += (aim * forwardAmount);
                }
            }

            Console.WriteLine("Forward Sum: " + horizontalPosition);
            Console.WriteLine("Depth Sum: " + depth);
            Console.WriteLine("Day 2 Answer: " + (depth * horizontalPosition));
        }

        private static int ExtractIntFromInput(string input, string keyWord)
        {
            return Convert.ToInt32(input.Replace(keyWord, ""));
        }
    }
}
