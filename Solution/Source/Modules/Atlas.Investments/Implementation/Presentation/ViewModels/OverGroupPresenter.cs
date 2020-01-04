using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class OverGroupPresenter : PriceSystemGroupPresenter<IOverGroup,IOverGroupManagerApplicationServices>, IOverGroupPresenter
    {
        //public OverGroupPresenter(IOverGroup nomenclator) : base(nomenclator)
        //{
        //}

        public IPriceSystemPresenter PriceSystem { get; set; }

        private IRegularGroupViewModel _regularGroups;
        public IRegularGroupViewModel RegularGroups
        {
            get
            {
                if (_regularGroups == null)
                {
                    _regularGroups = ServiceLocator.Current.GetInstance<IRegularGroupViewModel>();
                    _regularGroups.OverGroup = this;
                    _regularGroups.Load();
                    _regularGroups.Raised += OnInteractionRequested;
                }
                return _regularGroups;
            }
        }

        public override ICommand DeleteMySelfCommand
        {
            get { return PriceSystem.OverGroups.DeleteCommand; }
        }

        //public override bool HasItems
        //{
        //    get { return RegularGroups != null && RegularGroups.Items.Count > 0; }
        //}

        //public object SelectedItem { get { return RegularGroups.SelectedItem; } }

        public override Thickness DeepThickness
        {
            get { return new Thickness(0, 0, 0, 0); }
        }

        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override  String Kind
        {
            get { return "OverGroup"; }
        }

        //public override ICommand AddNewItem
        //{
        //    get { return RegularGroups.AddCommand; }
        //}

        //public override ICommand DeleteMySelf
        //{
        //    get { return PriceSystem.OverGroups.DeleteCommand; }
        //}

        protected override IOverGroupManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.PriceSystem = PriceSystem.Object;
            return service;
        }

        //public void Notify()
        //{
        //    OnPropertyChanged(() => Items);
        //    OnPropertyChanged(() => HasItems);
        //    OnPropertyChanged(() => IconData);
        //}

        public override ICrudViewModel Items { get { return RegularGroups; } }
        public void SpreadChanges(IBudgetComponentItem toSpread)
        {
            foreach (IRegularGroupPresenter regularGroupPresenter in RegularGroups.Items)
            {
              regularGroupPresenter.SpreadChanges(toSpread);   
            }
        }

        public bool ExistGroup(string code)
        {
            return CreateServices().ExistGroup(code, Object);
        }

        public IRegularGroupPresenter GetRegularGroup(string code)
        {
           var regularGroup = CreateServices().GetRegularGroup(code, Object);
            var regularGroups = ServiceLocator.Current.GetInstance<IRegularGroupViewModel>();
            regularGroups.OverGroup = this;
            return regularGroups.CreatePresenter(regularGroup);
        }

        public void AddFromScratch(string code, string name)
        {
            var regularGroups = ServiceLocator.Current.GetInstance<IRegularGroupViewModel>();
            regularGroups.OverGroup = this;
            regularGroups.AddFromScratch(code, name);
        }
    }
}
