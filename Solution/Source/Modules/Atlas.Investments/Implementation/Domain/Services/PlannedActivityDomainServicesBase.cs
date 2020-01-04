using System;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    /// Base class of the domain services managing the business rules of the planned activities of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component which items business rules are handled here.</typeparam>
    public abstract class PlannedActivityDomainServicesBase<TComponent> :
        PlannedActivityDomainServices
        where TComponent : class, IBudgetComponent
    {
        /// <summary>
        /// Sets the creation data to the given planned activity.
        /// </summary>
        /// <param name="plannedActivity">The <see cref="IPlannedActivity"/> to set the data to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedActivity"/> is null.</exception>
        protected override void SetDataToNew(IPlannedActivity plannedActivity)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");

            plannedActivity.Name = Resources.NewPlannedActivityName;
            plannedActivity.Description = Resources.NewPlannedActivityDescription;
            plannedActivity.Code = Guid.NewGuid().ToString();
        }
    }
}
