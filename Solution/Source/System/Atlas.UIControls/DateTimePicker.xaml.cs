using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Interaction logic for DateTimePicker.xaml
    /// </summary>
    public partial class DateTimePicker :  INotifyPropertyChanged
    {
        public DateTimePicker()
        {
            InitializeComponent();
            MouseLeave += OnLostFocus;
            
        }

        private void OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            DateComboBox.IsDropDownOpen = false;
        }

        private void Calendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateComboBox.IsDropDownOpen = false;
            if (sender.GetType() == typeof(Calendar) && ((Calendar)sender).SelectedDate != null && SelectedDateChanged!=null)
            {
                ((Calendar)sender).Tag = ((Calendar)sender).SelectedDate.Value.ToShortDateString();

                SelectedDateChanged.Invoke(SelectedDateChanged.Target, e);
                OnPropertyChanged("SelectedDate");
            }
           
        }

        public event EventHandler<SelectionChangedEventArgs>  SelectedDateChanged;


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

        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDatepProperty); }
            set
            {
                SetValue(SelectedDatepProperty, value);
                OnPropertyChanged("SelectedDate");
            }
            
        }
        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedDatepProperty = DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(DateTimePicker), new PropertyMetadata(OnSelectedDateChanged));

        private static void OnSelectedDateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((DateTimePicker)o).UpdateSelection(e);
        }
        private void UpdateSelection(DependencyPropertyChangedEventArgs e)
        {
            //((Calendar) DateComboBox.SelectedItem).SelectedDate = (DateTime) e.NewValue;
        }

        private void DateTimePicker_OnLoaded(object sender, RoutedEventArgs e)
        {
            SelectedDateChanged += Calendar_OnSelectedDatesChanged;
        }
    }
}
