using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.Prism.Commands;
using ListView = System.Windows.Forms.ListView;

namespace CompanyName.Atlas.UIControls
{
    public class Paginator: ContentControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes CurrentCellChanged="AtlasCashFlowDataGridEntries_OnCurrentCellChanged" 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private  void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public Paginator()
        {

            ActualPage = 1;
           // _itemsPerPages=14;
           // DoPaginate = true;
            DataContextChanged+=OnDataContextChanged;

            IsVisibleChanged+=OnIsVisibleChanged;

        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!IsVisible)
            {
                PaginationFilter = (name => true);
                PaginationItems.Filter = (x => true);
            }
               

        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ////// DoPaginate = true;
            ////if (dependencyPropertyChangedEventArgs.NewValue != null &&
            ////    dependencyPropertyChangedEventArgs.NewValue.GetType().Implements<ICrudViewModel>())
            ////{
            ////    if (PaginationItems != null)
            ////        PaginationItems.Refresh();
            ////    Paginate(((ICrudViewModel)dependencyPropertyChangedEventArgs.NewValue).Items);
            //if (PaginationItems != null)
            //{
            //    PaginationItems.CurrentChanged -= PaginationItemsOnCurrentChanged;
            //    PaginationItems.CurrentChanged += PaginationItemsOnCurrentChanged;
            //    PaginationItems.Refresh();
            //}
            ActualPage = 1;
           // _itemsPerPages = 14;
            Paginate();

            ICommand command = new DelegateCommand(ExecuteMethod,CanExecuteMethod);

            
            //   //}
            


