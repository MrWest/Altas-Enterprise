using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the contract <see cref="IExpenseConceptManagerApplicationServices" /> being the application services used
    ///     to manage the CRUD operations coming from upper layers regarding to the ExpenseConcept domain entities.
    /// </summary>
    public class ExpenseConceptManagerApplicationServices :
        ItemManagerApplicationServicesBase<IExpenseConcept, IExpenseConceptRepository, IExpenseConceptDomainServices>,
        IExpenseConceptManagerApplicationServices
    {
        public IExpenseConcept Export(IDatabaseContext databaseContext, IExpenseConcept exportable)
        {
            if (Equals(exportable, null))
                return null;

            var expConvert = Repository.GetClone(exportable);
            //if in the context is nothing found
            if (!databaseContext.GetAll<IExpenseConcept>().Any(x => x.Code == expConvert.Code))
                databaseContext.Add(expConvert);
            else
            {
                expConvert = databaseContext.GetAll<IExpenseConcept>().First(x => x.Code == expConvert.Code);
            }

            return expConvert;
        }
    }
}