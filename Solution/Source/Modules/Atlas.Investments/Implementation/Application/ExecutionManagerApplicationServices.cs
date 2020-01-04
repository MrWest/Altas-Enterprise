using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class ExecutionManagerApplicationServices : ItemManagerApplicationServicesBase<IExecution, IExecutionRepository, IExecutionDomainService>, IExecutionManagerApplicationServices
    {
        private IExecutedActivity _activity;
        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override IExecutionRepository Repository
        {
            get
            {
                IExecutionRepository repository = base.Repository;
                repository.ExecutedActivity = ExecutedActivity;

                return repository;
            }
        }

        /// <summary>
        /// Gets or sets the Section to which belong the section managed here.
        /// </summary>
        public IExecutedActivity ExecutedActivity
        {
            get
            {
                if (_activity == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _activity;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _activity = value;
            }

        }

        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override IExecutionDomainService DomainServices
        {
            get
            {
                IExecutionDomainService domainServices = base.DomainServices;
                domainServices.ExecutedActivity = ExecutedActivity;

                return domainServices;
            }
        }

    }
}
