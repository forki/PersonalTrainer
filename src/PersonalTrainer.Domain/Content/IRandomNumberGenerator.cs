namespace Figroll.PersonalTrainer.Domain.Content
{
    public interface IRandomNumberGenerator
    {
        bool CoinFlipIsHeads();
        bool CoinFlipIsTails();
        bool IsPercentageChance(int percentageChance);
        int Between(int min, int max);
    }
}