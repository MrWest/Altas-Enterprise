using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Application
{
    public class UnitConverterManagerApplicationServices : ItemManagerApplicationServicesBase<IUnitConverter,IUnitConverterRepository,IUnitConverterDomainService>, IUnitConverterManagerApplicationServices
    {
        private IConvertibleEntity _conversionForEntity;

        public IConvertibleEntity ConversionForEntity
        {
            get { return _conversionForEntity; }
            set { _conversionForEntity = value; }
        }

        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override IUnitConverterRepository Repository
        {
            get
            {
                IUnitConverterRepository repository = base.Repository;
                repository.ConversionForEntity = ConversionForEntity;

                return repository;
            }
        }


        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override IUnitConverterDomainService DomainServices
        {
            get
            {
                IUnitConverterDomainService domainServices = base.DomainServices;
                domainServices.ConversionForEntity = ConversionForEntity;

                return domainServices;
            }
        }
    }
}
