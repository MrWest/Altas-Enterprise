using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class PeriodManagerApplicationServices : ItemManagerApplicationServicesBase<IPeriod,IPeriodRepository,IPeriodDomainService>,IPeriodManagerApplicationServices
    {
        private IEntity _holder;

        public IEntity Holder
        {
            get { return _holder; }
            set { _holder = value; }
        }

        public IPeriod GetPeriod(IEntity holder)
        {
            var period = Repository.Entities.FirstOrDefault();
            if (period != null)
                return period;
            period = DomainServices.Create();
           return Repository.Add(period);
        }

        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override IPeriodRepository Repository
        {
            get
            {
                IPeriodRepository repository = base.Repository;
                repository.Holder = Holder;

                return repository;
            }
        }

        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override IPeriodDomainService DomainServices
        {
            get
            {
                IPeriodDomainService domainServices = base.DomainServices;
                domainServices.Holder = Holder;

                return domainServices;
            }
        }

    }
}
