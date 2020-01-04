using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    /// <summary>
    /// Implements the domain interface for a <see cref="IUnitConverter"/> domain service
    /// </summary>
    public class UnitConverterDomainService : DomainServicesBase<IUnitConverter>, IUnitConverterDomainService
    {
        private IConvertibleEntity _conversionForEntity;

        public IConvertibleEntity ConversionForEntity {
            get { return _conversionForEntity; }
            set{_conversionForEntity = value;} 
        }

        /// <summary>
        ///     Creates a new instance of an WageScale.
        /// </summary>
        /// <returns>A new instance of type <see cref="IConvertibleEntity" />.</returns>
        public override IUnitConverter Create()
        {
            IUnitConverter currency = base.Create();
            currency.Factor = 1;
            currency.ConversionForEntity = ConversionForEntity;
           

            return currency;
        }
    }
}
