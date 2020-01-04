using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    ///     Contract for the application services managing the CRUD operations coming from upper layers such as Presentation
    ///     regarding to ExpenseConcept entities.
    /// </summary>
    public interface ISpecialityManagerApplicationServices : IItemManagerApplicationServices<ISpeciality>,IExportable<ISpeciality>
    {
        
    }
}