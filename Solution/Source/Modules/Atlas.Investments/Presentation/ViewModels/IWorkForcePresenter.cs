using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract for the presenter view models used to decorate and impersonate instances of the domain entity: "Work
    ///     Force" in the UI.
    /// </summary>
    public interface IWorkForcePresenter : IPresenter<IWorkForce>
    {
        /// <summary>
        ///     Gets or sets the code of the underlying <see cref="IWorkForce" />.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        ///     Gets or sets the name of the underlying <see cref="IWorkForce" />.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the wage scale of the underlying <see cref="IWorkForce" />.
        /// </summary>
        IWageScalePresenter WageScale { get; set; }

        /// <summary>
        ///     Gets the retribution of the underlying <see cref="IWorkForce" />.
        /// </summary>
        decimal Retribution { get; }

        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenters (<see cref="IWageScalePresenter" />) wrapping the
        ///     <see cref="IWageScale" /> there are in the system.
        /// </summary>
        IEnumerable<IWageScalePresenter> WageScales { get; }
    }
}