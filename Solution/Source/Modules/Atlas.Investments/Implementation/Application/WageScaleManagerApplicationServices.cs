using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWageScaleManagerApplicationServices" /> being the application services used
    ///     to manage the CRUD operations coming from upper layers regarding to the Wage Scale domain entities.
    /// </summary>
    public class WageScaleManagerApplicationServices :
        ItemManagerApplicationServicesBase<IWageScale, IWageScaleRepository, IWageScaleDomainServices>,
        IWageScaleManagerApplicationServices
    {
    }
}