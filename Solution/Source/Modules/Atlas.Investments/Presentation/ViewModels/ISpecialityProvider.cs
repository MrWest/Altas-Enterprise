using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    interface ISpecialityProvider
    {
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IExpenseConcept" /> domain entities in the system.
        /// </summary>
        IEnumerable<ISpecialityPresenter> Specialities { get; }
    }
}
