using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Describes a Section feature. Created to arrange data in a price system
    /// </summary>
    public interface ISection:IPriceSystem,IBudgetComponent
    {

        /// <summary>
        /// Gets the list of planned Activities composing the current <see cref="ISection"/>.
        /// </summary>
        IList<IPlannedActivity> PlannedActivities { get; }

        /// <summary>
        ///     Gets or sets the parent investment element (<see cref="IInvestmentElement" />) of the current one.
        /// </summary>
        IPriceSystem AboveSection { get; set; }

        /// <summary>
        /// Gets the list of Sections for the current <see cref="ISection"/>.
        /// </summary>
        IList<ISection> Sections { get; }

    }
}
