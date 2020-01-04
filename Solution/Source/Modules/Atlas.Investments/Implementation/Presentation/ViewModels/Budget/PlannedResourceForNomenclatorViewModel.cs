using System;
using System.Linq;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class PlannedResourceForNomenclatorViewModel:NomenclatorViewModel<IPlannedResource, IPlannedResourceForNomenclatorViewModel>, IPlannedResourceForNomenclatorViewModel
    {
        private object _activePs =
            ServiceLocator.Current.GetInstance<IPriceSystemProvider>().PriceSystems.SingleOrDefault(x => x.IsActive)?.Object.Id;

        protected override Tuple<Func<IPlannedResource, bool>, string> GetAddedExpression()
        {

            return Tuple.Create<Func<IPlannedResource, bool>, string>(x =>
                  !Equals(x.PriceSystem, null) && !Equals(_activePs, null) &&
                  (_activePs.ToString() == x.PriceSystem.ToString()), "PriceSystem");

        }
    }
}