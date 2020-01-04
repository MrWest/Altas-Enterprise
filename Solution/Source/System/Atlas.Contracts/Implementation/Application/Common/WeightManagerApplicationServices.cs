using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class MeasurableUnitManagerApplicationServices<TMeasurable> : ItemManagerApplicationServicesBase<TMeasurable, IMeasurableUnitRepository<TMeasurable>, IMeasurableUnitDomainService<TMeasurable>>, IMeasurableUnitManagerApplicationServices<TMeasurable>
        where TMeasurable : class ,IMeasurableUnit
    {
        private IEntity _holder;

        public IEntity Holder
        {
            get { return _holder; }
            set { _holder = value; }
        }

        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override IMeasurableUnitRepository<TMeasurable> Repository
        {
            get
            {
                IMeasurableUnitRepository<TMeasurable> repository = base.Repository;
               // repository.Holder = Holder;

                return repository;
            }
        }

        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override IMeasurableUnitDomainService<TMeasurable> DomainServices
        {
            get
            {
                IMeasurableUnitDomainService<TMeasurable> domainServices = base.DomainServices;
                domainServices.Holder = Holder;

                return domainServices;
            }
        }

    }
}
