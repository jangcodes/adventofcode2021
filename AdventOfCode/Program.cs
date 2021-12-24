using AdventOfCode.Week3.Day18;
using System;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Program
    {
        public static async Task Main()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            await Day18Work.Execute();

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
