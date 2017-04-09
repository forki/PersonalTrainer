using System;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain.Utilities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _random;

        public RandomNumberGenerator()
        {
            _random = RandomProvider.GetThreadRandom();
        }

        public bool CoinFlipIsHeads() => _random.CoinFlipIsHeads();
        public bool CoinFlipIsTails() => _random.CoinFlipIsTails();
        public bool IsPercentageChance(int percentageChance) => _random.IsPercentageChance(percentageChance);

        // Next is upper bound exclusive so make it inclusive
        // which is more intuitive for non-programmers.
        public int Between(int min, int max) => _random.Next(min, max + 1);

        public int Between(int min, int max, int step)
        {
            var minSteps = min / step;
            var maxSteps = max / step;

            return Between(minSteps, maxSteps) * step;
        }
    }
}