using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Interaction logic for Paginator.xaml
    /// </summary>
    public  class Paginator : DataGrid, INotifyPropertyChanged
    {
        
        public Paginator()
        {   
           
            _elements = 15;
            _actualPage = 1;
            InitializeComponent();
       
          //  PaginationItems.CurrentChanged+=PaginationItemsOnCurrentChanged;
          //  DataGrid dg = new DataGrid();

          //  dg = (DataGrid) PaginationItems;

           
          // Paginate();
        }

    
        private void PaginationItemsOnCurrentChanged(object sender, EventArgs eventArgs)
        {
            Paginate();
        }


        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public readonly DependencyProperty PaginationItemsProperty = DependencyProperty.Register("PaginationItems", typeof(IEnumerable), typeof(Paginator), new PropertyMetadata(OnItemsCollectionsChanged));


        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the total summary.
        /// </summary>
        public static readonly DependencyProperty TotalTextProperty = DependencyProperty.Register("TotalText", typeof(string), typeof(AtlasDataGrid), new PropertyMetadata(Properties.Resources.Total));

       private  ItemCollection _paginationItems;

        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public  IEnumerable PaginationItems
        {
            get
            {
                //_paginationItems = (ItemCollection) PaginationItems;
                //if (_paginationItems != null && _realitemsCount < 1)
                //_realitemsCount = _paginationItems.Count;
                //return _paginationItems;
                if (_paginationItems != null && _realitemsCount < 1)
                    _realitemsCount = _paginationItems.Count;
                return (IEnumerable)GetValue(PaginationItemsProperty); 
            }
            set
            {
                //_paginationItems = value;
                SetValue(PaginationItemsProperty, value);
                OnPropertyChanged("PaginationItems");
            }
        }
        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public string TotalText
        {
            get { return (string)GetValue(TotalTextProperty); }
            set { SetValue(TotalTextProperty, value); }
        }
        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public  readonly DependencyProperty PaginationCountProperty = DependencyProperty.Register("PaginationCount", typeof(Int32), typeof(Paginator), new PropertyMetadata(OnItemsCollectionsChanged));
     
       /// <summary>
       /// Identifies the SelectedSource dependency property.
       /// </summary>
        public readonly DependencyProperty PaginationControlDependencyProperty = DependencyProperty.Register("PaginationControl", typeof(Selector), typeof(Paginator), new PropertyMetadata(OnItemsCollectionsChanged2));

      //  public Selector PaginatedControl { get; set; }
        //private static int _yata;

        //private static int restless
        //{
        //    get
        //    {
        //        return _yata;
        //    }
        //    set
        //    {
        //        _yata = value;
        //        Paginator.pa
        //    }
        //}

        private static  void OnItemsCollectionsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
          
            ((Paginator)o).UpdateSelection(e);
          //  TextBox.Text = e.NewValue.ToString();
            //((DateTimePicker)o).UpdateSelection(e);
        }
        private static void OnItemsCollectionsChanged2(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

            ((Paginator)o).UpdateSelection(e);
            //  TextBox.Text = e.NewValue.ToString();
            //((DateTimePicker)o).UpdateSelection(e);
        }
        private void UpdateSelection(DependencyPropertyChangedEventArgs e)
        {
            Paginate();
            //((Calendar) DateComboBox.SelectedItem).SelectedDate = (DateTime) e.NewValue;
        }
        //  public static ItemCollection InnerPaginationItems { get; set; }

   //     public static DependencyProperty PaginationFilter = DependencyProperty.Register("PaginationFilter", typeof(ItemCollection), typeof(Paginator), new PropertyMetadata(InnerPaginationFilter));

        private  Predicate<object> _paginationFilter;
        public  Predicate<object> PaginationFilter
        {
            get { return _paginationFilter; }
            set
            {
                _paginationFilter = value;
                try
                {
                    if (PaginationItems != null)
                        ((ItemCollection) PaginationItems).Filter = _paginationFilter;
                }
                catch (Exception e)
                {
                    
                }
              
            }
        }

        private int _actualPage;
        private int _elements ;
        public int ActualPage
        {
            get { return _actualPage; }
            set
            {
                if (((int) value) < 1)
                    _actualPage = 1;
                else if(((int)value)>TotalPages)
                    _actualPage = TotalPages;
                else
                _actualPage = value;

              //  Paginate();
              
            } 
        }

        private  int _realitemsCount;
        public int TotalPages
        {
            get
            {

                int result = (_realitemsCount%Elements == 0)
                    ? (int) _realitemsCount/Elements
                    : (int) Math.Round(((double) _realitemsCount/(double) Elements) + 0.5, 0);
               
                return result==0?1:result;
            }
            
        }

        public int Elements
        {
            get { return _elements; }
            set
            {
                if (((int)value) < 5)
                    _elements = 5;
                else if (((int)value) > 25)
                    _elements = 25;
                else
                    _elements = value;
              //  Paginate();
            }
        }
        public String Tooltip
        {
            get { return "Pagina: " + ActualPage + " de " + TotalPages; }
            
        }

        private void Previus_OnClick(object sender, RoutedEventArgs e)
        {
            //PaginationItems = (ItemCollection)PaginationItems;
            if (PaginationItems != null)
                PaginationFilter = ((ItemCollection)PaginationItems).Filter;

            APage.Text = Convert.ToString(ActualPage - 1 < 1 ? 1 : ActualPage - 1);
           // Paginate();
        }

        private void Paginate()
        {
            PaginationFilter = (name=>name==name);
            
            if (PaginationItems != null&&(_realitemsCount= PaginationItems.Count)>0 )
            {
                
                ArrayList showList = new ArrayList();
                for (int w = 0; w < Elements; w++)
                {
                    int index = ((ActualPage - 1) * Elements + w);
                    if(index>=0&&index<PaginationItems.Count)
                    showList.Add(PaginationItems[index]);
                }

                PaginationFilter = (showList.Contains);
            }
        }

        private void Next_OnClick(object sender, RoutedEventArgs e)
        {
           // PaginationItems = (ItemCollection)PaginationItems;
            if (PaginationItems != null)
                PaginationFilter = ((ItemCollection)PaginationItems).Filter;

            APage.Text = Convert.ToString(ActualPage + 1 > TotalPages ? TotalPages : ActualPage + 1);
            // Paginate();
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

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (NElements != null)
            {
                int rslt;
                if (Int32.TryParse(NElements.Text, out rslt))
                {
                    Elements = Convert.ToInt32(NElements.Text);
                    Paginate();
                }

                else
                    NElements.Text = Convert.ToString(Elements);
            }
           
            
           
        }

        private void TextBoxBase_OnTextChanged2(object sender, TextChangedEventArgs e)
        {
            int rslt;
            if (Int32.TryParse(APage.Text, out rslt))
            {
                 ActualPage = Convert.ToInt32(APage.Text); 
                 Paginate();
            }
            else
            {
                APage.Text = Convert.ToString(ActualPage);  
            }
           
        }

        private void Paginator_OnLoaded(object sender, RoutedEventArgs e)
        {

            if (PaginationItems!=null)
           ((ItemCollection)PaginationItems).CurrentChanged += PaginationItemsOnCurrentChanged;

            DoPaginate += Paginator_Paginated;
          //  PaginationItems = (ItemCollection)PaginationItems;
           // PaginationItems.CurrentChanged += PaginationItemsOnCurrentChanged;
        }

        private void TextBoxBase_OnTextChanged4(object sender, TextChangedEventArgs e)
        {
           // if(PaginationItems.Count>15)
            Paginate();
        }

        private void Paginator_Paginated(object sender, SelectionChangedEventArgs e)
        {
          

        }

        public event EventHandler<SelectionChangedEventArgs> DoPaginate;


        private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ActualPage > 1;
        }

        private void Next_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ActualPage != TotalPages;
        }
    }
}
