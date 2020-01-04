using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public abstract class MeasurableUnit : EntityBase,IMeasurableUnit
    {
        //private IEntity _holder;
        //public IEntity Holder
        //{
        //    get { return _holder; }
        //    set { _holder = value; }
        //}
        public string MeasurementUnit { get; set; }
        public decimal Amount { get; set; }
    }
}
