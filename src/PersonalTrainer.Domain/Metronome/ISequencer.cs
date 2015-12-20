namespace Figroll.PersonalTrainer.Domain.Metronome
{
    public interface ISequencer: IMetronome
    {
        void SetPattern(string pattern);
    }
}