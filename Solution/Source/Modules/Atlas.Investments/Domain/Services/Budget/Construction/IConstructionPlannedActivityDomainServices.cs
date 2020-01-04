using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services.Budget.Construction
{
    /// <summary>
    ///     Contract to be implemented by the domain services managing the business rules for the set of
    ///     <see cref="IPlannedActivity" /> of an <see cref="IConstructionComponent" />.
    /// </summary>
    public interface IConstructionPlannedActivityDomainServices :
        IPlannedActivityDomainServices
    {
    }
}