using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Examples;
using BenchmarkDotNet.Running;
using System;

[MemoryDiagnoser]
public class Program
{
    static void Main(string[] args) => BenchmarkSwitcher.FromAssemblies(new[] { typeof(Program).Assembly }).Run(args);

    // BENCHMARKS GO HERE
}
