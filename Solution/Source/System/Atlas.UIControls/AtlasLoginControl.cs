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
    public class AtlasLoginControl : LoginInfoHolder, ILoginInfoHolder
    {

        
       
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
       public static readonly DependencyProperty LoginCommandProperty = DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(AtlasLoginControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
       public static readonly DependencyProperty LoginViewProperty = DependencyProperty.Register("LoginView", typeof(AtlasLoginViewState), typeof(AtlasLoginControl), new PropertyMetadata(AtlasLoginViewState.UserLabel));

        public AtlasLoginControl()
        {
            DefaultStyleKey = typeof (AtlasLoginControl);
            KeyDown+=OnKeyDown;
            MouseDown+=OnMouseDown;

            this.Focus();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            UnsuccessfullText = null;
            //PasswordBox.FlowDirection = FlowDirection.LeftToRight;
            if (LoginView == AtlasLoginViewState.UserLabel)
            {
                LoginView = AtlasLoginViewState.UserText;
                //(TextBox) Template.FindName("UserTextBox", this) ??
                ((TextBox)Template.FindName("UserTextBox", this)).Focus();
            }
            else if (LoginView == AtlasLoginViewState.UserText)
            {
               LoginView = AtlasLoginViewState.PassLabel;
                ((TextBlock)Template.FindName("Password", this)).Focus();
            }
                
            else
            if (LoginView == AtlasLoginViewState.PassLabel)
            {
                LoginView = AtlasLoginViewState.PassText;
                // this.Focus();
                //  PasswordBox.Focus();
                var password = (PasswordBox)Template.FindName("PasswordBox", this);

                password.Focus();

            }
            else
            //if (LoginView == AtlasLoginViewState.PassText)
            //if (LoginView == AtlasLoginViewState.PassText)
            {
                PasswordBox.Password = ((PasswordBox)Template.FindName("PasswordBox", this)).Password;
                if (LoginCommand != null)
                    LoginCommand.Execute(this);
                LoginView = AtlasLoginViewState.UserLabel;
            }

           
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            UnsuccessfullText = null;
            //PasswordBox.FlowDirection = FlowDirection.LeftToRight;
            if (keyEventArgs.Key == Key.Enter || keyEventArgs.Key == Key.Tab)
            {
                if (LoginView == AtlasLoginViewState.UserLabel)
                {
                    LoginView = AtlasLoginViewState.UserText;
                    //(TextBox) Template.FindName("UserTextBox", this) ??
                    ((TextBox) Template.FindName("UserTextBox", this)).Focus();
                }
                    
                else if (LoginView == AtlasLoginViewState.UserText)
                {
                      LoginView = AtlasLoginViewState.PassLabel;
                      ((TextBlock)Template.FindName("Password", this)).Focus();
                    
                   // this.OnKeyDown(keyEventArgs);
                }
                   
                else if (LoginView == AtlasLoginViewState.PassLabel)
                {
                    LoginView = AtlasLoginViewState.PassText;
                 //((ContentPresenter) Template.FindName("PasswordBox", this)).Focus();
                  //  PasswordBox.SelectAll();
                   // ((PasswordBox)contentTemplate.FindName("PasswordBox")).Focus();
                    PasswordBox.Focus();
                  //  PasswordBox.CaretBrush =PasswordBox.BorderBrush;
                    PasswordBox.Focus();
                    PasswordBox.Focus();
                    ((PasswordBox)Template.FindName("PasswordBox", this)).Focus();
                }
                   
                else
                    //if (LoginView == AtlasLoginViewState.PassText)
                {
                   PasswordBox.Password = ((PasswordBox)Template.FindName("PasswordBox", this)).Password;
                    if(LoginCommand!=null)
                        LoginCommand.Execute(this);
                    LoginView = AtlasLoginViewState.UserLabel;
                }
                   
            }
            //else
            if (keyEventArgs.Key == Key.Escape)
            {
                LoginView = AtlasLoginViewState.UserLabel;
            }
            //else
            //{
            //    var key = keyEventArgs.Key.ToString().Replace("NumPad","");
            //    if (LoginView==AtlasLoginViewState.PassText && key.Length == 1)
            //    {
            //        PasswordBox.Password = PasswordBox.Password + key;
                    
            //        PasswordBox.CaretBrush = PasswordBox.Background;
            //        //PasswordBox.FlowDirection = FlowDirection.RightToLeft;
                    
            //    }
            //    //if (keyEventArgs.Key == Key.Back && PasswordBox.Password.Length>0)
            //    //{
            //    //    PasswordBox.Password = PasswordBox.Password.Remove(PasswordBox.Password.Length - 1);
            //    //}

            //}
     
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //var border = (Border)Template.FindName("Border", this);
            //if (border != null)
            //    border.MouseDown += OnMouseDown;

            var userTextBox = (TextBox)Template.FindName("UserTextBox", this);
            if (userTextBox != null)
            {
             //   userTextBox.MouseDown += OnMouseDown;
               // userTextBox.KeyDown += OnKeyDown;
            }
                

            var userTextBlock = (TextBlock)Template.FindName("UserTextBlock", this);
            if (userTextBlock != null)
            {
             //   userTextBlock.MouseDown += OnMouseDown;
                userTextBlock.KeyDown += OnKeyDown;
                userTextBlock.Focus();
            }
               

            var password = (TextBlock)Template.FindName("Password", this);
            if (password != null)
            {
              //  password.MouseDown += OnMouseDown;
                password.KeyDown += OnKeyDown;
            }
               

            //var passwordBox = (ContentPresenter)Template.FindName("PasswordBox", this);
            //if (passwordBox != null)
            //    passwordBox.MouseDown += OnMouseDown;

            this.Focus();
        }

        

        
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public AtlasLoginViewState LoginView
        {
            get { return (AtlasLoginViewState)GetValue(LoginViewProperty); }
            set { SetValue(LoginViewProperty, value); }
        }
    }
}
