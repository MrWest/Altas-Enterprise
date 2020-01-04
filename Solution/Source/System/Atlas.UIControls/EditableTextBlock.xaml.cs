using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.Atlas.UIControls
{  
    /// <summary>
    /// Interaction logic for EditableTextBlock.xaml
    /// </summary>
    /// 
    [TemplatePart(Name = TextBoxControlPartName, Type = typeof(TextBox))]
    [TemplatePart(Name = LabelControlPartName, Type = typeof(Label))]
 
  
    public  class EditableTextBlock : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Identifies the dependency property to contain the value of the text being edited in this label.
        /// </summary>
        public const string TextBoxControlPartName = "PART_TextBox";
        /// <summary>
        /// Identifies the dependency property to contain the value of the text being edited in this label.
        /// </summary>
        public const string LabelControlPartName = "PART_Label";

        /// <summary>
        /// Identifies the dependency property to contain the value of the text being edited in this label.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EditableTextBlock),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

         /// <summary>
        /// Using a DependencyProperty as the backing store for InEdition.  This enables animation, styling, binding, etc...
        /// </summary>
       
        public static readonly DependencyProperty InEditionProperty =
            DependencyProperty.Register("InEdition", typeof(bool), typeof(EditableTextBlock), new PropertyMetadata(false));

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
        /// Occurs when a property value changes.
        /// </summary>
        public EditableTextBlock()
        {
            DefaultStyleKey = typeof(EditableTextBlock);
            LostFocus+=OnLostFocus;
        }
        /// <summary>
        /// Gets or sets OnLostFocus.
        /// </summary>
        private void OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
          
            FinishEdition();
        }


        /// <summary>
        /// Gets or sets the value of the text handled in this label.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets whether this control is in edition mode.
        /// </summary>
        public bool InEdition
        {
            get { return (bool)GetValue(InEditionProperty); }
            set { SetValue(InEditionProperty, value); }
        }

        private TextBox TextBoxControl
        {
            get { return (TextBox)Template.FindName(TextBoxControlPartName, this); }
        }

        private Label LabelControl
        {
            get { return (Label)Template.FindName(LabelControlPartName, this); }
        }

        /// <summary>
        /// Gets whether this control is in edition mode.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            TextBoxControl.KeyUp += TextBox_KeyUp;
            TextBoxControl.LostFocus += TextBox_LostFocus;
            LabelControl.MouseDoubleClick += LabelControl_MouseDoubleClick;
            TextBoxControl.MouseDoubleClick += LabelControl_MouseDoubleClick;
        }

        private void LabelControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBoxControl.Focus();
            this.Focus();
            StartEdition();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool acceptedValue = e.Key == Key.Enter;
            bool rejectedValue = e.Key == Key.Escape;

            if (acceptedValue || rejectedValue)
                FinishEdition(acceptedValue);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            FinishEdition();
        }

        private void StartEdition()
        {
            // Put the control in edition mode
            if (InEdition)
                return;

           
            InEdition = true;

            // Also focus the text box 
            
            TextBoxControl.Text = Text;
            TextBoxControl.SelectAll();
           
           // TextBoxControl.Focus();
        }

        /// <summary>
        /// Gets whether this OnLostFocus edition mode.
        /// </summary>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
          
        }

        private void FinishEdition(bool acceptValue = true)
        {
            if (!InEdition)
                return;

            if (acceptValue)
                Text = TextBoxControl.Text;

            InEdition = false;
        }
    }
   
}
