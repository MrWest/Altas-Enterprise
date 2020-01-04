using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain
{
    /// <summary>
    /// Iterface for a Period Domain Service
    /// </summary>
    public interface IPeriodDomainService : IDomainServices<IPeriod>
    {
        IEntity Holder { get; set; }
    }
}
