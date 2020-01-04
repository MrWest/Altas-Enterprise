using System;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Construction
{
    /// <summary>
    /// Implementation of the contract <see cref="IConstructionPlannedActivityDomainServices"/>, representing a domain
    /// services ensuring that the business rules for the <see cref="IPlannedActivity"/> of a certain
    /// <see cref="IConstructionComponent"/> are respected.
    /// </summary>
    public class ConstructionPlannedActivityDomainServices :
        PlannedActivityDomainServicesBase<IConstructionComponent>,
        IConstructionPlannedActivityDomainServices
    {
      
    }
}
