using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// implements the incapsulation of a Section  instance for presentation 
    /// </summary>
    
        [Obsolete]
    public class SectionPresenter : GenericSectionPresenter<ISection,ISectionManagerApplicationService>, ISectionPresenter 
    {

        private IGenericSectionPresenter _aboveSection;
        
        //private IPlannedActivityViewModel<ISection> _plannedActivities;

        /// <summary>
        /// Gets or sets the parent investment element of the investment component presenters in the current crud view model.
        /// </summary>
        public IGenericSectionPresenter AboveSection
        {
            get
            {
                if (_aboveSection == null)
                    throw new InvalidOperationException(Resources.InitializeInvestmentComponentViewModelParentBeforeUsingIt);

                return _aboveSection;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _aboveSection = value;
            }
        }
       

        /// <summary>
        ///     Gets the shortened version of the current <see cref="ISectionPresenter" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 14 ? Name.Substring(0, 15) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value); 
                OnPropertyChanged(()=>Name);
                OnPropertyChanged(() => ShortName);
            }
        }

        public bool IsExpanded { get; set; }


       

        public  int Depth
        {
            get
            {
                //var parent = Parent.Value as IInvestmentElementPresenter;

                return 0;
            }
        }

        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="InvestmentElementPresenterBase{T,TServices}" /> according to its depth.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string IconData
        {
            get
            {
                switch (Depth)
                {
                    case 0:
                        return
                            "F1 M 19,29L 47,29L 47,57L 19,57L 19,29 Z M 43,33L 23,33.0001L 23,53L 43,53L 43,33 Z M 39,41L 39,45L 27,45L 27,41L 39,41 Z M 24,24L 51.9999,24.0001L 51.9999,52L 48.9999,52.0001L 48.9999,27.0001L 24,27.0001L 24,24 Z M 54,47L 53.9999,22.0001L 29,22L 29,19L 57,19L 57,47L 54,47 Z ";
                    default:
                        return "F1 M 19,29L 47,29L 47,57L 19,57L 19,29 Z M 43,33L 23,33.0001L 23,53L 43,53L 43,33 Z M 39,41L 39,45L 35,45L 35,49L 31,49L 31,45L 27,45L 27,41L 31,41L 31,37L 35,37L 35,41L 39,41 Z M 24,24L 51.9999,24.0001L 51.9999,52L 48.9999,52.0001L 48.9999,27.0001L 24,27.0001L 24,24 Z M 53.9999,47L 53.9999,22.0001L 29,22L 29,19L 56.9999,19.0001L 57,47L 53.9999,47 Z ";
                }
            }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [StringLengthValidator(1, 100, MessageTemplateResourceType = typeof(Resources), MessageTemplateResourceName = "InvestmentElementMustHaveAName")]
        public override  string Name
        {
            get { return Object.Name; }
            set
            {
               SetProperty(v => Object.Name = v, value); 
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
            }
        }
        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedPriceSystem; }
        }

      

        /// <summary>
        ///     Gets or sets the crud view model handling the investment components presenters being children of the current investment element presenter.
        /// </summary>
        //public IPlannedActivityViewModel<ISection> PlannedActivities
        //{
        //    get
        //    {
                
        //        if (_plannedActivities == null)
        //        {
        //            _plannedActivities = ServiceLocator.Current.GetInstance<IPlannedActivityViewModel<ISection>>();
        //            _plannedActivities.Component = this;
        //            _plannedActivities.Load();
        //            _plannedActivities.Raised += OnInteractionRequested;
        //        }

        //        return _plannedActivities;
        //    }
        //}

        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override ISectionManagerApplicationService CreateServices()
        {
            ISectionManagerApplicationService services = base.CreateServices();
            services.AboveSection = (IPriceSystem) AboveSection.Object;

            return services;
        }

        decimal IBudgetSummary.PlannedCost { get { return 0; }  }
        decimal IBudgetComponentPresenter<ISection>.ExecutedCost {
            get { return 0; }
        }
        public void NotifyUp()
        {
           // throw new NotImplementedException();
        }

        decimal IBudgetComponentPresenter<ISection>.PlannedCost {
            get { return 0; }
        }
        decimal IBudgetSummary.ExecutedCost {
            get { return 0; }
        }
        public decimal ExecutionPercent { get; private set; }
        public decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
        {
            throw new NotImplementedException();
        }

        public ICommand FilterCommand { get; }

        public object View { get; set; }

        public string FilterCriteria { get; set; }
        public DateTime StartDate()
        {
            throw new NotImplementedException();
        }

        public DateTime FinishDate()
        {
            throw new NotImplementedException();
        }

        public bool Calculated { get; set; }
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
        public void SpreadChanges(IBudgetComponentItem toSpread)
        {
            throw new NotImplementedException();
        }

        public decimal Cost { get; }
        public bool IsCostCalculated { get; set; }
        public IBudgetPresenter Budget { get; set; }
    }
}
