using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class UnderGroupPresenter : PriceSystemGroupPresenter<IUnderGroup,IUnderGroupManagerApplicationServices>, IUnderGroupPresenter
    {
        //public UnderGroupPresenter(IUnderGroup nomenclator) : base(nomenclator)
        //{
        //}

        public IRegularGroupPresenter RegularGroup { get; set; }

        public override INavigable Parent
        {
            get { return RegularGroup; }
        }

        private IUnderGroupActivityViewModel _activities;
        private IUnderGroupResourceViewModel _resources;

        public IUnderGroupActivityViewModel Activities
        {
            get
            {
                if (_activities == null)
                {
                    _activities = ServiceLocator.Current.GetInstance<IUnderGroupActivityViewModel>();
                    _activities.UnderGroup = this;
                    _activities.Load();
                    _activities.Raised += OnInteractionRequested;
                }
                return _activities;
            }
        }

        public IUnderGroupResourceViewModel Resources
        {
            get
            {
                if (_resources == null)
                {
                    _resources = ServiceLocator.Current.GetInstance<IUnderGroupResourceViewModel>();
                    _resources.UnderGroup = this;
                    _resources.Load();
                    _resources.Raised += OnInteractionRequested;
                }
                return _resources;
            }
        }




        public override Thickness DeepThickness
        {
            get { return new Thickness(16, 0, 0, 0); }
        }

        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "UnderGroup"; }
        }

        public override ICrudViewModel Items
        {
            get { return Activities; }
        }

        //public override ICommand AddNewItem
        //{
        //    get { return VariantLines.AddCommand; }
        //}

        //public override ICommand DeleteMySelfcoCommand
        //{
        //    get { return RegularGroup.UnderGroups.DeleteCommand; }
        //}

        protected override IUnderGroupManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.RegularGroup = RegularGroup.Object;
            return service;
            
        }

        public void SpreadChanges(IBudgetComponentItem toSpread)
        {
            if(!Activities.IsLoaded)
                Activities.Load();
            foreach (IUnderGroupActivityPresenter underGroupActivityPresenter in Activities.Items)
            {
                underGroupActivityPresenter.SpreadChanges(toSpread);
            }
        }

        public bool ExistActivity(string code)
        {
            return CreateServices().ExistActivity(code, Object);
        }

        public IUnderGroupActivityPresenter GetActivity(string code)
        {
            var activity = CreateServices().GetActivity(code, Object);
            var activities = ServiceLocator.Current.GetInstance<IUnderGroupActivityViewModel>();
            activities.UnderGroup = this;
            return activities.CreatePresenter(activity);
        }

        public void AddFromScratch(string code, string name, string desc, string muId, string cuId, decimal price)
        {
            var activities = ServiceLocator.Current.GetInstance<IUnderGroupActivityViewModel>();
            activities.UnderGroup = this;
            activities.AddFromScratch(code, name, desc, muId, cuId, price);
        }

        //public void Notify()
        //{
        //    OnPropertyChanged(() => Items);
        //    OnPropertyChanged(() => HasItems);
        //    OnPropertyChanged(() => IconData);
        //}

        //public IList<IPlannedActivityPresenter<IUnderGroup>> Items { get { return (IList<IPlannedActivityPresenter<IUnderGroup>>) VariantLines.Items; } }

        //public ICommand FilterCommand { get; }

        //public object View { get; set; }

        //public string FilterCriteria { get; set; }
    }
}
