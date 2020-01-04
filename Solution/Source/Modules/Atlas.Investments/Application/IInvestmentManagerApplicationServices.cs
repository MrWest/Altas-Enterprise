using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    ///     Represents the application services provider letting the upper layers (like Presentation) perform CRUD-operations
    ///     implying investment elements (<see cref="IInvestmentElement" /> instances).
    /// </summary>
    public interface IInvestmentManagerApplicationServices : IInvestmentElementManagerApplicationServices<IInvestment>, IExportable<IInvestment>
    {
        //bool Export(IDatabaseContext databaseContext, IInvestment investment);
        IEnumerable<IInvestment> FindInvestmentTypeByContains(string text);
        IEnumerable<IInvestment> FindNatureByContains(string text);
        IEnumerable<IInvestment> FindImpactByContains(string text);
    }
}