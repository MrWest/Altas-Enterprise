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
    public class RegularGroupPresenter : PriceSystemGroupPresenter<IRegularGroup,IRegularGroupManagerApplicationServices>, IRegularGroupPresenter
    {
        //public RegularGroupPresenter(IRegularGroup nomenclator) : base(nomenclator)
        //{
        //}

        public IOverGroupPresenter OverGroup { get; set; }

        public override INavigable Parent
        {
            get { return OverGroup; }
        }

        private IUnderGroupViewModel _underGroups;
        public IUnderGroupViewModel UnderGroups
        {
            get
            {
                if (_underGroups == null)
                {
                    _underGroups = ServiceLocator.Current.GetInstance<IUnderGroupViewModel>();
                    _underGroups.RegularGroup = this;
                    _underGroups.Load();
                    _underGroups.Raised += OnInteractionRequested;
                }
                return _underGroups;
            }
        }

        //public override bool HasItems
        //{
        //    get { return UnderGroups != null && UnderGroups.Items.Count > 0; }
        //}

        //public object SelectedItem { get { return UnderGroups.SelectedItem; } }

        public override Thickness DeepThickness
        {
            get { return new Thickness( 8, 0, 0, 0); }
        }

        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "RegularGroup"; }
        }

        //public override ICommand AddNewItem
        //{
        //    get { return UnderGroups.AddCommand; }
        //}

        //public override ICommand DeleteMySelf
        //{
        //    get { return OverGroup.RegularGroups.DeleteCommand; }
        //}

        protected override IRegularGroupManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OverGroup = OverGroup.Object;
            return service;
        }

        //public void Notify()
        //{
        //    OnPropertyChanged(() => Items);
        //    OnPropertyChanged(() => HasItems);
        //    OnPropertyChanged(() => IconData);
           
        //}

        public override ICrudViewModel Items { get { return UnderGroups; }}
        public void SpreadChanges(IBudgetComponentItem toSpread)
        {

            foreach (IUnderGroupPresenter underGroupPresenter in UnderGroups.Items)
            {
                underGroupPresenter.SpreadChanges(toSpread);
            }
        }

        public bool ExistUnderGroup(string code)
        {
            return CreateServices().ExistUnderGroup(code, Object);
        }

        public IUnderGroupPresenter GetUnderGroup(string code)
        {
            var underGroup = CreateServices().GetUnderGroup(code, Object);
            var underGroups = ServiceLocator.Current.GetInstance<IUnderGroupViewModel>();
            underGroups.RegularGroup = this;
            return underGroups.CreatePresenter(underGroup);
        }

        public void AddFromScratch(string code, string name)
        {
            var underGroups = ServiceLocator.Current.GetInstance<IUnderGroupViewModel>();
            underGroups.RegularGroup = this;
            underGroups.AddFromScratch(code, name);
        }
    }
}
