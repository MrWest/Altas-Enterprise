using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    ///     Represents the converter that is used to get the content for <see cref="AtlasWindow" /> instances depending of the
    ///     navigation configuration it has.
    /// </summary>
    public class AtlasWindowContentConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Gets the content control from two other controls, initializing the datacontext of it if there is possible to be
        ///     done.
        /// </summary>
        /// <param name="values">
        ///     The two controls where the first is expected to be control to return. The second control is the one that contains
        ///     object
        ///     to be set as datacontext of the first.
        /// </param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">
        ///     A <see cref="Func{T, TResult}" /> which is a method that allows to get the control that contains
        ///     in its SelectedItem property the datacontext for the one to be returned.
        /// </param>
        /// <param name="culture">Not used.</param>
        /// <returns>A control that is the content for the <see cref="AtlasWindow" /> using this converter for such purpose.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var navigationItem = values[0] as SecondLevelNavItem;
            if (navigationItem == null)
                return null;

            var navigationItemContent = navigationItem.Content as ContentControl;
            if (navigationItemContent == null)
                return null;

            Func<object, object> getDataContext = (parameter as Func<object, object>) ?? (x => x);
            object optionalNavigationControl = getDataContext.Invoke(values.Length >= 2 ? values[1] : null);

            if (optionalNavigationControl!=null && optionalNavigationControl.GetType().Implements<ICanAdd>())
            {
                ((ICanAdd) optionalNavigationControl).CanAdd = navigationItem.Can;
            }
            navigationItemContent.SetBinding(FrameworkElement.DataContextProperty, new Binding("SelectedItem")
            {
                Source = optionalNavigationControl
            });
             navigationItemContent.DataContextChanged += NavigationItemContentOnDataContextChanged;

            return navigationItemContent;
        }

        private void NavigationItemContentOnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation(0.8,1,TimeSpan.FromSeconds(0.2));
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(-30, 0, 30, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2));

            ((ContentControl)sender).BeginAnimation(ContentControl.OpacityProperty,opacityAnimation);
            ((ContentControl)sender).BeginAnimation(ContentControl.MarginProperty, thicknessAnimation);

        }

        /// <summary>
        ///     Not supported
        /// </summary>
        /// <exception cref="NotSupportedException">Always throw the exception.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}