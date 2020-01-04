using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasComboBox: ComboBox
    {
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(AtlasComboBox), new PropertyMetadata(null));


        public AtlasComboBox()
        {
            DefaultStyleKey = typeof(AtlasComboBox);
        }


        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public string Placeholder
        {
            get { return ((string)GetValue(PlaceholderProperty)); }
            set { SetValue(PlaceholderProperty, value); }
        }
    }
}