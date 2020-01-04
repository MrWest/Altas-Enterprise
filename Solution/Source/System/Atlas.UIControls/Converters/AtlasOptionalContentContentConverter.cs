using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CompanyName.Atlas.UIControls.Converters
{
    public class AtlasOptionalContentContentConverter : IValueConverter
    {
        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
        private string optionalNavControlUri;
        private readonly string _assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        /// <summary>
        ///     Converts a date time scale value into a boolean, according to a provided expected date time scale value.
        /// </summary>
        /// <param name="value">The <see cref="DateTimeScale" /> value to convert.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">
        ///     The <see cref="DateTimeScale" /> value that this method is expected to return true if
        ///     <paramref name="value" /> is equals to it.
        /// </param>
        /// <param name="culture">Not used.</param>
        /// <returns>True if <paramref name="value" /> and <paramref name="parameter" /> are equals, false otherwise.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter!=null)
            {
            _navigationServices.HideOptionalNavigationContent();
                //ViewModelLocationProvider.Register(typeof(InvestmentElementsView).FullName, GetModuleAccessViewModel);


                //  optionalNavControlUri = "/{0};component/Presentation/Views/ModuleAccessView.xaml".EasyFormat(_assemblyName);
                //   optionalNavControlUri = ((string)parameter).EasyFormat(_assemblyName);
                optionalNavControlUri = ((string)parameter);
                _navigationServices.SetupOptionalNavigation(optionalNavControlUri, control => ((AtlasOptionalContent)control).ElementsTreeView);

                        _navigationServices.ShowOptionalNavigationContent(); 
            }
            

            return value;
        }

        /// <summary>
        ///     Converts a boolean value into a date time scale one, according to a provided expected date time scale value.
        /// </summary>
        /// <param name="value">The <see cref="bool" /> value to convert.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">
        ///     The <see cref="DateTimeScale" /> value that this method is expected to return if
        ///     <paramref name="value" /> is true.
        /// </param>
        /// <param name="culture">Not used.</param>
        /// <returns>A <see cref="DateTimeScale" /> equals to <paramref name="parameter" /> if <paramref name="value" /> is true.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
