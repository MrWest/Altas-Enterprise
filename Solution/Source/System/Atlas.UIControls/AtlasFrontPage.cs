using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Interface for presententation and login
    /// </summary>
    public class AtlasFrontPage:ContentControl
    {
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty LoginCommandProperty = DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(AtlasFrontPage), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

          /// <summary>
        /// Initializes a new instance of <see cref="AtlasFrontPage"/>.
        /// </summary>
        public AtlasFrontPage()
        {
            DefaultStyleKey = typeof(AtlasFrontPage);
        }
    }
}
