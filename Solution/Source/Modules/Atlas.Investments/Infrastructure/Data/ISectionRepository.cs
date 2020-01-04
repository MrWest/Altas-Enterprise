using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    /// <summary>
    ///     Contract to be implemented by the repositories to be implemented by the repositories handling 
    ///    data operations for
    ///     section domain entities.
    /// </summary>
    public interface ISectionRepository : IRelatedRepository<ISection,IPriceSystem>
    {
        /// <summary>
        ///     Gets the reference to the <see cref="IInvestmentElement" /> containing the investment components handled in the
        ///     current repository.
        /// </summary>
        IPriceSystem AboveSection{ get; set; }
    }
}
