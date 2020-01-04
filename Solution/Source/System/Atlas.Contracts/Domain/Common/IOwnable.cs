using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    /// <summary>
    /// Entity who has or not an owner
    /// </summary>
    public interface IOwnable
    {
        IEntity OwnerEntity { get; set; }
        string OwnerEntityId { get; set; }
    }
}
