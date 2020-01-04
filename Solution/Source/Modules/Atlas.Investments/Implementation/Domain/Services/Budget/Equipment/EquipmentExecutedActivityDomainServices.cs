using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Equipment
{
    /// <summary>
    ///     Implementation of the contract <see cref="IEquipmentExecutedActivityDomainServices" />, representing a domain
    ///     services ensuring that the business rules for the <see cref="IExecutedActivity" /> of a certain
    ///     <see cref="IEquipmentComponent" /> are respected.
    /// </summary>
    public class EquipmentExecutedActivityDomainServices :
        ExecutedActivityDomainServicesBase,
        IEquipmentExecutedActivityDomainServices
    {
    }
}