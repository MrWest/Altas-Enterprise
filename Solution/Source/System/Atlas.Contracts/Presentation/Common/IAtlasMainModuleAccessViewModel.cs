using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasMainModuleAccessViewModel:ICrudViewModel<IAtlasMainModuleAccess, IAtlasMainModuleAccessPresenter>
    {
        String AssemblyName { get; set; }
        IEnumerable<IPresenter> Collection { get; set; }
    }
}
