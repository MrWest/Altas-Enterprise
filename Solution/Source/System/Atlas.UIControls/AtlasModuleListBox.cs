using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasModuleListBox: ListBox, INotifyPropertyChanged
    {
        public AtlasModuleListBox()
        {
            DefaultStyleKey = typeof(ListBox);
           
            PropertyChanged = OnPropertyHasChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (Items.Count > 0)
                SelectedItem = Items[0];
            OnPropertyChanged("SelectedItem");
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            if (Items.Count > 0)
                SelectedItem = Items[0];
            OnPropertyChanged("SelectedItem");
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
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