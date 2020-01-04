using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// viewmodel implementation for  planned resources
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public class PlannedResourceViewModel<TComponent> : BudgetComponentItemViewModelBase<IPlannedResource, IPlannedResourcePresenter<TComponent>,  IBudgetComponentResourceManagerApplicationServices<TComponent>>,
        IPlannedResourceViewModel<TComponent>
        where TComponent : class ,IBudgetComponentItem
    {
        private IBudgetComponentItemPresenter<TComponent> _component;
        public IBudgetComponentItemPresenter<TComponent> Component
        {
            get
            {
                if (_component == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _component;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _component = value;
            }
        }
        protected override IPlannedResourcePresenter<TComponent> CreatePresenterFor(IPlannedResource budgetComponentItem)
        {
            IPlannedResourcePresenter<TComponent> presenter = base.CreatePresenterFor(budgetComponentItem);
            presenter.Component = Component;
            presenter.Object.Component = Component.Object;

            var weightPresenter = ServiceLocator.Current.GetInstance<IWeightPresenter>();
            weightPresenter.Object = budgetComponentItem.Weight;
                
                //ServiceLocator.Current.GetInstance<IMeasurableUnitManagerApplicationServices<IWeight>>().Find(budgetComponentItem.Weight.Id)??
               //budgetComponentItem.Weight;
          //  weightPresenter.Object.Holder = presenter.Object;
            weightPresenter.Holder = presenter;

            presenter.Weight = weightPresenter;

            var volumePresenter = ServiceLocator.Current.GetInstance<IVolumePresenter>();
            volumePresenter.Object = budgetComponentItem.Volume;
                //ServiceLocator.Current.GetInstance<IMeasurableUnitManagerApplicationServices<IVolume>>().Find(budgetComponentItem.Volume.Id) ??
               // budgetComponentItem.Volume;
           // volumePresenter.Object.Holder = presenter.Object;
            volumePresenter.Holder = presenter;

            presenter.Volume = volumePresenter;

            return presenter;
        }

        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
            //Component.Notify();
        }

        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            base.OnDeletedItem(sender, e);
            //Component.Notify();
        }

        public override bool CanAdd(IPlannedResourcePresenter<TComponent> presenter)
        {
            return true;
        }
        public override bool CanDelete(IPlannedResourcePresenter<TComponent> presenter)
        {
            return true;
        }

        protected override IBudgetComponentResourceManagerApplicationServices<TComponent> CreateServices()
        {
            IBudgetComponentResourceManagerApplicationServices<TComponent> service = base.CreateServices();
            service.Component = Component.Object as TComponent;
            return service;
        }

        protected override INavigable Parent { get { return Component; } }
        public void AddFromScratch(string code, string name, string desc, string mu, string cu, decimal norm, decimal price, int kind, string wmu, decimal wv)
        {
            ExecuteUsingServices(services =>
            {
                services.AddFromScratch(code, name, desc, mu,cu,norm, price, kind,  wmu,  wv);
            });

          //  Load();
        }

        public void EditFromScratch(object Id, string name, string desc, string MU, string cu, decimal norm, decimal Price, int kind, string wmu, decimal wv)
        {
            ExecuteUsingServices(services =>
            {
                services.EditFromScratch(Id, name, desc, MU, cu, norm, Price, kind,  wmu,  wv);
            });
        }

        public bool ExistPlannedResource(string code)
        {
            return CreateServices().ExistPlannedResource(code);
        }

       
        public IPlannedResourcePresenter<TComponent> GetPlannedResource(string code)
        {
            var resource = CreateServices().GetPlannedResource<TComponent>(code);

            return CreatePresenterFor(resource);
        }


        //protected override IBudgetComponentResourceManagerApplicationServices<TComponent> CreateServices()
        //{
        //    IBudgetComponentResourceManagerApplicationServices<TComponent> service = base.CreateServices();
        //    service.Component = Component;
        //    return service;
        //}

        
    }
}
