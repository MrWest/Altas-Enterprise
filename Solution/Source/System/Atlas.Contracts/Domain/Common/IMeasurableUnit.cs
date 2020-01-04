using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IMeasurableUnit:IEntity
    {
       // IEntity Holder { get; set; }
        string MeasurementUnit { get; set; }
        decimal Amount { get; set; }
    }
}
