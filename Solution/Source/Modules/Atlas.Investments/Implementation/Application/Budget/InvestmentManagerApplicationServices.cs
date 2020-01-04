using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentManagerApplicationServices" /> representing the application
    ///     services provider object responding to the CRUD requests coming from upper layers regarding to the domain entities:
    ///     investments.
    /// </summary>
    public class InvestmentManagerApplicationServices :
        ItemManagerApplicationServicesBase<IInvestment, IInvestmentRepository, IInvestmentDomainServices>,
        IInvestmentManagerApplicationServices
    {
    }
}