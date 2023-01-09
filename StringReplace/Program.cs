using System;
using BenchmarkDotNet.Running;

namespace StringReplace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringReplaceBenchmarks>();
            Console.ReadKey();
        }
    }
}