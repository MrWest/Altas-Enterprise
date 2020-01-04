using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public abstract class SubSpecialityHolderViewModel<THolder,TComponent, TPresenter,TService> : NavigableViewModel<THolder, TPresenter,TService>, ISubSpecialityHolderViewModel<THolder,TComponent,TPresenter>
        where TComponent : class, IBudgetComponent
        where TPresenter : class , ISubSpecialityHolderPresenter<THolder,TComponent>
        where THolder:class,ISubSpecialityHolder
        where TService:class,ISubSpecialityHolderManagerApplicationServices<THolder>
    {
        public IBudgetComponentPresenter<TComponent> BudgetComponent { get; set; }
        protected override TService CreateServices()
        {
            var service = base.CreateServices();
            service.BudgetComponent = BudgetComponent.Object;
            return service;
        }

        protected override TPresenter CreatePresenterFor(THolder item)
        {
             
            var presenter = base.CreatePresenterFor(item);
            presenter.BudgetComponent = BudgetComponent;
            return presenter;
        }

       
    }
}