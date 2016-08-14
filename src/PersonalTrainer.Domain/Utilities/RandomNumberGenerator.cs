using System;
using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain.Utilities
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random RNG;

        public RandomNumberGenerator()
        {
            RNG = RandomProvider.GetThreadRandom();
        }

        public bool CoinFlipIsHeads()
        {
            return RNG.CoinFlipIsHeads();
        }

        public bool CoinFlipIsTails()
        {
            return RNG.CoinFlipIsTails();
        }

        public bool IsPercentageChance(int percentageChance)
        {
            return RNG.IsPercentageChance(percentageChance);
        }

        public int Between(int min, int max)
        {
            // Next is upper bound exclusive so make it inclusive
            // which is more intuitive for non-programmers.
            return RNG.Next(min, max + 1);
        }

        public int Between(int min, int max, int step)
        {
            var minSteps = min/step;
            var maxSteps = max/step;

            return Between(minSteps, maxSteps) * step;
        }
    }
}