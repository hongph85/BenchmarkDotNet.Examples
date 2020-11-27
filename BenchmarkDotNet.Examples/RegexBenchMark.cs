using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BenchmarkDotNet.Examples
{
    public class RegexBenchMark
    {
        private Regex _regex = new Regex("[a-zA-Z0-9]*", RegexOptions.Compiled);

        [Benchmark] public bool IsMatch() => _regex.IsMatch("abcdefghijklmnopqrstuvwxyz123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
    }
}
