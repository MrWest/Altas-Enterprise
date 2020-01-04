using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class UnderGroupResourceViewModel: UnderGroupItemViewModel<IUnderGroupResource, IUnderGroupResourcePresenter, IUnderGroupResourcesManagerApplicationServices>, IUnderGroupResourceViewModel
    {
        protected override IUnderGroupResourcePresenter CreatePresenterFor(IUnderGroupResource budgetComponentItem)
        {
            var presenter =  base.CreatePresenterFor(budgetComponentItem);
            presenter.UnderGroup = UnderGroup;

            var weightPresenter = ServiceLocator.Current.GetInstance<IWeightPresenter>();
            weightPresenter.Object = ServiceLocator.Current.GetInstance<IMeasurableUnitManagerApplicationServices<IWeight>>().Find(budgetComponentItem.Weight.Id) ??
               budgetComponentItem.Weight;
           // weightPresenter.Object.Holder = presenter.Object;
            weightPresenter.Holder = presenter;

            presenter.Weight = weightPresenter;

            var volumePresenter = ServiceLocator.Current.GetInstance<IVolumePresenter>();
            volumePresenter.Object = ServiceLocator.Current.GetInstance<IMeasurableUnitManagerApplicationServices<IVolume>>().Find(budgetComponentItem.Volume.Id) ??
                budgetComponentItem.Volume;
            //volumePresenter.Object.Holder = presenter.Object;
            volumePresenter.Holder = presenter;

            presenter.Volume = volumePresenter;
            return presenter;
        }

        protected override IUnderGroupResourcesManagerApplicationServices CreateServices()
        {
            var services = base.CreateServices();
            services.UnderGroup = UnderGroup.Object;
            return services;
        }
    }
}