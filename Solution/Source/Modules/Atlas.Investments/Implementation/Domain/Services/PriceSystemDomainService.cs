using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implements some domain services used to enforce operations for price system domain
    ///     entities.
    /// </summary>
    public class PriceSystemDomainService : DomainServicesBase<IPriceSystem>, IPriceSystemDomainService
    {
        /// <summary>
        ///     Creates a new instance of a Price System.
        /// </summary>
        /// <returns>A new instance of type <see cref="IPriceSystem" />.</returns>
        public override IPriceSystem Create()
        {
            IPriceSystem ps = base.Create();
            ps.Name = Resources.NewPriceSystem;

            return ps;
        }
    }
}
