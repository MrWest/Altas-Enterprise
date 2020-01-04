using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    /// <summary>
    /// Contract of the repository to handle the data operations regarding investment elements
    /// (<see cref="IInvestmentElement"/> instances).
    /// </summary>
    public interface IInvestmentElementRepository2<T> : IRelatedRepository<T, IBudget>,
        IRelatedRepository<T, IPeriod> 
        where T : class, IInvestmentElement
    {
    }
}
