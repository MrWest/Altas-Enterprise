using System;
using System.Globalization;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Features;

namespace CompanyName.Atlas.Contracts.Presentation.Services
{
    /// <summary>
    /// Contract for the services regarding navigation configuration and interaction, in order to communicate in such terms
    /// the system and its modules.
    /// </summary>
    public interface INavigationServices: ICopyPasteableController
    {
        /// <summary>
        /// Setups the navigation configuration using default parameters.
        /// </summary>
        void SetupNavigation();

        /// <summary>
        /// Sets up the navigation configuration using the navigation resources located at a xaml laying in the provided URI.
        /// </summary>
        /// <param name="navigationSetupUri">
        /// A URI pointing to the module's navigation resources. This navigation resources declaratively must specify 
        /// all the navigation configuration.
        /// </param>
        void SetupNavigation(string navigationSetupUri);

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
        void SetupOptionalNavigation(string optionalNavigationUri, Func<object, object> getContentMethod);

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
        void SetupSubSystemMenu(string subSystemMenuUri, Func<object, object> getContentMethod);
  
        /// <summary>
        ///     Displays the optional navigation content.
        /// </summary>
        void ShowOptionalNavigationContent();

        /// <summary>
        ///     Hides the optional navigation content.
        /// </summary>
        void HideOptionalNavigationContent();

        string OptionalUri { get; set; }

        
       
    }
}
