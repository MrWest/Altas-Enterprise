using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.UIControls.Annotations;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasEasyTextBox: TextBox,INotifyPropertyChanged
    {
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(AtlasEasyTextBox), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register("Placeholder", typeof(string), typeof(AtlasEasyTextBox), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return ((IEnumerable) GetValue(ItemsSourceProperty)); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public string Placeholder
        {
            get { return ((string)GetValue(PlaceholderProperty)); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty MaxItemsNumberProperty = DependencyProperty.Register("MaxItemsNumber", typeof(int), typeof(AtlasEasyTextBox), new PropertyMetadata(10));

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public int MaxItemsNumber
        {
            get { return (int)GetValue(MaxItemsNumberProperty); }
            set { SetValue(MaxItemsNumberProperty, value); }
        }

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty NomenclatorSourceProperty = DependencyProperty.Register("NomenclatorSource", typeof(INomenclatorViewModel), typeof(AtlasEasyTextBox), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public INomenclatorViewModel NomenclatorSource
        {
            get { return (INomenclatorViewModel)GetValue(NomenclatorSourceProperty); }
            set { SetValue(NomenclatorSourceProperty, value); }
        }

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty StartsOpenProperty = DependencyProperty.Register("StartsOpen", typeof(bool), typeof(AtlasEasyTextBox), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public bool StartsOpen
        {
            get { return (bool)GetValue(StartsOpenProperty); }
            set { SetValue(StartsOpenProperty, value); }
        }
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(object), typeof(AtlasEasyTextBox), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public object SelectedObject
        {
            get { return GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }

        private BackgroundWorker _backgroundWorker;
        public AtlasEasyTextBox()
        {
            DefaultStyleKey = typeof(AtlasEasyTextBox);
            //KeyDown+=AtlasEasyTextBoxKeyDown;
            PropertyChanged +=OnPropertyChanged;
            //_timer = new Timer(1000);
            //_timer.Elapsed += TimerOnElapsed;
            
            _backgroundWorker = new BackgroundWorker();
            
            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork; 
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
           
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            var rsltList = runWorkerCompletedEventArgs.Result as IList<INomenclatorPresenter>;
            string currenText = ((TextBox) Template.FindName("PART_NameText", this)).Text;
            if (executedText != currenText)
            {
                _lastPassedTextChangedEventArgs = currenText;
                _backgroundWorker.RunWorkerAsync(NomenclatorSource);
            }
            else
            {
                DoFilter(rsltList);
            }

           
               
        }

        private string executedText = "";
        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var nomenclatorViewModel = doWorkEventArgs.Argument as INomenclatorViewModel;
            nomenclatorViewModel.Text = _lastPassedTextChangedEventArgs;
            executedText = _lastPassedTextChangedEventArgs;
            var rsltList = new List<INomenclatorPresenter>();
            nomenclatorViewModel.FillMe(rsltList);
            doWorkEventArgs.Result = rsltList;
        }


        private int  seconds =0;
        private bool allow = true;
      

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            
        }

        //private void AtlasEasyTextBoxKeyDown(object sender, KeyEventArgs keyEventArgs)
        //{

        //}
        private Popup _popup;
        private TextBox _textBox;
        private ListBox _listBox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _textBox = ((TextBox)Template.FindName("PART_NameText", this));
            _textBox.TextChanged += OnTextChanged;
            _textBox.PreviewKeyDown += OnKeyDownForTextBox;
            _textBox.GotFocus += OnGotFocus;
            _textBox.LostFocus += OnLostFocus;
            _listBox = ((ListBox)Template.FindName("PART_EasyListBox", this));
            _listBox.PreviewKeyDown += OnKeyDown;
            _listBox.SelectionChanged += ListBoxOnSelectionChanged;
            _listBox.LostFocus += OnLostFocus;
            _popup = ((Popup)Template.FindName("Popup", this));
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var selectedItem = ((ListBox)sender).SelectedItem;
            if (selectedItem != null&&!keyboard)
            {
                var nomenclator = ((selectedItem as IPresenter).Object as INomenclator);
                if (nomenclator != null)
                {
                    Text = nomenclator.ToString();
                    SelectedObject = nomenclator.Id;

                    OnPropertyChanged("SelectedObject");
                    OnPropertyChanged("Text");
                    _popup.StaysOpen = false;
                    _popup.IsOpen = false;
                }
            }
        }


        private void OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {

            if (_popup.IsOpen && !_listBox.IsMouseOver)
            {
                _popup.IsOpen = false;
                _popup.StaysOpen = false;
            }
            firsttime = true;
        }

        private void OnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            NomenclatorSource.MaxNumber = MaxItemsNumber;
            NomenclatorSource.Text = ((TextBox)sender).Text;
            ItemsSource = NomenclatorSource.Items;

            _popup.IsOpen = true;
            _popup.StaysOpen = true;



        }

        private int _selectdLisBoxItemIndex;

        private bool keyboard;
        private void OnKeyDownForTextBox(object sender, KeyEventArgs keyEventArgs)
        {
            keyboard = true;
           // var _listBox = ((ListBox)Template.FindName("PART_EasyListBox", this));
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
                if (_selectdLisBoxItemIndex - 1 < 0 )
                {
                    _selectdLisBoxItemIndex = _listBox.Items.Count-1;
                    _listBox.SelectedIndex = _selectdLisBoxItemIndex;
                }
                else
                {
                    _selectdLisBoxItemIndex--;
                    _listBox.SelectedIndex = _selectdLisBoxItemIndex;
                }
            }


            var selectedItem = ((ListBox) Template.FindName("PART_EasyListBox", this)).SelectedItem;
            if (keyEventArgs.Key == Key.Enter && selectedItem!=null)
            {
                SelectedObject = ((selectedItem as IPresenter).Object as INomenclator).Id;
                Text = ((selectedItem as IPresenter).Object as INomenclator).ToString();
               // SelectedObject = nomenclator.Id;

                OnPropertyChanged("SelectedObject");
                OnPropertyChanged("Text");
                _popup.StaysOpen = false;
                _popup.IsOpen = false;
            }

            if (keyEventArgs.Key == Key.Escape)
            {
                var nomenclator = DataContext as INomenclator;
                if (!Equals(nomenclator, null))
                {
                    _textBox.Text = nomenclator.Name;
                }

                OnPropertyChanged("SelectedObject");
                OnPropertyChanged("Text");
                _popup.StaysOpen = false;
                _popup.IsOpen = false;
            }

            keyboard = false;
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var selectedItem = ((ListBox) sender).SelectedItem;
            if (selectedItem != null)
            {
                var nomenclator = ((selectedItem as IPresenter).Object as INomenclator);
                if (nomenclator != null)
                {
                    Text = nomenclator.ToString();
                    SelectedObject = nomenclator.Id;

                    OnPropertyChanged("SelectedObject");
                    OnPropertyChanged("Text");
                    _popup.StaysOpen = false;
                    _popup.IsOpen = false;
                }
            }
           
           
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Enter)
            {
                SelectedObject = ((((ListBox)sender).SelectedItem as IPresenter).Object as INomenclator).Id;
                OnPropertyChanged("SelectedObject");
                _popup.StaysOpen = false;
                _popup.IsOpen = false;
            }
           
        }

        //private Timer _timer;
        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (NomenclatorSource != null && IsLoaded && !(_listBox.IsMouseOver && ((TextBox)textChangedEventArgs.Source).Text==""))
            {
                _lastPassedTextChangedEventArgs = ((TextBox)textChangedEventArgs.Source).Text;
                if (!_backgroundWorker.IsBusy)
                {
                    NomenclatorSource.MaxNumber = MaxItemsNumber;
                    _backgroundWorker.RunWorkerAsync(NomenclatorSource);
                }
              

                //if (allow)
                //{
                //    allow = false;
                //    _timer.Start();
                //    if(firsttime)
                //DoFilter(((TextBox)textChangedEventArgs.Source).Text);


                //    firsttime = false;
                //}

                

            }

        }

        private bool firsttime = true;
        private bool timerFree = true;

        //private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        //{
         
        //    if (seconds >= 2)
        //    {
        //        if (timerFree)
        //        {
        //            timerFree = false;
        //            DoFilter(_lastPassedTextChangedEventArgs);
        //            timerFree = true;

                 

        //        }

        //        allow = true;
        //        seconds = 0;
        //        _timer.Stop();
        //    }
        //    seconds++;
        //}

        private string _lastPassedTextChangedEventArgs;
        private void DoFilter(IList<INomenclatorPresenter> nomenclatorList)
        {

            //if (_lastPassedTextChangedEventArgs!=null && textChangedEventArgs ==  _lastPassedTextChangedEventArgs)
            //{
            //    _timer.Stop();
            //    return;
            //}
               
            
            //NomenclatorSource.MaxNumber = MaxItemsNumber;
            //NomenclatorSource.Text = textChangedEventArgs;
            ItemsSource = nomenclatorList;

            var listBox = (ListBox) Template.FindName("PART_EasyListBox", this);
            int code =
                nomenclatorList.Cast<INomenclatorPresenter>()
                    .Count(x => !Equals(x.Code, null) && x.Code.ToString().ToLower().Contains(NomenclatorSource.Text.ToLower()));
            int name =
                nomenclatorList.Cast<INomenclatorPresenter>()
                    .Count(x => !Equals(x.Name, null) && x.Name.ToLower().Contains(NomenclatorSource.Text.ToLower()));
            int desc =
               nomenclatorList.Cast<INomenclatorPresenter>()
                    .Count(
                        x => !Equals(x.Description, null) && x.Description.ToLower().Contains(NomenclatorSource.Text.ToLower()));
            if (code > name || code > desc)
            {
                var datatemplate = this.FindResource(
                    "CodeTemplate") as DataTemplate;
                listBox.ItemTemplate = datatemplate;
            }
            else
            {
                var datatemplate = this.FindResource(
                    "NameTemplate") as DataTemplate;
                listBox.ItemTemplate = datatemplate;
            }


            OnPropertyChanged("Text");
            _popup.StaysOpen = false;
            if (nomenclatorList.Count > 0)
                _popup.IsOpen = true;
            else
                _popup.IsOpen = false;
            OnPropertyChanged("ItemsSource");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}