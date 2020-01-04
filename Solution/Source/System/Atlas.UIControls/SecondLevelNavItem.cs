using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    public class SecondLevelNavItem : ListBoxItem, INotifyPropertyChanged
    {
        ///// <summary>
        ///// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        ///// </summary>
        //public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(SecondLevelNavItem), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(SecondLevelNavItem), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the <see cref="string"/> value representing the text displayed by instances of
        /// <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SecondLevelNavItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Dependency property containing the <see cref="string"/> value representing the text displayed by instances of
        /// <see cref="SecondLevelNavItem"/>.
        /// </summary>
        //public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(SecondLevelNavItem), new PropertyMetadata(false,OnSelectChange));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        //public static readonly DependencyProperty DoPaginateProperty = DependencyProperty.Register("DoPaginate", typeof(bool), typeof(SecondLevelNavItem), new PropertyMetadata(true, OnDoPaginateChanged));


        /// <summary>
        /// Dependency property containing the <see cref="string"/> value representing the text displayed by instances of
        /// <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty OptionalUriProperty = DependencyProperty.Register("OptionalUri", typeof(string), typeof(SecondLevelNavItem), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property containing the <see cref="string"/> value representing the text displayed by instances of
        /// <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty DefaultOptionalUriProperty = DependencyProperty.Register("DefaultOptionalUri", typeof(string), typeof(SecondLevelNavItem), new PropertyMetadata(null));

        public readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();

        public event PropertyChangedEventHandler PropertyChanged;
        ///// <summary>
        ///// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        ///// </summary>
        public static readonly DependencyProperty CanProperty = DependencyProperty.Register("Can", typeof(bool), typeof(SecondLevelNavItem), new PropertyMetadata(false));

        ////private static void OnSelectChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        ////{
        ////    if (e.NewValue != null)
        ////    {
        ////        var doit = (bool)e.NewValue;

        ////      ((SecondLevelNavItem)d).ReloadOptionalContent(doit);
        ////    }
        ////    //var damnit = shit.FindName("TextBlock");
        ////    // throw new System.NotImplementedException();

        ////}


        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            ReloadOptionalContent(IsSelected);
        }
        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            ReloadOptionalContent(IsSelected);
        }

        public void ReloadOptionalContent(bool choice)
        {
            if (OptionalUri != null && choice)
            {
                if(OptionalUri.ToString()!=_navigationServices.OptionalUri)
                {
                    _navigationServices.HideOptionalNavigationContent();

                    _navigationServices.SetupOptionalNavigation(OptionalUri, control => ((AtlasOptionalContent)control).ElementsTreeView);

                    _navigationServices.ShowOptionalNavigationContent();
                }
              
            }
            if (DefaultOptionalUri != null && !choice)
            {
                if (DefaultOptionalUri.ToString() != _navigationServices.OptionalUri)
                {
                    _navigationServices.HideOptionalNavigationContent();

                    _navigationServices.SetupOptionalNavigation(DefaultOptionalUri, control => ((AtlasOptionalContent)control).ElementsTreeView);

                    _navigationServices.ShowOptionalNavigationContent();
                }

            }
        }
        ///// <summary>
        ///// Gets or sets the command that triggers the filtering process.
        ///// </summary>
        //public bool DoPaginate
        //{
        //    get { return (Boolean)GetValue(DoPaginateProperty); }
        //    set { SetValue(DoPaginateProperty, value); }
        //}

        ///// <summary>
        ///// Gets of sets whether the root navigation bar is collapsed or not.
        ///// </summary>
        public bool Can
        {
            get { return (bool)GetValue(CanProperty); }
            set { SetValue(CanProperty, value); }
        }


        public string OptionalUri {
            get { return (string)GetValue(OptionalUriProperty); }
            set { SetValue(OptionalUriProperty, value); }
        }


        public string DefaultOptionalUri
        {
            get { return (string)GetValue(DefaultOptionalUriProperty); }
            set { SetValue(DefaultOptionalUriProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public SecondLevelNavItem()
        {
            DefaultStyleKey = typeof(SecondLevelNavItem);
            
        }

        //protected override void OnSelected(RoutedEventArgs e)
        //{
        //    base.OnSelected(e);
        //    if (OptionalUri != null)
        //    {
        //        _navigationServices.HideOptionalNavigationContent();
             
        //        _navigationServices.SetupOptionalNavigation(OptionalUri, control => ((AtlasOptionalContent)control).ElementsTreeView);

        //        _navigationServices.ShowOptionalNavigationContent();
        //    }
        //}

        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public object Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the object representing the icon to display by the current <see cref="SecondLevelNavItem"/>.
        ///// </summary>
        //public object Content
        //{
        //    get { return GetValue(ContentProperty); }
        //    set { SetValue(ContentProperty, value); }
        //}
        /// <summary>
        /// Gets or sets the text to be displayed by the current <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }


        /// <summary>
        /// Gets or sets the text to be displayed by the current <see cref="SecondLevelNavItem"/>.
        /// </summary>
        //public bool IsSelected
        //{
        //    get { return (bool)GetValue(IsSelectedProperty); }
        //    set { SetValue(IsSelectedProperty, value); }
        //}

    }
}
