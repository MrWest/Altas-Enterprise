using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    /// Implements the main interface describing an activity for a budget component
    /// </summary>
    /// <typeparam name="TItem">represents the activity</typeparam>
    /// <typeparam name="TComponent"> the budget component related with the activity</typeparam>
    public class PlannedActivityDomainServices : ActivityDomainServicesBase<IPlannedActivity>, IPlannedActivityDomainServices
        ////where TItem:class ,IPlannedActivity
        //where TComponent : class , IBudgetComponent
    {
        //private TComponent _component;
        //public TComponent Component
        //{
        //    get
        //    {
        //        if (_component == null)
        //            throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

        //        return _component;
        //    }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("value");

        //        _component = value;
        //    }
        //}
        /// <summary>
        ///     When overridden in a deriver it sets the data for the given item.
        /// </summary>
        /// <param name="item">The just created item that is in need of its creation data.</param>
        protected override void SetDataToNew(IPlannedActivity item)
        {
            if (item == null)
                throw new ArgumentNullException("plannedActivity");

            item.Name = Resources.NewPlannedActivityName;
            item.Description = Resources.NewPlannedActivityDescription;
           // item.Code = Guid.NewGuid().ToString();
            var period = ServiceLocator.Current.GetInstance<IPeriod>();
            period.Holder = item;
            item.Period = period;
        }

        //protected override IPlannedActivity SetComponent(IPlannedActivity addedItem, TComponent component)
        //{
        //    addedItem.Component = Component;
        //    return addedItem;
        //}
    }
}