               //OnPropertyChanged("TotalPages");
               //OnPropertyChanged("ActualPage");
            //ActualPage = 1;
            //_itemsPerPages = 14;
        }

        private bool CanExecuteMethod()
        {
            throw new NotImplementedException();
        }

        private void ExecuteMethod()
        {
            

        }

        private void PaginationItemsOnCurrentChanged(object sender, EventArgs eventArgs)
        {
            PaginationItems.Filter = (name => Equals(name, name));
        }

        //private void PaginationItemsOnCurrentChanged(object sender, EventArgs eventArgs)
        //{
        //    Paginate();
        //}


        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public static readonly DependencyProperty PaginationItemsProperty = DependencyProperty.Register("PaginationItems", typeof(ItemCollection), typeof(Paginator), new PropertyMetadata(OnItemsCollectionsChanged));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Next Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty MoveNextCommandProperty = DependencyProperty.Register("MoveNextCommand", typeof(ICommand), typeof(Paginator), new PropertyMetadata(null));


        /// <summary>
        /// Dependency property to contain the command that is executed by the Previous Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty MovePreviousCommandProperty = DependencyProperty.Register("MovePreviousCommand", typeof(ICommand), typeof(Paginator), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Previous Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ActualPageProperty = DependencyProperty.Register("ActualPage", typeof(int), typeof(Paginator), new PropertyMetadata(1,OnDoPaginateChanged));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Previous Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty TotalPagesProperty = DependencyProperty.Register(" TotalPages", typeof(Int32), typeof(Paginator), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty PageCommandProperty = DependencyProperty.Register("PageCommand", typeof(ICommand), typeof(Paginator));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty DoPaginateProperty = DependencyProperty.Register("DoPaginate", typeof(bool), typeof(Paginator), new PropertyMetadata(true,OnDoPaginateChanged));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty OnePageStyleProperty = DependencyProperty.Register("OnePageStyle", typeof(bool), typeof(Paginator), new PropertyMetadata(false, OnePageStyleChanged));

       

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty ItemsPerPagesProperty = DependencyProperty.Register("ItemsPerPages", typeof(int), typeof(Paginator), new PropertyMetadata(14));


      
        private  ItemCollection _auxPaginationItems;

        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public  ItemCollection PaginationItems
        {
            get
            {
              
                //if (_paginationItems != null && _itemsPerPages < 1)
                //    _itemsPerPages = _paginationItems.Count;
                return (ItemCollection)GetValue(PaginationItemsProperty);
            }
            set
            {
               
                SetValue(PaginationItemsProperty, value);
                OnPropertyChanged("PaginationItems");
                Paginate();

                OnPropertyChanged("TotalPages");
                OnPropertyChanged("ActualPage");
              
            }
        }
        
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool DoPaginate
        {
            get { return (Boolean)GetValue(DoPaginateProperty); }
            set { SetValue(DoPaginateProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool OnePageStyle
        {
            get { return (bool)GetValue(OnePageStyleProperty); }
            set { SetValue(OnePageStyleProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand PageCommand
        {
            get { return (ICommand)GetValue(PageCommandProperty); }
            set { SetValue(PageCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that is executed by the Next Button of the current <see cref="Paginator"/>.
        /// </summary>
        public ICommand MoveNextCommand
        {
            get { return (ICommand)GetValue(MoveNextCommandProperty); }
            set { SetValue(MoveNextCommandProperty, value); }
        }

        private void MoveNextCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (ActualPage < TotalPages);
        }

        private void MoveNextCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MoveNextCommand != null)
            {
              
                ActualPage  = (ActualPage + 1 > TotalPages ? TotalPages : ActualPage + 1);
               
            }
               

           
        }
        private void PageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (true);
        }

        private void PageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (PageCommand != null)
            {

                Paginate();
            }



        }
        /// <summary>
        /// Gets or sets the command that is executed by the Next Button of the current <see cref="Paginator"/>.
        /// </summary>
        public ICommand MovePreviousCommand
        {
            get { return (ICommand)GetValue(MovePreviousCommandProperty); }
            set { SetValue(MovePreviousCommandProperty, value); }
        }

        private void MovePreviousCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (ActualPage>1);
        }

        private void MovePreviousCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MovePreviousCommand != null)
            {
               
                ActualPage = (ActualPage - 1 < 1 ? 1 : ActualPage - 1);
              
            }
               

           
        }
        /// <summary>
        /// Executed when the template was applied to the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            MovePreviousCommand = new RoutedCommand("MovePrevious", typeof(Paginator));
            MoveNextCommand = new RoutedCommand("MoveNext", typeof(Paginator));
            PageCommand = PageCommand ?? new RoutedCommand("Paginate", typeof (Paginator));
            var commandBinding = new CommandBinding(MovePreviousCommand, MovePreviousCommand_Executed, MovePreviousCommand_CanExecute);
            var commandBinding2 = new CommandBinding(MoveNextCommand, MoveNextCommand_Executed, MoveNextCommand_CanExecute);
            var commandBinding3 = new CommandBinding(PageCommand, PageCommand_Executed, PageCommand_CanExecute);
            CommandBindings.Add(commandBinding);
            CommandBindings.Add(commandBinding2);
            CommandBindings.Add(commandBinding3);
            PageCommand.Execute(this.GetType().GetMethod("Paginate"));
           

        }
       

        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
      //  public static readonly DependencyProperty PaginationCountProperty = DependencyProperty.Register("PaginationCount", typeof(Int32), typeof(Paginator), new PropertyMetadata(OnItemsCollectionsChanged));

        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
       // public static readonly DependencyProperty PaginationControlDependencyProperty = DependencyProperty.Register("PaginationControl", typeof(Selector), typeof(Paginator), new PropertyMetadata(OnItemsCollectionsChanged2));


        private static void OnDoPaginateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
           // while ((bool) e.NewValue && ((Paginator) o).ItemsPerPages > 1)
            //{
                ((Paginator)o).UpdateSelection(e);
             //   ((Paginator) o).ItemsPerPages--;

