using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Transfer
{
    public interface IExportable
    {
        bool Export(IDatabaseContext databaseContext);
       
    }
}
