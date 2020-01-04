using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.Equipment
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned activities of an equipment budget component.
    /// </summary>
    public class EquipmentExecutedActivityManagerApplicationServices :
        ExecutedActivityItemManagerApplicationServicesBase,
        IEquipmentExecutedActivityManagerApplicationServices
    {
       
    }
}