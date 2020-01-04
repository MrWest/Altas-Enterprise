using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class UnderGroupActivityPresenter: UnderGroupItemPresenter<IUnderGroupActivity, IUnderGroupActivityManagerApplicationServices>, IUnderGroupActivityPresenter
    {
        protected override IUnderGroupActivityManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.UnderGroup = UnderGroup.Object;
            return service;
        }
        public IPeriodPresenter Period
        {
            get; set;
        }
        protected ISubSpecialityPresenter _SubSpeciality;
        /// <summary>
        /// Get or sets the Expense Concept of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public ISubSpecialityPresenter SubSpeciality
        {
            get
            {
                if (_SubSpeciality == null)
                {
                    _SubSpeciality = ServiceLocator.Current.GetInstance<ISubSpecialityPresenter>();
                    var provider = ServiceLocator.Current.GetInstance<IEntityProviderManagerApplicationServices<ISubSpeciality>>();

                    var entity = provider.GetEntity(Object.SubSpeciality);
                    //entity = ServiceLocator.Current.GetInstance<ISubExpenseConcept>();
                    //entity.Name = "ANTIMETA";
                    if (entity != null)
                        _SubSpeciality.Object = entity;
                }
                //  return Object.SubExpenseConcept != null ? ServiceLocator.Current.GetInstance<IExpenseConceptProvider>().ExpenseConcepts.FirstOrDefault(x => true) : null;
                return _SubSpeciality;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.SubSpeciality = v.Object.Id, value);
                    OnPropertyChanged(() => SubExpenseConcept);
                }

            }

        }

        public override decimal Quantity
        {
            get
            {
                return Object.Quantity;
            }
            set
            {
                OnPropertyChanged(()=>Quantity);
            }
        }

        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "UnderGroupActivity"; }
        }

        public override void NotifyUp()
        {
            OnPropertyChanged(() => Quantity);
            OnPropertyChanged(() => UnitaryCost);
            OnPropertyChanged(() => Cost);
          
            UnderGroup.DoNotify();
        }


        public override void SpreadChanges(IBudgetComponentItem toSpread)
        {
            if (toSpread.GetType().Implements<IPlannedResource>())
                foreach (
                    IPlannedResourcePresenter<IUnderGroupActivity> plannedResourcePresenter in PlannedResources.Items)
                    plannedResourcePresenter.SpreadChanges(toSpread);

            else
            {
                if (Code == toSpread.Code)
                {
                    if (toSpread.GetType().Implements<IUnderGroupActivity>())
                        base.SpreadChanges(toSpread);
                    if (toSpread.GetType().Implements<IPlannedActivity>())
                        CreateServices().AdquirePlannedProperties(Object, toSpread as IPlannedActivity);

                   
                    
                    Items.Load();
                    OnPropertyChanged(()=> Items);
                    OnPropertyChanged(() => Name);
                    OnPropertyChanged(() => Description);
                    OnPropertyChanged(() => Code);
                    OnPropertyChanged(() => MeasurementUnit);
                    OnPropertyChanged(() => UnitaryCost);
                    OnPropertyChanged(() => Currency);
                    OnPropertyChanged(() => SubExpenseConcept);
                    OnPropertyChanged(() => Category);
                    OnPropertyChanged(() => Quantity);

                }

            }
              
        }

      

        //public bool ExistPlannedResource(string code)
        //{
        //    var resourceViewModel = ServiceLocator.Current.GetInstance<IPlannedResourceViewModel<IUnderGroupActivity>>();
        //    resourceViewModel.Component = this;
        //   return resourceViewModel.ExistPlannedResource(code);
        //}

        public IPlannedResourcePresenter<IUnderGroupActivity> GetPlannedResource(string code)
        {
            var resourceViewModel = ServiceLocator.Current.GetInstance<IPlannedResourceViewModel<IUnderGroupActivity>>();
            resourceViewModel.Component = this;
            return resourceViewModel.GetPlannedResource(code);
        }

        public void AddFromScratch(string code, string name, string desc, string muId, string cuId, decimal norm, decimal price,
            int kind, string muwId, decimal wv)
        {
            var resourceViewModel = ServiceLocator.Current.GetInstance<IPlannedResourceViewModel<IUnderGroupActivity>>();
            resourceViewModel.Component = this;
            resourceViewModel.AddFromScratch(code, name, desc, muId, cuId, norm,  price, kind, muwId, wv);
        }
    }
}