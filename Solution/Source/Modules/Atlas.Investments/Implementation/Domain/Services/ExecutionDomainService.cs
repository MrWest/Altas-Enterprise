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
    public class ExecutionDomainService : DomainServicesBase<IExecution>, IExecutionDomainService
    {
        private IExecutedActivity _activity;
        public IExecutedActivity ExecutedActivity { get { return _activity; } set { _activity = value; } }

        /// <summary>
        ///     Creates a new instance of an Execution.
        /// </summary>
        /// <returns>A new instance of type <see cref="IExecution" />.</returns>
        public override IExecution Create()
        {
            IExecution execution = base.Create();
            execution.Date = DateTime.Today;
            execution.Description = Resources.NewExecution;

            return execution;
        }

    }
}
