using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IInvestmentTypeProvider
    {
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="ICategory" /> domain entities in the system.
        /// </summary>
        IEnumerable<IInvestmentTypePresenter> InvestmentTypes { get; }
    }
}