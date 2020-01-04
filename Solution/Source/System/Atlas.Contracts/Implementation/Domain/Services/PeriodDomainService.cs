using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    /// <summary>
    /// Implements the domain interface for a Period domain service
    /// </summary>
    public class PeriodDomainService : DomainServicesBase<IPeriod>, IPeriodDomainService
    {
        private IEntity _holder;
        public IEntity Holder { get { return _holder; } set { _holder = value; } }

        /// <summary>
        ///     Creates a new instance of a Price System.
        /// </summary>
        /// <returns>A new instance of type <see cref="IPriceSystem" />.</returns>
        public override IPeriod Create()
        {
            IPeriod period = base.Create();
            period.Holder = Holder;
            period.Name = Holder.GetType().Implements<INomenclator>()?((INomenclator)Holder).Name+"_"+Resources.NewPeriod : Resources.NewPeriod;

            return period;
        }
    }
}
