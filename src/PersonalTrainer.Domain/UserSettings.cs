using Figroll.PersonalTrainer.Domain.API;

namespace Figroll.PersonalTrainer.Domain
{
    public class UserSettings : IUserSettings
    {
        public bool UseTTS { get; }
        public string DefaultVoice { get; }
        public string ContentLocation { get; }

        public UserSettings()
        {
            UseTTS = true;
            DefaultVoice = "Amy";
            ContentLocation = @"d:\milovana\content\new";
        }
    }
}