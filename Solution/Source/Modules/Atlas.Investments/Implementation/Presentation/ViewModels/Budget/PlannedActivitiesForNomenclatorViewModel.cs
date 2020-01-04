using System;
using System.Linq;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class PlannedActivitiesForNomenclatorViewModel:NomenclatorViewModel<IPlannedActivity, IPlannedActivitiesForNomenclatorViewModel>, IPlannedActivitiesForNomenclatorViewModel
    {
        private object _activePs =
           ServiceLocator.Current.GetInstance<IPriceSystemProvider>().PriceSystems.SingleOrDefault(x=> x.IsActive)?.Object.Id;

        protected override Tuple<Func<IPlannedActivity, bool>, string> GetAddedExpression()
        {

            return Tuple.Create<Func<IPlannedActivity, bool>, string>(x =>
                       !Equals(x.PriceSystem, null) && !Equals(_activePs, null) &&
                       (_activePs.ToString() == x.PriceSystem.ToString()), "PriceSystem");



        }
        //public override void Load()
        //{
        //    foreach (INomenclatorPresenter<IPlannedActivity> presenter in Items)
        //        presenter.PropertyChanged -= OnPresenterPropertyChanged;

        //    _items.Clear();

        //    var priceSystemProvider =ServiceLocator.Current.GetInstance<IPriceSystemManagerApplicationService>();
        //    var activePs = priceSystemProvider.Items.SingleOrDefault(x=>x.IsActive);



        //    ExecuteUsingServices(services =>
        //    {
        //        foreach (IPlannedActivity item in GetItems(services).Where(x=>Equals(x.PriceSystem,null)|| x?.PriceSystem.ToString() == activePs?.Id.ToString()))
        //        {

        //            INomenclatorPresenter<IPlannedActivity> presenter = CreatePresenterFor(item);
        //            presenter.PropertyChanged += OnPresenterPropertyChanged;

        //            _items.Add(presenter);
        //        }
        //    });

        //}
    }
}