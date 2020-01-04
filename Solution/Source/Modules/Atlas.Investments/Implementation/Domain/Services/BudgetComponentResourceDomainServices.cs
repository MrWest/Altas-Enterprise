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
{/// <summary>
    /// Defines the main interface describing the resourses associeted to another budget component item 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TComponentItem"></typeparam>
    public class BudgetComponentResourceDomainServices<TComponentItem> : BudgetComponentItemDomainServicesBase<IPlannedResource>, IBudgetComponentResourceDomainServices<TComponentItem>
        ////where TItem : class, IPlannedResource
        where TComponentItem : class , IBudgetComponentItem
    {

        private TComponentItem _component;
        public TComponentItem  Component
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
        /// <summary>
        ///     When overridden in a deriver it sets the data for the given item.
        /// </summary>
        /// <param name="item">The just created item that is in need of its creation data.</param>
        protected override void SetDataToNew(IPlannedResource item)
        {
            if (item == null)
                throw new ArgumentNullException("plannedResource");

            item.Name = Resources.NewPlannedResourceName;
            item.Description = Resources.NewPlannedResourceDescription;
           // item.Code = Guid.NewGuid().ToString();
            var weight = ServiceLocator.Current.GetInstance<IWeight>();
           // weight.Holder = item;
            item.Weight = weight;
            var volume = ServiceLocator.Current.GetInstance<IVolume>();
           // volume.Holder = item;
            item.Volume = volume;

            item.Component = Component;
            item.PriceSystem = Component.PriceSystem;
        }

        //protected override TItem SetComponent(TItem addedItem, TComponentItem component)
        //{
        //    addedItem.Component = Component;
        //    return addedItem;
        //}
    }
}
