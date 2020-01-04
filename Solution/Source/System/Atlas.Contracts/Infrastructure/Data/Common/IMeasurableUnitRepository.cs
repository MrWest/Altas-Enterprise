using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Common
{
    public interface IMeasurableUnitRepository<TMeasurable> : IRepository<TMeasurable>
        where TMeasurable : class ,IMeasurableUnit
    {
       // IEntity Holder { get; set; }
    }
}
