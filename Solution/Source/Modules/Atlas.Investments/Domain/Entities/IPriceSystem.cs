using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Describes a Price system features
    /// </summary>
    public interface IPriceSystem: INomenclator
    {
        /// <summary>
        /// Gets the list of Sections for the current <see cref="ISection"/>.
        /// </summary>
        //IList<ISection> Sections { get; }
        IList<IOverGroup> OverGroups { get; }

        bool IsActive { get; set; }
    }
}
