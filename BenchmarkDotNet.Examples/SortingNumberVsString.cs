using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkDotNet.Examples
{
    public class DoubleSorting : SortingNumberVsString<double> { protected override double GetNext() => _random.Next(); }
    public class Int32Sorting : SortingNumberVsString<int> { protected override int GetNext() => _random.Next(); }
    public class StringSorting : SortingNumberVsString<string>
    {
        protected override string GetNext()
        {
            var dest = new char[_random.Next(1, 5)];
            for (int i = 0; i < dest.Length; i++) dest[i] = (char)('a' + _random.Next(26));
            return new string(dest);
        }
    }

    public abstract class SortingNumberVsString<T>
    {
        protected Random _random;
        private T[] _orig, _array;

        [Params(10)]
        public int Size { get; set; }

        protected abstract T GetNext();

        [GlobalSetup]
        public void Setup()
        {
            _random = new Random(42);
            _orig = Enumerable.Range(0, Size).Select(_ => GetNext()).ToArray();
            _array = (T[])_orig.Clone();
            Array.Sort(_array);
        }

        public void Random()
        {
            _orig.AsSpan().CopyTo(_array);
            Array.Sort(_array);
        }
    }

    [MemoryDiagnoser]
    public class SortingBenchMark
    {
        DoubleSorting s1;
        Int32Sorting s2;
        StringSorting s3;
        public SortingBenchMark()
        {
            s1 = new DoubleSorting();
            s1.Setup();
            s2 = new Int32Sorting();
            s2.Setup();
            s3 = new StringSorting();
            s3.Setup();

        }

        [Benchmark]
        public void SortingDouble()
        {
            s1.Random();
        }
        [Benchmark]
        public void SortingInt()
        {
            s2.Random();

        }

        [Benchmark(Baseline = true)]
        public void SortingString()
        {
            s3.Random();
        }
    }
}
