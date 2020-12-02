using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BenchmarkDotNet.Examples
{
    [MemoryDiagnoser]
    public class SequenceVsParallelBenchmark
    {
        private readonly Consumer consumer = new Consumer();

        [Params(1000, 10000, 50000)]
        public int MaxNumber { get; set; }

        [Benchmark]
        public void CalculatePrimesUpTo()
        {
            (from number in Enumerable.Range(1, MaxNumber)
                   where IsPrime(number)
                   select number).Consume(consumer);
        }

        [Benchmark]
        public void ParallelCalculatePrimesUpTo()
        {
            (from number in Enumerable.Range(1, MaxNumber).AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                   where IsPrime(number)
                   select number).Consume(consumer);
        }

        [Benchmark]
        public void ForEachCalculatePrimes()
        {
            var primeNumbers = new List<int>();

            foreach (int number in Enumerable.Range(1, MaxNumber))
            {
                if (IsPrime(number))
                    primeNumbers.Add(number);
            }

            primeNumbers.Consume(consumer);
        }

        [Benchmark]
        public void ParallelForEachCalculatePrimes()
        {
            var primeNumbers = new ConcurrentBag<int>();

            Parallel.ForEach(Enumerable.Range(1, MaxNumber), number =>
            {
                if (IsPrime(number))
                    primeNumbers.Add(number);
            });

            primeNumbers.Consume(consumer);
        }

        // uses inefficient trial division algorithm
        private bool IsPrime(int number)
        {
            if (number == 1)
                return false;

            var squareRoot = Math.Sqrt(number);

            for (int divisor = 2; divisor <= squareRoot; divisor++)
            {
                if (number % divisor == 0)
                    return false;
            }

            return true;
        }
    }
}
