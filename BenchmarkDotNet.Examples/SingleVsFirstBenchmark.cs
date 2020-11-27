using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNet.Examples
{
    public class SingleVsFirstBenchmark
    {
        private readonly List<string> _haystack = new List<string>();
        private readonly int _haystackSize = 1000000;
        private readonly string _needle = "needle";
        public SingleVsFirstBenchmark()
        {
            //Add a large amount of items to our list. 
            Enumerable.Range(1, _haystackSize).ToList().ForEach(x => _haystack.Add(x.ToString()));
            //Insert the needle right in the middle. 
            _haystack.Insert(_haystackSize / 2, _needle);
        }

        [Benchmark(Baseline = true)]
        public string Single() => _haystack.SingleOrDefault(x => x == _needle);

        [Benchmark()]
        public string First() => _haystack.FirstOrDefault(x => x == _needle);
    }
}