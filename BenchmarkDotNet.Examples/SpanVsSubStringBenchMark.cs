using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNet.Examples
{
    [MemoryDiagnoser]
    public class SpanVsSubStringBenchMark
    {
        string input = "123,456";
        public SpanVsSubStringBenchMark()
        {
        }

        [Benchmark(Baseline = true)]
        public void UsingSubString()
        {
            int commaPos = input.IndexOf(',');
            int first = int.Parse(input.Substring(0, commaPos));
            int second = int.Parse(input.Substring(commaPos + 1));
        }

        [Benchmark]
        public void UsingSpan()
        {
            ReadOnlySpan<char> inputSpan = input;
            int commaPos = input.IndexOf(',');
            int first = int.Parse(inputSpan.Slice(0, commaPos));
            int second = int.Parse(inputSpan.Slice(commaPos + 1));
        }
    }
}
