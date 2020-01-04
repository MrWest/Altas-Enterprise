using System;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasPasswordBox:TextBox
    {
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(AtlasPasswordBox), new PropertyMetadata(null));

        public AtlasPasswordBox()
        {
            DefaultStyleKey = typeof(TextBox);

            TextChanged+=OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var password = ((TextBox)textChangedEventArgs.Source).Text;
            string hidenPass = "";
            int count = 0;
            while (count < password.Length)
            {
                hidenPass += "*";
                count++;
            }
            Password = hidenPass;
        }

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public string Password
        {
            get
            {
                

                return (string)GetValue(PasswordProperty); ;
            }
            set
            {
                SetValue(PasswordProperty, value);
               // SetValue(TextProperty, value);
            }
        }
    }
}