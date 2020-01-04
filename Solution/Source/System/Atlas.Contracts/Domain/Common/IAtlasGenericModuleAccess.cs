using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IAtlasGenericModuleAccess : INomenclator
    {
        object User { get; set; }
        IList<IAtlasModuleRole> Rols { get; set; }
        IList<IAtlasModuleAccess> OwnedAccesses { get; set; }
    }
}
