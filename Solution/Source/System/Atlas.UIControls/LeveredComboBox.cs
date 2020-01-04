using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Visuals;
using CompanyName.Atlas.UIControls.Annotations;
using Microsoft.Practices.Prism.Mvvm;

namespace CompanyName.Atlas.UIControls
{
    public class LeveredComboBox: ComboBox, INotifyPropertyChanged
    {
        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static  DependencyProperty SubLevelProperty = DependencyProperty.Register("SubLevel", typeof(object), typeof(LeveredComboBox), new PropertyMetadata(OnSubLevelChanged));
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(LeveredComboBox), new PropertyMetadata(null));

       

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public string Placeholder
        {
            get { return ((string)GetValue(PlaceholderProperty)); }
            set { SetValue(PlaceholderProperty, value); }
        }
        private static void OnSubLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           // ((LeveredComboBox)d).Tag = e.NewValue;
           // ((LeveredComboBox)d).OnPropertyChanged(()=>SubLevelProperty);
            //((LeveredComboBox)d).OnPropertyChanged("SelectedValue");
           
        }

         
        /// <summary>
        /// Gets the selected Item for the comboBox subItems List
        /// </summary>

        [Bindable(true)]
        public object SubLevel
        {
            get {return (object)GetValue(SubLevelProperty);}
            set { SetValue(SubLevelProperty, value); }
        }
        public LeveredComboBox()
        {
            DefaultStyleKey = typeof (LeveredComboBox);
            PropertyChanged = OnPropertyHasChanged;

            

        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Invoked when a property in the current entity presenter view model has been notified as changed.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">Arguments containing the details of the event.</param>
        protected virtual void OnPropertyHasChanged(object sender, PropertyChangedEventArgs e)
        {

            
        }
    }
}
