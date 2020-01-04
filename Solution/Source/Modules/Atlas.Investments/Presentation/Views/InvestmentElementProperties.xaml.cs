using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.Views.Arrangement;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for InvestmentElementProperties.xaml
    /// </summary>
    public partial class InvestmentElementProperties
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementProperties" /> view.
        /// </summary>
        public InvestmentElementProperties()
        {
            InitializeComponent();
            IsVisibleChanged+=OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (DataContext != null)
            {
                var invElement = (IInvestmentElementPresenter) DataContext;
                invElement.SecondView = BudgetViewType.All;
            }
        }


        /// <summary>
        ///     Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        ///     between this view and the new datacontext are wired up. Also makes sure that the content loaded in the current
        ///     control is in correspondance with the data context type.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            base.OnDataContextChanged(sender, e);

            bool makeContentBinding = true;
            object dataContext = e.NewValue;

            if (dataContext is IInvestmentPresenter)
                ContentPlace.ContentTemplate = (DataTemplate)FindResource("InvestmentDataTemplate");
            else if (dataContext is IInvestmentComponentPresenter<IInvestmentComponent> || dataContext is IInvestmentComponentPresenter<IInvestment>)
                ContentPlace.ContentTemplate = (DataTemplate)FindResource("InvestmentComponentDataTemplate");
            else
                makeContentBinding = false;

            if (!makeContentBinding) return;

            var contentBinding = new Binding { Source = dataContext };
            ContentPlace.SetBinding(ContentProperty, contentBinding);
        }

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null&&DataContext!=null)
            {
                var sample = ((ComboBox)sender).SelectedItem;

                var current = DataContext;
                if (current != null)
                {
                    foreach (PropertyInfo prop in current.GetType().GetProperties())
                    {
                        if (sample.GetType().Implements(prop.PropertyType))
                        {
                            var aux = ((IEntity)current.GetType().GetProperty(prop.Name).GetValue(current));
                            if (aux == null)
                                current.GetType().GetProperty(prop.Name).SetValue(current, sample);
                            if (aux != null && ((IEntity)sample).Id != aux.Id)
                                current.GetType().GetProperty(prop.Name).SetValue(current, sample);

                        }

                    }
                }

            }
        }

        private void DocumentsDataGrid_OnDrop(object sender, DragEventArgs e)
        {
            if (((AtlasDataGrid) sender).DataContext != null)
            {
                string[] formats = e.Data.GetFormats();
                String file = ((string[])e.Data.GetData(formats[7]))[0].ToString();

                var documentsVM = ((AtlasDataGrid) sender).DataContext as IInvestmentDocumentViewModel;
                var newDocument = ServiceLocator.Current.GetInstance<IInvestmentDocument>();
                FileInfo fi = new FileInfo(file);
                newDocument.Name = fi.Name.Replace(fi.Extension,"");
                newDocument.FilePath = file;
                newDocument.RecieveDate = fi.LastWriteTime;

                var presenter = ServiceLocator.Current.GetInstance<IInvestmentDocumentPresenter>();
                presenter.Object = newDocument;
                documentsVM.AddCommand.Execute(presenter);
             //   documentsVM.Load();
            }
           
           
        }

        private void ContentPlace_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                bool makeContentBinding = true;
                object dataContext = this.DataContext;

                ContentPlace.ContentTemplate = null;
                if (dataContext is IInvestmentPresenter)
                    ContentPlace.ContentTemplate = (DataTemplate)FindResource("InvestmentDataTemplate");
                else if (dataContext is IInvestmentComponentPresenter<IInvestmentComponent> || dataContext is IInvestmentComponentPresenter<IInvestment>)
                    ContentPlace.ContentTemplate = (DataTemplate)FindResource("InvestmentComponentDataTemplate");
                else
                    makeContentBinding = false;

                if (!makeContentBinding) return;

                var contentBinding = new Binding { Source = dataContext };
                ContentPlace.SetBinding(ContentProperty, contentBinding);
            }
           
        }

        private void InvestmentType_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext != null)
            {
                var presenter = (((FrameworkElement)sender).DataContext as IInvestmentPresenter);

                if (presenter != null)
                {
                    ((AtlasCacheTextBox)sender).CacheSource =
                        presenter.FindInvestmentTypeByContains(((AtlasCacheTextBox)sender).Text).ToArray();

                }
            }
        }

        private void Nature_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext != null)
            {
                var presenter = (((FrameworkElement)sender).DataContext as IInvestmentPresenter);

                if (presenter != null)
                {
                    ((AtlasCacheTextBox)sender).CacheSource =
                        presenter.FindNatureByContains(((AtlasCacheTextBox)sender).Text).ToArray();

                }
            }
        }

        private void Impact_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext != null)
            {
                var presenter = (((FrameworkElement)sender).DataContext as IInvestmentPresenter);

                if (presenter != null)
                {
                    ((AtlasCacheTextBox)sender).CacheSource =
                        presenter.FindImpactByContains(((AtlasCacheTextBox)sender).Text).ToArray();

                }
            }
        }
    }
}