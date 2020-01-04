using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Properties;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using IView = Microsoft.Practices.Prism.Mvvm.IView;

namespace CompanyName.Atlas.Views
{
    /// <summary>
    /// Interaction logic for Atlas main UI.
    /// </summary>
    internal partial class Shell : IView//, INavigationServices
    {

        public UnityBootstrapper UnityBootstrapper { get; set; }
        /// <summary>
        /// Initializes a new instance of the Atlas main UI.
        /// </summary>
        public Shell()
        {
            InitializeComponent();
           

            //  SetupNavigation();
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    var moduleview = (AtlasModuleView)Template.FindName("AtlasModuleView", this);
        //    if (moduleview != null && UnityBootstrapper != null)
        //    {
        //        UnityBootstrapper.Container.RegisterInstance(typeof(INavigationServices), "AtlasModuleView", moduleview, null);
               
        //    }
        //}

        private static string GetFullPath(string relativePath)
        {
           
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        /// <summary>
        /// Setups the navigation configuration using default parameters.
        /// </summary>
        //public void SetupNavigation()
        //{
        //    Assembly callingAssembly = Assembly.GetCallingAssembly();
        //    string assembly = callingAssembly.GetName().Name;

        //    SetupNavigation("/{0};component/Presentation/Views/Navigation.xaml".EasyFormat(assembly));
        //}

        /// <summary>
        /// Setups the navigation configuration using the navigation resources located at a xaml laying in the provided uri.
        /// </summary>
        /// <param name="navigationSetupUri">
        /// A URI pointing to the module's navigation resources. This navigation resources declaratively must specify 
        /// all the navigation configuration.
        /// </param>
        //public void SetupNavigation(string navigationSetupUri)
        //{
        //    var navigationResources = (ResourceDictionary)Application.LoadComponent(new Uri(navigationSetupUri, UriKind.RelativeOrAbsolute));
        //    var navigationConfig = (IList)navigationResources[GlobalParameters.NavigationResourceKey];

        //    //foreach (FirstLevelNavItem navigationItem in navigationConfig)
        //    //    Navigation.Add(navigationItem);
        //}

        /// <summary>
        /// Sets up an optional navigation control using resources located the given xaml's URI.
        /// </summary>
        /// <param name="optionalNavigationUri">
        /// The URI to the xaml where the navigation setup is located.
        /// </param>
        /// <param name="getContentMethod">
        /// A method that allows to get the control containing the data context of the optional navigation structure.
        /// For instance: if a certain window uses optional navigation, its content's data context will be set according to the
        /// control returned by <paramref name="getContentMethod"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="optionalNavigationUri"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="optionalNavigationUri"/> specifies an invalid URI.</exception>
        //public void SetupOptionalNavigation(string optionalNavigationUri, Func<object, object> getContentMethod)
        //{
        //    //OptionalNavigationContent = (Control)Application.LoadComponent(new Uri(optionalNavigationUri, UriKind.RelativeOrAbsolute));
        //    //ContentConverterMethod = getContentMethod;
        //}
        /// <summary>
        ///     Displays the optional navigation content.
        /// </summary>
        //public void ShowOptionalNavigationContent()
        //{
        //    //OptionalNavigationVisibility = Visibility.Visible;
        //}

        /// <summary>
        ///     Hides the optional navigation content.
        /// </summary>
        //public void HideOptionalNavigationContent()
        //{
        //    //OptionalNavigationVisibility = Visibility.Collapsed;
        //}

        #region Command event handlers

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
