using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    /// Base class of the domain services managing the business rules of the planned resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component which items business rules are handled here.</typeparam>
   
    [Obsolete]
    public  class PlannedResourceDomainServicesBase<TComponent> :
        BudgetComponentResourceDomainServices< TComponent>
        where TComponent : class, IBudgetComponentItem
    {
        /// <summary>
        /// Sets the creation data to the given planned resource.
        /// </summary>
        /// <param name="plannedResource">The <see cref="IPlannedResource"/> to set the data to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedResource"/> is null.</exception>
        protected override void SetDataToNew(IPlannedResource plannedResource)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");

            plannedResource.Name = Resources.NewPlannedResourceName;
            plannedResource.Description = Resources.NewPlannedResourceDescription;
            plannedResource.Code = Guid.NewGuid().ToString();
            plannedResource.Weight = ServiceLocator.Current.GetInstance<IWeight>();
            plannedResource.Volume = ServiceLocator.Current.GetInstance<IVolume>();
            plannedResource.MenNumber = 1;
        }
    }
}
