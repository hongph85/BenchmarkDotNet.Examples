using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNet.Examples
{
    [MemoryDiagnoser]
    public class ExecuteBenchmark
    {
        SingleVsFirstStub singleAndFirst;
        public ExecuteBenchmark()
        {
            singleAndFirst = new SingleVsFirstStub();
        }

        [Benchmark()]
        public void SplitTest()
        {
            const string FullName = "Steve J Gordon";
            NameParser Parser = new NameParser();

            Parser.GetLastName(FullName);
        }

        [Benchmark()]
        public void SingleTest()
        {
            singleAndFirst.Single();
        }

        [Benchmark()]
        public void FirstTest()
        {
            singleAndFirst.First();
        }
    }
}
