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
    /// Holds the login information
    /// </summary>
    public class LoginInfoHolder : ContentControl, ILoginInfoHolder
    {
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
       public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(LoginInfoHolder), new PropertyMetadata(null));
       /// <summary>
       /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
       /// </summary>
       public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(String), typeof(LoginInfoHolder), new PropertyMetadata(null));

        /// <summary>
       /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
       /// </summary>
       public static readonly DependencyProperty PasswordChangedProperty = DependencyProperty.Register("PasswordChanged", typeof(RoutedEventHandler), typeof(LoginInfoHolder), new PropertyMetadata(null));

       /// <summary>
       /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
       /// </summary>
       public static readonly DependencyProperty PasswordBoxProperty = DependencyProperty.Register("PasswordBox", typeof(PasswordBox), typeof(LoginInfoHolder), new PropertyMetadata(null));

       /// <summary>
       /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
       /// </summary>
       public static readonly DependencyProperty UnsuccessfullTextProperty = DependencyProperty.Register("UnsuccessfullText", typeof(string), typeof(LoginInfoHolder), new PropertyMetadata(null));

       /// <summary>
       /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
       /// </summary>
       public string UserName
       {
           get { return (string)GetValue(UserNameProperty); }
           set { SetValue(UserNameProperty, value); }
       }
       /// <summary>
       /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
       /// </summary>
       public String Password
       {
           get
           {
               return null;
               //var passwordBox = (PasswordBox)FindName("PasswordBox");
               //Password = passwordBox.Password;
               //return (String)GetValue(PasswordProperty);
           }
           set { SetValue(PasswordProperty, value); }
       }

       public  RoutedEventHandler PasswordChanged
           {
               get { return (RoutedEventHandler)GetValue(PasswordChangedProperty); }
               set { SetValue(PasswordChangedProperty, value); }
           }

       public PasswordBox PasswordBox
       {
           get { return (PasswordBox)GetValue(PasswordBoxProperty); }
           set { SetValue(PasswordBoxProperty, value); }
       }
       /// <summary>
       /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
       /// </summary>
       public string UnsuccessfullText
       {
           get { return (string)GetValue(UnsuccessfullTextProperty); }
           set { SetValue(UnsuccessfullTextProperty, value); }
       }
           /// <summary>
        /// Initializes a new instance of <see cref="AtlasFrontPage"/>.
        /// </summary>
        public LoginInfoHolder()
        {
            DefaultStyleKey = typeof(LoginInfoHolder);
            PasswordChanged=OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            Password =((PasswordBox) sender).Password;
        }
    }
}
