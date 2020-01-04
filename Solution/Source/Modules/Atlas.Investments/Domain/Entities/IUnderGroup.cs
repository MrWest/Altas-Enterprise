using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db4objects.Db4o.Internal.Handlers;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IUnderGroup:IPriceSystemGroup//,IBudgetComponent
    {
        IRegularGroup RegularGroup { get; set; }
        string RegularGroupId { get; set; }
        IList<IUnderGroupActivity> Activities { get; }
        IList<IUnderGroupResource> Resources { get; }
       
    }
}
