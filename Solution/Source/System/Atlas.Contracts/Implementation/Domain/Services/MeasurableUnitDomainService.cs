using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class MeasurableUnitDomainService<TMeasurable> : DomainServicesBase<TMeasurable>, IMeasurableUnitDomainService<TMeasurable>
    where TMeasurable : class ,IMeasurableUnit
    {
        private IEntity _holder;
        public IEntity Holder { get { return _holder; } set { _holder = value; } }

        /// <summary>
        ///     Creates a new instance of a IWeight.
        /// </summary>
        /// <returns>A new instance of type <see cref="IWeight" />.</returns>
        public override TMeasurable Create()
        {
            TMeasurable measurable = base.Create();

            return measurable;
        }
    }
}
