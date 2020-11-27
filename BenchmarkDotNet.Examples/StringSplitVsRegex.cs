using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BenchmarkDotNet.Examples
{
    public class StringSplitVsRegex
    {
        [Benchmark]
        public string UsingSplit()
        {
            var fullName = "John Smith";
            var names = fullName.Split(" ");
            var lastName = names.LastOrDefault();
            return lastName ?? string.Empty;
        }

        [Benchmark]
        public string UsingRegex()
        {
            var regex = new Regex(@"\w+");
            var fullName = "John Smith";            
            var lastName = regex.Matches(fullName).LastOrDefault()?.Value;
            return lastName ?? string.Empty;
        }
    }
}
