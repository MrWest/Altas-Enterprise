using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// This is a text box that allows to set a filtering.
    /// </summary>
    public class FilterBox : TextBox
    {
        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(FilterBox));

        /// <summary>
        /// Dependency property containing the value of the tooltip of instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty CleanButtonTooltipProperty = DependencyProperty.Register("CleanButtonTooltip", typeof(object), typeof(FilterBox), new PropertyMetadata(Properties.Resources.CleanFilter));


        /// <summary>
        /// Initializes a new instance of <see cref="FilterBox"/>.
        /// </summary>
        public FilterBox()
        {
            DefaultStyleKey = typeof(FilterBox);
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (FilterCommand != null && keyEventArgs.Key == Key.Enter)
                FilterCommand.Execute(Text);
            if (FilterCommand != null && keyEventArgs.Key == Key.Escape)
            {
                Text = string.Empty;
                if (FilterCommand.CanExecute(Text))
                    FilterCommand.Execute(Text);


            }
        }


        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand FilterCommand
        {
            get { return (ICommand)GetValue(FilterCommandProperty); }
            set { SetValue(FilterCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip of the Clean filter button.
        /// </summary>
        public object CleanButtonTooltip
        {
            get { return GetValue(CleanButtonTooltipProperty); }
            set { SetValue(CleanButtonTooltipProperty, value); }
        }


        /// <summary>
        /// Invoked when the template for the current <see cref="FilterBox"/> is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var clearButton = (Button)Template.FindName("clearButton", this);
            clearButton.PreviewMouseDown += (sender, e) =>
            {
                Text = string.Empty;
               
                if (FilterCommand!=null&& FilterCommand.CanExecute(Text))
                    FilterCommand.Execute(Text);

                e.Handled = true;
            };
        }
    }
}