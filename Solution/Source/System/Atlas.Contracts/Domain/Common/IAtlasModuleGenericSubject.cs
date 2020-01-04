using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IAtlasModuleGenericSubject : INomenclator
    {
        object Content { get; set; }
        IList<IAtlasModuleSubject> Subjects { get; }
        /// <summary>
        /// Get or sets a list of Documents associated to this investment <see cref="IInvestment"/>.
        /// </summary>
        IList<IDocument> Documents { get; set; }
    }
}
