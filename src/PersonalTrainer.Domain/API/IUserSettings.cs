namespace Figroll.PersonalTrainer.Domain.API
{
    public interface IUserSettings
    {
        bool UseTTS { get; }
        string DefaultVoice { get; }
        string ContentLocation { get; }
    }
}