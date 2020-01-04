using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.UIControls.Annotations;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasTabControl:TabControl, IFiltrable,INotifyPropertyChanged
    {
        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(AtlasTabControl), new PropertyMetadata(null, PropertyChangedCallback2));

        private static void PropertyChangedCallback2(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var tabControl = (AtlasTabControl)dependencyObject;

         //   tabControl.FilterBox.FilterCommand = (ICommand)dependencyPropertyChangedEventArgs.NewValue;
            //var filterBox = (FilterBox)tabControl.Template.FindName("FilterBox", tabControl);
            //filterBox.FilterCommand = (ICommand)dependencyPropertyChangedEventArgs.NewValue;
        }

        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="PlanningExecutionView"/>.
        /// </summary>
        public static readonly DependencyProperty FiltrableObjectProperty = DependencyProperty.Register("FiltrableObject", typeof(IFiltrable), typeof(AtlasTabControl), new PropertyMetadata(null));


        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="PlanningExecutionView"/>.
        /// </summary>
        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register("View", typeof(object), typeof(AtlasTabControl), new PropertyMetadata(null,PropertyChangedCallback));

        public static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var tabControl = (AtlasTabControl) dependencyObject;
          //  tabControl.SelectedItem = tabControl.Items.Cast<AtlasTabItem>().Single(x => x.View == tabControl.View);
        }

        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="PlanningExecutionView"/>.
        /// </summary>
        public static readonly DependencyProperty SecondViewProperty = DependencyProperty.Register("SecondView", typeof(object), typeof(AtlasTabControl), new PropertyMetadata(null,SecondViewPropertyChangedCallback));

        private static void SecondViewPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var tabControl = (AtlasTabControl) dependencyObject;
            tabControl.InternalSignalText();
        }

        public async void InternalSignalText()
        {


            await AsyncSignalText().ContinueWith(Continues);

           
        }


        private async void Continues(Task task)
        {
            //if(AsyncSignalText().IsCompleted)
            OnPropertyChanged("SecondView");
        }
        private async Task AsyncSignalText()
        {

            var _statusBarServices = ServiceLocator.Current.GetInstance<IStatusBarServices>();
            _statusBarServices.ForceSignalLoading();




        }
        //private static void PropertyChangedCallback2(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        //{
        //    var tabControl = (AtlasTabControl) dependencyObject;
        //    if (tabControl.FiltrableObject != null)
        //        tabControl.FiltrableObject.SecondView = dependencyPropertyChangedEventArgs.NewValue;
        //}


        /// <summary>
        /// Dependency property containing the criteria to use in filtering the budget component items displayed in
        /// <see cref="PlanningExecutionView"/> instances.
        /// </summary>
        public static readonly DependencyProperty FilterCriteriaProperty = DependencyProperty.Register("FilterCriteria", typeof(string), typeof(AtlasTabControl), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property containing the criteria to use in filtering the budget component items displayed in
        /// <see cref="PlanningExecutionView"/> instances.
        /// </summary>
        public static readonly DependencyProperty SpecialTabItemProperty = DependencyProperty.Register("SpecialTabItem", typeof(object), typeof(AtlasTabControl), new PropertyMetadata(null));


        /// <summary>
        /// Dependency property containing the criteria to use in filtering the budget component items displayed in
        /// <see cref="PlanningExecutionView"/> instances.
        /// </summary>
        public static readonly DependencyProperty VariablesProperty = DependencyProperty.Register("Variables", typeof(bool), typeof(AtlasTabControl), new PropertyMetadata(false, TabitemPropertyChangedCallback));

        public int saveSelectedIndex;
        private static void TabitemPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var tabControl = (AtlasTabControl)dependencyObject;
            if (!Equals(dependencyPropertyChangedEventArgs.NewValue, null)  )
            {
                if (tabControl.Variables)
                {
                    tabControl.saveSelectedIndex = tabControl.SelectedIndex;
                    tabControl.SelectedIndex = -1;
                }
                else
                {
                    if(tabControl.SelectedIndex == -1)
                         tabControl.SelectedIndex = tabControl.saveSelectedIndex;
                }
                //  var specials = dependencyPropertyChangedEventArgs.NewValue as TabItem;
                //  tabControl.SpecialTabItem.MouseDown+= SpecialTabItemOnMouseDown;
                //  tabControl.SpecialTabItem.Tag = tabControl;
            }
        }

        private static void SpecialTabItemOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var specials = sender as TabItem;
            ((AtlasTabControl) specials.Tag).SetValue(SelectedContentProperty, specials.Content) ;//= specials.Content;
        }

        /// <summary>
        /// Dependency property containing the criteria to use in filtering the budget component items displayed in
        /// <see cref="PlanningExecutionView"/> instances.
        /// </summary>
        //public static readonly DependencyProperty FilterBoxProperty = DependencyProperty.Register("FilterBox", typeof(FilterBox), typeof(AtlasTabControl), new PropertyMetadata(null));


        public AtlasTabControl()
        {
            DefaultStyleKey = typeof(AtlasTabControl);
            SelectionChanged+=OnSelectionChanged;
            PropertyChanged += OnPropertyChanged;
            IsVisibleChanged+=OnIsVisibleChanged;
            DataContextChanged+=OnDataContextChanged;

        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
           if(Items.Count>0 && SelectedIndex==-1 && saveSelectedIndex != -1)
                   SelectedIndex = saveSelectedIndex;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (IsVisible)
            {
                if (Items.Count > 0 && SelectedIndex == -1)
                    SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public object SpecialTabItem
        {
            get
            {
               
                return GetValue(SpecialTabItemProperty);
            }
            set { SetValue(SpecialTabItemProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool Variables
        {
            get
            {
                return (bool)GetValue(VariablesProperty);
            }
            set { SetValue(VariablesProperty, value); }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (selectionChangedEventArgs.OriginalSource?.GetType() == typeof(AtlasTabControl))
            {
                
           

                if (SelectedItem != null && SelectedItem is AtlasTabItem && SecondView != ((AtlasTabItem)SelectedItem).View)
                {
                    FilterCommand.Execute("");
                    SecondView = ((AtlasTabItem)SelectedItem).View;
                

                    OnPropertyChanged("SecondView");
                }
                //if(FiltrableObject!=null && SelectedItem != null && FiltrableObject.SecondView != ((AtlasTabItem)SelectedItem).View)
                if(FiltrableObject!=null&& !Equals(SecondView , FiltrableObject.SecondView))
                    SecondView = FiltrableObject.SecondView  ;
                OnPropertyChanged("SelectedIndex");

                if (SelectedIndex > -1)
                    Variables = false;

                if (Items.Count > 0 && SelectedItem==null && SelectedIndex == -1 && selectionChangedEventArgs.RemovedItems.Count > 0
                   && selectionChangedEventArgs.RemovedItems[0] != null && !Variables && saveSelectedIndex!=-1)
                {
                    //Variables = false;
                    SelectedItem = Items[saveSelectedIndex];
                }

            }

        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand FilterCommand
        {
            get
            {
                //if (FiltrableObject != null)
                //    return FiltrableObject.FilterCommand;
                return (ICommand)GetValue(FilterCommandProperty);
            }
            set { SetValue(FilterCommandProperty, value); }
        }
        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public object View
        {
            get { return GetValue(ViewProperty); }
            set
            {
               
                //if (FiltrableObject != null)
                //    FiltrableObject.View = value;
                SetValue(ViewProperty, value);
            }
        }
        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public IFiltrable FiltrableObject
        {
            get { return (IFiltrable) GetValue(FiltrableObjectProperty); }
            set { SetValue(FiltrableObjectProperty, value); }
        }

        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public object SecondView
        {
            get
            {
                if (SelectedItem != null && SelectedItem is AtlasTabItem)
                {
                    return ((AtlasTabItem)SelectedItem).View;
                   
                }
                return GetValue(SecondViewProperty);
            }
            set
            {
                //if (FiltrableObject != null)
                //    FiltrableObject.SecondView = value;

                SetValue(SecondViewProperty, value);
                OnPropertyChanged("SecondView");
            }
        }
        /// <summary>
        /// Gets or sets the filtering criteria to use when filtering the budget component items by name.
        /// </summary>
        public string FilterCriteria
        {
            get { return (string)GetValue(FilterCriteriaProperty); }
            set { SetValue(FilterCriteriaProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}