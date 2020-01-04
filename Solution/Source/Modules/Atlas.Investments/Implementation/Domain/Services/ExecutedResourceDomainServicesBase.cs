using System;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    ///// <summary>
    ///// Base class of the domain services managing the business rules of the executed resources of a certain budget component.
    ///// </summary>
    ///// <typeparam name="TComponent">The type of the budget component which items business rules are handled here.</typeparam>
    //public abstract class ExecutedResourceDomainServicesBase<TComponent> :
    //    ExecutedBudgetComponentItemDomainServicesBase<IExecutedResource, TComponent>
    //    where TComponent : class, IBudgetComponent
    //{
    //    /// <summary>
    //    /// Sets the creation data to the given executed resource.
    //    /// </summary>
    //    /// <param name="executedResource">The <see cref="IExecutedResource"/> to set the data to.</param>
    //    /// <exception cref="ArgumentNullException"><paramref name="executedResource"/> is null.</exception>
    //    protected override void SetDataToNew(IExecutedResource executedResource)
    //    {
    //        if (executedResource == null)
    //            throw new ArgumentNullException("executedResource");

    //        executedResource.Name = Resources.NewExecutedResourceName;
    //        executedResource.Description = Resources.NewExecutedResourceDescription;
    //        executedResource.Code = Guid.NewGuid().ToString();
    //    }
    //}
}
