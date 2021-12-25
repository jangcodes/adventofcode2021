using AdventOfCode.Week3.Day19;
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

            await Day19Work.Execute();

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
