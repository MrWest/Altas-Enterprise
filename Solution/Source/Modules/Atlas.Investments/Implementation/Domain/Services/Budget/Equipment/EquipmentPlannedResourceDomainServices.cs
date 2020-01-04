using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Equipment
{
    /// <summary>
    /// Implementation of the contract <see cref="IEquipmentPlannedResourceDomainServices"/>, representing a domain
    /// services ensuring that the business rules for the <see cref="IPlannedResource"/> of a certain
    /// <see cref="IEquipmentComponent"/> are respected.
    /// </summary>
    public class EquipmentPlannedResourceDomainServices :
        PlannedResourceDomainServicesBase<IEquipmentComponent>,
        IEquipmentPlannedResourceDomainServices
    {
    }
}
