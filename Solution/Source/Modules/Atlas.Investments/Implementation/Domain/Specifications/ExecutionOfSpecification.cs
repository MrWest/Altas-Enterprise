using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    public class ExecutionOfSpecification : Specification<IExecution>
    {
          /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public ExecutionOfSpecification(IExecutedActivity activity)
        {
            if (activity == null)
                throw new ArgumentNullException("activity");

            Predicate = execution => execution.ExecutedActivity != null && Equals(execution.ExecutedActivity.Id, activity.Id);
        }
    }

    public class ExecutionOfQueryable : EntityFrameworkQueryable<Execution>

    {

        public ExecutionOfQueryable(IExecutedActivity activity, IEntityFrameworkDbContext<Execution> context) : base(context)
        {

            if (activity == null)
                throw new ArgumentNullException("activity");
            Query = (from e in context.Entities orderby e.Id ascending where e.ExecutedActivityId == activity.Id select e);
        }
    }
}

    