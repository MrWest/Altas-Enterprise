using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace CompanyName.Atlas.UIControls
{
    public class UserSelectionForm: Popup
    {
       // public ICommand AssociatedCommand { get; set; }

        public string AssemblyName { get; set; }

       // private IEnumerable<IAtlasUserPresenter> users;
        //private ListBox _userListBox = new ListBox();
        //public ListBox UserListBox { get { return _userListBox; } }
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty UserListBoxProperty = DependencyProperty.Register("UserListBox", typeof(ListBox), typeof(UserSelectionForm), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty AssociatedCommandProperty = DependencyProperty.Register("AssociatedCommand", typeof(ICommand), typeof(UserSelectionForm), new PropertyMetadata(null));

        public UserSelectionForm()
        {
            DefaultStyleKey = typeof(UserSelectionForm);

            Loaded += UserSelectionForm_Loaded;
             // UserListBox.SetBinding(ListBox.ItemsSourceProperty, new Binding("ItemsSource") { Source = _categoriesSelector });
            IsOpen = false;
           
        }

        private void UserSelectionForm_Loaded(object sender, RoutedEventArgs e)
        {
            UserListBox = new ListBox();
            UserListBox.MouseDoubleClick += UserSelectionForm_MouseDoubleClick;
            var userProvider = ServiceLocator.Current.GetInstance<IAtlasUserViewModel>();
            userProvider.Load();
            //user
            UserListBox.ItemsSource = userProvider.Items;
            //selecting only the users allowed for the calling module
            UserListBox.Items.Filter = (x => ((IAtlasUserPresenter)x).AllowedModules.Any(u => u.ModuleName.Equals(AssemblyName)));
            Child = UserListBox;
        }

        private void UserSelectionForm_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(UserListBox.SelectedItem != null && AssociatedCommand != null)
            {
                AssociatedCommand.Execute(UserListBox.SelectedItem);
                IsOpen = false;
            }
        }

        /// <summary>
        /// Gets or sets wheher the total summary is shown.
        /// </summary>
        public ListBox UserListBox
        {
            get { return (ListBox)GetValue(UserListBoxProperty); }
            set { SetValue(UserListBoxProperty, value); }
        }
        /// <summary>
        /// Gets or sets wheher the total summary is shown.
        /// </summary>
        public ICommand AssociatedCommand
        {
            get { return (ICommand)GetValue(AssociatedCommandProperty); }
            set { SetValue(AssociatedCommandProperty, value); }
        }
    }
}
