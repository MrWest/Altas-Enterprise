using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    ///     Contract for the application services managing the CRUD operations coming from upper layers such as Presentation
    ///     regarding to Work Force entities.
    /// </summary>
    public interface IWorkForceManagerApplicationServices : IItemManagerApplicationServices<IWorkForce>
    {
    }
}