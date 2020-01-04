using System;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IExpenseConceptDomainServices" /> used to enforce the business rules in
    ///     Expense Concept domain entities.
    /// </summary>
    public class ExpenseConceptDomainServices : CodedNomenclatorDomainServicesBase<IExpenseConcept>, IExpenseConceptDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an ExpenseConcept.
        /// </summary>
        /// <returns>A new instance of type <see cref="IExpenseConcept" />.</returns>
        public override IExpenseConcept Create()
        {
            IExpenseConcept phase = base.Create();
            phase.Name = Resources.NewExpenseConcept_Name;
           // phase.Code = Guid.NewGuid().ToString();

            return phase;
        }
    }
}