using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract to be implemented by the PriceSystem crud view models, these view models will handle the CRUD operations for PriceSystem
    ///     domain entities in the UI (presentation layer).
    /// </summary>
    public interface IPriceSystemViewModel: ICrudViewModel<IPriceSystem, IPriceSystemPresenter>, IPriceSystemProvider
    {
    }

    public interface IPriceSystemProvider
    {
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IPriceSystemPresenter> PriceSystems { get; }
    }
}
