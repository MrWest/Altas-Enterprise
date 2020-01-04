using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface ICurrenciable: ICosttable
    {
        string Currency { get; set; }

        
    }
}