//            }

            

        }

        private static void OnePageStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Paginator)d).DoOnePageStyle((bool)e.NewValue);
        }
        private static void OnItemsCollectionsChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

            ((Paginator)o).UpdateSelection(e);
           
        }


     

        public int ItemsPerPages
        {
            get { return (int)GetValue(ItemsPerPagesProperty); }
            set { SetValue(ItemsPerPagesProperty, value); }
        }
     
        private void UpdateSelection(DependencyPropertyChangedEventArgs e)
        {

            // if (DoPaginate)
           
            Paginate();
         
        }

        
        private void DoOnePageStyle(bool option)
        {
            ActualPage = 1;
            Paginate();
            if (option)
            {
                
                PaginationFilter = (name => true);
            }
           
        }
        private Predicate<object> _paginationFilter;
        public Predicate<object> PaginationFilter
        {
            get { return _paginationFilter; }
            set
            {
                _paginationFilter = value;
                try
                {
                    if (PaginationItems != null)
                    {
                        ((ItemCollection) PaginationItems).Filter = _paginationFilter;
                            if (((ItemCollection)PaginationItems).NeedsRefresh)
                              ((ItemCollection)PaginationItems).Refresh();
                    }

                }
                catch (Exception e)
                {

                }

            }
        }

        private int _actualPage;
      
        public int ActualPage
        {
            get { return (int)GetValue(ActualPageProperty); }
            set
            {
                if (((int) value) < 1)
                    ActualPage = 1;
                else if (((int) value) > TotalPages)
                    ActualPage = TotalPages;
                else
                    SetValue(ActualPageProperty, value);

                OnPropertyChanged("ActualPage");

            }
        }

      //  private int _itemsPerPages;
        public int TotalPages
        {
            get
            {
                if (OnePageStyle)
                    return 1;
                if (Elements == 0)
                    return 1;
                int result = (Elements % ItemsPerPages == 0)
                    ? (int)Elements / ItemsPerPages
                    : (int)Math.Round(((double)Elements / (double)ItemsPerPages) + 0.5, 0);

                SetValue(TotalPagesProperty, result == 0 ? 1 : result);
               
                return (int) GetValue(TotalPagesProperty);
            }

        }

        public int Elements
        {
            get
            {
                
                if (PaginationItems == null)
                    return 0;

                int elements = 0;

                if (PaginationItems.SourceCollection == null)
                    return elements;
                IEnumerator enumerator = PaginationItems.SourceCollection.GetEnumerator();
                while (enumerator.MoveNext())
                    elements++;

                enumerator = PaginationItems.SourceCollection.GetEnumerator();

                if (elements > PaginationItems.Count)
                {
                    while (enumerator.MoveNext())
                    {
                        if (!PaginationItems.Contains(enumerator.Current) && !PaginationItems.IsInUse)
                            PaginationItems.Add(enumerator.Current);
                    }
                       
                }
                //enumerator = PaginationItems.SourceCollection.GetEnumerator();

                //while (elements > PaginationItems.Count)
                //{
                //    PaginationItems.Refresh();
                //}
                //if (elements > PaginationItems.Count)
                //{
                //   // PaginationItems.DeferRefresh();
                //    PaginationItems.Refresh();
                //    while (enumerator.MoveNext())
                //    {
                //        if ( !_auxPaginationItems.IsInUse)
                //            _auxPaginationItems.Add(enumerator.Current);
                //    }

                //}


                return elements ;
            }
            
        }
        public String Tooltip
        {
            get { return "Pagina: " + ActualPage + " de " + TotalPages; }

        }


        private void Paginate()
        {
            try
            {
                PaginationFilter = (name => true);
                if (PaginationItems != null)
                {
                    PaginationItems.Filter = (x => true);
                    OnPropertyChanged("PaginationItems");
                }


                if (PaginationItems != null && Elements > 0 && PageCommand != null)
                {

                    ArrayList showList = new ArrayList();
                    int _elements = Elements;
                    if (TotalPages < ActualPage)
                        ActualPage = TotalPages;
                    for (int w = ((ActualPage - 1) * ItemsPerPages); w < (((ActualPage - 1) * ItemsPerPages) + ItemsPerPages); w++)
                    {
                        //int index = ((ActualPage - 1) * _itemsPerPages + w);


                        if (w >= 0 && w < _elements)
                        {
                            if (_elements <= PaginationItems.Count)
                                showList.Add(PaginationItems[w]);
                            else
                            {
                                var enumerator = PaginationItems.SourceCollection.GetEnumerator();

                                int count = 0;
                                while (enumerator.MoveNext() && count < w)
                                    count++;

                                showList.Add(enumerator.Current);



                            }
                        }



                        else
                        {
                            if (w >= 0 && w < _elements)
                                showList.Add(PaginationItems.SourceCollection);
                        }

                    }


                    PaginationFilter = (showList.Contains);


                    //  PageCommand.Execute(PaginationFilter.ToString());
                }

                OnPropertyChanged("TotalPages");
                OnPropertyChanged("ActualPage");
            }
            catch (Exception e)
            {
               
            }
           
        }

       

       
    }
}
