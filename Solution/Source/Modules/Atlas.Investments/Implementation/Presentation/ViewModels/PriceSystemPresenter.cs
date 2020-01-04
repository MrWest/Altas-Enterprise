using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IPriceSystemPresenter" /> being the presenter view model used to decorated
    ///     and impersonate PriceSystem domain entities in the UI.
    /// </summary>
    public class PriceSystemPresenter: NomenclatorPresenterBase<IPriceSystem, IPriceSystemManagerApplicationService>,IView,
        IPriceSystemPresenter
        
       
    {
        
        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedPriceSystem; }
        }

        private IOverGroupViewModel _overGroups;
        public IOverGroupViewModel OverGroups
        {
            get
            {
                if (_overGroups == null)
                {
                    _overGroups = ServiceLocator.Current.GetInstance<IOverGroupViewModel>();
                    _overGroups.PriceSystem = this;
                    _overGroups.Load();
                    _overGroups.Raised += OnInteractionRequested;
                }
                return _overGroups;
            }
        }

    

        public IList<IOverGroupPresenter> Items { get { return OverGroups.Items; } }
        public object DataContext { get; set; }
        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }

      

        public ICommand FilterCommand { get { return OverGroups.FilterCommand; } }

        public object View { get; set; }

        public object SecondView { get; set; }

        public string FilterCriteria { get; set; }
        public bool Export(IDatabaseContext databaseContext)
        {
            if (databaseContext == null)
                return false;
            return CreateServices().Export(databaseContext, Object);
        }

        public bool IsActive
        {
            get { return Object.IsActive; }
            set
            {
                SetProperty(v => Object.IsActive = v, value);
                OnPropertyChanged(() => IsActive);
             
            }
        }

        public void SpreadChanges(IBudgetComponentItem toSpread)
        {

            foreach (IOverGroupPresenter overGroupPresenter in OverGroups.Items)
            {
                overGroupPresenter.SpreadChanges(toSpread);
            }
        }

        public bool ExistOverGroup(string code)
        {

          return  CreateServices().ExistOverGroup(code, Object);

        }

        public void AddFromScratch(string code, string name)
        {
            CreateServices().AddFromScratch(code, name, Object);
        }

        public IOverGroupPresenter GetOverGroup(string code)
        {
          var overGroup = CreateServices().GetOverGroup(code, Object);
          var overGroups = ServiceLocator.Current.GetInstance<IOverGroupViewModel>();
            overGroups.PriceSystem = this;
            return overGroups.CreatePresenter(overGroup);
        }
    }
}
