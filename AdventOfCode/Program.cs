using AdventOfCode.Week3.Day17;
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

            await Day17Work.Execute();

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }
    }
}
