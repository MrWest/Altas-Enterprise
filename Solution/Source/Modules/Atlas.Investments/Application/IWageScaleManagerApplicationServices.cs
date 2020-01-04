using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    ///     Contract for the application services managing the CRUD operations coming from upper layers such as Presentation
    ///     regarding to Wage Scale entities.
    /// </summary>
    public interface IWageScaleManagerApplicationServices : IItemManagerApplicationServices<IWageScale>
    {
    }
}