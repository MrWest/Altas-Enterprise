using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.Prism.Logging;
using Logger = CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Logging.Logger;

namespace CompanyName.Atlas
{
    /// <summary>
    /// Application's interaction. This is the object managing the whole lifetime of the Atlas application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal partial class App : IVisualResourcesServices
    {
        private readonly Bootstrapper _bootstrapper = new Bootstrapper();


        /// <summary>
        /// Merges the resource dictionary at a given uri with the current <see cref="App"/> one.
        /// </summary>
        /// <param name="dictionaryUri">The <see cref="string"/> representing the URI leading to the resource dictionary to merge.</param>
        public void Merge(string dictionaryUri)
        {
            if (dictionaryUri == null)
                throw new ArgumentNullException("dictionaryUri");

            var uri = new Uri(dictionaryUri, UriKind.RelativeOrAbsolute);
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = uri });
        }

        /// <summary>
        /// Invoked when the application has been started up. Initializes and launches the main window.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _bootstrapper.Run();
        }

        /// <summary>
        /// Invoked when the application has been shut down.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            // Signal the exiting of the application
            Logger.Instance.Log("Exited Atlas", Category.Debug, Priority.High);
            Logger.Instance.Dispose();

            // Say to the bootstrapper the application is getting close
            _bootstrapper.DatabaseContext.Dispose();

            base.OnExit(e);
        }

        [STAThread, DebuggerNonUserCode]
        static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
