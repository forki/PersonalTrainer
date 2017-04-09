using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using NLog;

namespace Figroll.PersonalTrainer
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly Logger _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType?.ToString());

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void UIOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Fatal("UI thread exception was unhandled and applicaton will close");
            HandleFatalException(e.Exception);

            MessageBox.Show("Your PERSONAL TRAINER session has been busted by the Feds on suspicion of perfomance enhancing drugs :-(" +
                            Environment.NewLine + "The inditement says " + e.Exception.Message, "Fatal Error");

            e.Handled = false;
        }

        private void HandleFatalException(Exception e)
        {
            _logger.Fatal(e, e.StackTrace);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Fatal("App domain exception was unhandled and applicaton will close");

            if (e.ExceptionObject is Exception)
            {
                HandleFatalException((Exception) e.ExceptionObject);
            }
            else
            {
                // Non CLR exception.
                _logger.Fatal("Exception object was null");
            }

            if (!e.IsTerminating)
            {
                Environment.Exit(1);
            }
        }
    }
}