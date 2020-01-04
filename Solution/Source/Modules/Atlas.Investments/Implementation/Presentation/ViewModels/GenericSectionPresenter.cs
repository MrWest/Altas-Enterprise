using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// Implements what its interface says
    /// </summary>
    public abstract class GenericSectionPresenter<TItem, TService> : EntityPresenterBase<TItem, TService>, IGenericSectionPresenter, IView
        where TItem:class, IPriceSystem
        where TService:class , IItemManagerApplicationServices<TItem>
    {
        private ISectionViewModel _sectionViewModel;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [StringLengthValidator(1, 100, MessageTemplateResourceType = typeof(Resources), MessageTemplateResourceName = "InvestmentElementMustHaveAName")]
        public virtual  string Name
        {
            get { return Object.Name; }
            set
            {
                SetProperty(v => Object.Name = v, value); 
                OnPropertyChanged(() => Name);
            }
        }
        /// <summary>
        ///     Gets or sets the Description of the <see cref="IPriceSystem" />.
        /// </summary>
        public string Description
        {
            get { return Object.Description; }
            set { SetProperty(v => Object.Description = v, value); }
        }
        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public ISectionViewModel Sections
        {
            get { return GetOrInitialize(ref _sectionViewModel); }
        }

        private ISectionViewModel GetOrInitialize(ref ISectionViewModel viewModel)//, Action<ISectionViewModel> initialize
        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<ISectionViewModel>();
                //  initialize(viewModel);
                viewModel.AboveSection = this;
                viewModel.Load();

                viewModel.Raised += OnInteractionRequested;

            }

            return viewModel;
        }

        /// <summary>
        /// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        /// between this view and the new datacontext are wired up.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetupInteractionWithDataContext(e, OnInteractionRequested);
        }

        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }
        public object DataContext { get; set; }

       
    }
}
