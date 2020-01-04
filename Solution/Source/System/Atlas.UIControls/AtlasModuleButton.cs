using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasModuleButton: Button
    {
        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="AtlasModuleButton"/>.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(AtlasModuleButton), new PropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of <see cref="AtlasModuleButton"/>.
        /// </summary>
        public AtlasModuleButton()
        {
            DefaultStyleKey = typeof(AtlasModuleButton);
        }

        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }
}
