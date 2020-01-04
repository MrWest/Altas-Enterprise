using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Features;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasCacheTextBox:TextBox
    {
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty CacheSourceProperty = DependencyProperty.Register("CacheSource", typeof(IList<string>), typeof(AtlasCacheTextBox));

        public AtlasCacheTextBox()
        {
            DefaultStyleKey = typeof(AtlasCacheTextBox);
            KeyDown += TextboxOnKeyDown;
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public IList<string> CacheSource
        {
            get { return (IList<string>)GetValue(CacheSourceProperty); }
            set { SetValue(CacheSourceProperty, value); }
        }

        private Popup popup;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var textbox = (TextBox)Template.FindName("MainTextBox", this);
            textbox.TextChanged += TextboxOnTextChanged;
            textbox.KeyDown+=TextboxOnKeyDown;
         
            _listBox = (ListBox)Template.FindName("CacheListBox", this);
            _listBox.SelectionChanged+=LisboxOnSelectionChanged;

             popup = (Popup)Template.FindName("CachePopup", this);
            
        }

        private int _selectdLisBoxItemIndex;
        private ListBox _listBox;

        private void TextboxOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Escape)
                popup.IsOpen = false;

            if (keyEventArgs.Key == Key.Down && _listBox.Items.Count > 0)
            {

                _selectdLisBoxItemIndex = _listBox.SelectedIndex;
                if (_selectdLisBoxItemIndex + 1 == _listBox.Items.Count)
                {
                    _selectdLisBoxItemIndex = 0;
                    _listBox.SelectedIndex = _selectdLisBoxItemIndex;
                }
                else
                {
                    _selectdLisBoxItemIndex++;
                    _listBox.SelectedIndex = _selectdLisBoxItemIndex;
                }
            }

            if (keyEventArgs.Key == Key.Up && _listBox.Items.Count > 0)
            {

                _selectdLisBoxItemIndex = _listBox.SelectedIndex;
                if (_selectdLisBoxItemIndex - 1 < 0)
                {
                    _selectdLisBoxItemIndex = _listBox.Items.Count - 1;
                    _listBox.SelectedIndex = _selectdLisBoxItemIndex;
                }
                else
                {
                    _selectdLisBoxItemIndex--;
                    _listBox.SelectedIndex = _selectdLisBoxItemIndex;
                }
            }

        }

        private void TextboxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (popup != null && !popup.IsOpen && Text != ((TextBox)sender).Text)
                popup.IsOpen = true;
            Text=((TextBox) sender).Text;
        }

        private void LisboxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (((ListBox) sender).SelectedItem != null)
            {
                Text = ((ListBox)sender).SelectedItem.ToString();
                popup.IsOpen = false;
            }

               
        }
    }
}