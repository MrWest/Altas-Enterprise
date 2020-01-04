using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment
{
    /// <summary>
    ///     Contract to be implemented by the domain services managing the business rules for the set of
    ///     <see cref="IExecutedActivity" /> of an <see cref="IEquipmentComponent" />.
    /// </summary>
    public interface IEquipmentExecutedActivityDomainServices :
        IExecutedActivityDomainServices
    {
    }
}