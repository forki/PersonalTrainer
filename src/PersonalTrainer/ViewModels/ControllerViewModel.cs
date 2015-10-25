using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Figroll.PersonalTrainer.ViewModels
{
    [Export(typeof(ControllerViewModel))]
    public class ControllerViewModel: Screen
    {
        private string _backgroundImage = string.Empty;

        public string BackgroundImage
        {
            get { return _backgroundImage; }
            private set
            {
                _backgroundImage = value;
                NotifyOfPropertyChange(() => BackgroundImage);
            }
        }

        public ControllerViewModel()
        {
            BackgroundImage = @"../Resources/autotrainer.jpg";
        }
    }
}
