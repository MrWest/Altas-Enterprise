using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    /// Represents the application services provider letting the upper layers (like Presentation) perform CRUD-operations 
    /// implying investment elements (<see cref="IInvestmentElement"/> instances).
    /// </summary>
    public interface IInvestmentElementManagerApplicationServices<TElement>: IItemManagerApplicationServices<TElement>
        where TElement: class ,IInvestmentElement
    {
        TElement ExportRelated(IDatabaseContext exportDatabaseContext, TElement item);
        int InDeep(IInvestmentElement investmentElement);
    }
}
