using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IOverGroup:IPriceSystemGroup
    {
        IPriceSystem AbovePriceSystem { get; set; }
        string AbovePriceSystemId { get; set; }
        IList<IRegularGroup> RegularGroups { get; }  
    }
}
