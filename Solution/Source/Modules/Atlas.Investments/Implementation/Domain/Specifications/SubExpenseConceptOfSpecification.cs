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
    public class SubExpenseConceptOfSpecification : Specification<ISubExpenseConcept>
    {
         /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public SubExpenseConceptOfSpecification(IExpenseConcept expenseConcept)
        {
            if (expenseConcept == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = sub => sub.ExpenseConcept != null && Equals(sub.ExpenseConcept.Id, expenseConcept.Id);
        }
    }

    public class SubExpenseConceptOfQueryable : EntityFrameworkQueryable<SubExpenseConcept>

    {

        public SubExpenseConceptOfQueryable(IExpenseConcept expenseConcept, IEntityFrameworkDbContext<SubExpenseConcept> context) : base(context)
        {

            if (expenseConcept == null)
                throw new ArgumentNullException("expenseConcept");
            Query = (from e in context.Entities orderby e.Id ascending where e.ExpenseConceptId == expenseConcept.Id select e);
        }
    }
}
