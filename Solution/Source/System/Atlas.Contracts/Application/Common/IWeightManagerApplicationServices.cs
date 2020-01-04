using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{

    public interface IMeasurableUnitManagerApplicationServices<TMeasurable> : IItemManagerApplicationServices<TMeasurable>
          where TMeasurable : class ,IMeasurableUnit
    {
        IEntity Holder { get; set; }

    }
}
