using System;
using System.Threading;

namespace Figroll.PersonalTrainer.Domain.Utilities
{
    public static class RandomProvider
    {
        public static Random GetThreadRandom()
        {
            return RandomWrapper.Value;
        }

        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> RandomWrapper =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));
    }
}