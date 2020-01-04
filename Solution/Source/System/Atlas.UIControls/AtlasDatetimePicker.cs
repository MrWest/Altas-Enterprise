using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CompanyName.Atlas.UIControls.Annotations;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasDatetimePicker:DatePicker,INotifyPropertyChanged
    {

        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        //public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(AtlasDatetimePicker), new PropertyMetadata(false));

        private static void OnSelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var datepicker = (AtlasDatetimePicker) d;
            datepicker.Notify();
        }

        public void Notify()
        {
            OnPropertyChanged("ShortDateText");
        }
        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty ShortDateTextProperty = DependencyProperty.Register("ShortDateText", typeof(string), typeof(AtlasDatetimePicker), new PropertyMetadata(null));

        public AtlasDatetimePicker()
        {
            DefaultStyleKey = typeof(AtlasDatetimePicker);
            SelectedDateChanged += OnSelectedDateChanged;

           
        }

        private void OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
           
            ((Popup)sender).IsOpen = false;
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    var popup = (Popup)Template.FindName("PART_Popup", this);
        //    popup.LostFocus += OnLostFocus;
        //}

        private void OnSelectedDateChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (SelectedDate != null && SelectedDate.Value != null)
            {
                ShortDateText = SelectedDate.Value.ToShortDateString();
                IsDropDownOpen = false;
                Notify();
            }
                
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string ShortDateText
        {
            get
            {
                
                return (string)GetValue(ShortDateTextProperty);
            }
            set { SetValue(ShortDateTextProperty, value); }
        }

        //public bool IsDropDownOpen
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsDropDownOpenProperty);
        //    }
        //    set { SetValue(IsDropDownOpenProperty, value); }
        //}
    }
}
