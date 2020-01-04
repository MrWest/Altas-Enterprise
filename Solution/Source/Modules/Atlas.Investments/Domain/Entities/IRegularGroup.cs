using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IRegularGroup:IPriceSystemGroup
    {
        IOverGroup OverGroup { get; set; }
        string OverGroupId { get; set; }
        IList<IUnderGroup> UnderGroups { get; }  
    }
}
