using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db4objects.Db4o.Ext;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasMainModuleAccessViewModel : NavigableViewModel<IAtlasMainModuleAccess, IAtlasMainModuleAccessPresenter, IAtlasMainModuleAccessManagerApplicationServices>, IAtlasMainModuleAccessViewModel
    {
       public String AssemblyName { get; set; }
       public IEnumerable<IPresenter> Collection { get; set; }

       
        protected override IAtlasMainModuleAccessPresenter CreatePresenterFor(IAtlasMainModuleAccess item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.AssemblyName = AssemblyName;
            presenter.Collection = Collection;
            return presenter;
        }

        protected override IAtlasMainModuleAccessManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.AssemblyName = AssemblyName;
            return service;
        }

        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            base.OnDeletedItem(sender, e);
            
            Filter("");
        }
    }
}
