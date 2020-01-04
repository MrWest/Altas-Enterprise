using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    /// <summary>
    /// Contract of the repository to handle the data operations regarding investment elements
    /// (<see cref="IInvestmentElement"/> instances).
    /// </summary>
    public interface IInvestmentElementRepository : 
        IRelatedRepository<IInvestmentElement, IInvestmentElement>,
        IRelatedRepository<IInvestmentElement, IBudget>,
        IRelatedRepository<IInvestmentElement, IPeriod>
    {
        /// <summary>
        /// Gets or sets the <see cref="IInvestmentElement"/> being the parent of those handled in the current
        /// <see cref="IInvestmentElementRepository"/>.
        /// </summary>
        IInvestmentElement Parent { get; set; }
        ///// <summary>
        ///// Gets or sets the time period in the current
        ///// <see cref="IInvestmentElementRepository"/>.
        ///// </summary>
        //IPeriod Period { get; set; }
    }
}
