namespace AdventOfCode.Day2
{
    internal class Day2Work
    {
        public static async Task<int> Execute()
        {
            string[]? textLineDay2 = await System.IO.File.ReadAllLinesAsync(@"Day2\Input.txt");

            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (string line in textLineDay2)
            {
                if (line.Contains("down"))
                {
                    aim = aim + Convert.ToInt32(line.Replace("down ", ""));
                }
                else if (line.Contains("up"))
                {
                    aim = aim - Convert.ToInt32(line.Replace("up ", ""));
                }
                else if (line.Contains("forward"))
                {
                    var forwardAmount = Convert.ToInt32(line.Replace("forward ", ""));
                    horizontalPosition = horizontalPosition + forwardAmount;
                    depth = depth + (aim * forwardAmount);
                }
            }

            Console.WriteLine("Forward Sum: " + horizontalPosition);
            Console.WriteLine("Depth Sum: " + depth);

            return depth * horizontalPosition;
        }
    }
}
