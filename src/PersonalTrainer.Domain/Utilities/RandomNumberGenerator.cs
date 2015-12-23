using System;
using Figroll.PersonalTrainer.Domain.API;
using Figroll.PersonalTrainer.Domain.Content;

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
            return RNG.Next(min, max);
        }
    }
}