using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital
{
    /// <summary>
    ///     Contract to be implemented by the domain services managing the business rules for the set of
    ///     <see cref="IPlannedActivity" /> of an <see cref="IWorkCapitalComponent" />.
    /// </summary>
    public interface IWorkCapitalPlannedActivityDomainServices :
         IPlannedActivityDomainServices
    {
    }
}