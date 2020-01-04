using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// This kind of item represents an entry in the first level navigation in a <see cref="AtlasWindow"/>.
    /// </summary>
    public class FirstLevelNavItem : Control
    {
        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="FirstLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(FirstLevelNavItem), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the <see cref="string"/> value representing the text displayed by instances of
        /// <see cref="FirstLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FirstLevelNavItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Dependency property containing the <see cref="ObservableCollection{SecondLevelNavItem}"/> in which are contained the
        /// navigation items of the second level of a certain <see cref="FirstLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty SubNavigationProperty = DependencyProperty.Register("SubNavigation", typeof(ObservableCollection<SecondLevelNavItem>), typeof(FirstLevelNavItem), new PropertyMetadata(null));


        /// <summary>
        /// Initializes a new instance of <see cref="FirstLevelNavItem"/>.
        /// </summary>
        public FirstLevelNavItem()
        {
            DefaultStyleKey = typeof(FirstLevelNavItem);

            SubNavigation = new ObservableCollection<SecondLevelNavItem>();
        }


        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="FirstLevelNavItem"/>.
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text to be displayed by the current <see cref="FirstLevelNavItem"/>.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the list of navigation items of the second level that are contained by the current 
        /// <see cref="FirstLevelNavItem"/>.
        /// </summary>
        public ObservableCollection<SecondLevelNavItem> SubNavigation
        {
            get { return (ObservableCollection<SecondLevelNavItem>)GetValue(SubNavigationProperty); }
            set { SetValue(SubNavigationProperty, value); }
        }
    }
}
