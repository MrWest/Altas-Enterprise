using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IExpenseConceptRepository" /> used to manage the data operations in
    ///     Expense Concept domain entities.
    /// </summary>
    public class ExpenseConceptRepository : Db4ORepositoryBase<IExpenseConcept>, IExpenseConceptRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ExpenseConceptRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public ExpenseConceptRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets all the public properties non-readonly properties that are relevant to the current repository when making its
        ///     operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Code), GetName(x => x.Type)
                }).ToArray();
            }
        }
    }

    /// <summary>
    ///     Implementation of the contract <see cref="IExpenseConceptRepository" /> used to manage the data operations in
    ///     Expense Concept domain entities.
    /// </summary>
    public class ExpenseConceptRepositoryEF : CodedNomenclatorRepositoryBaseEF<IExpenseConcept, ExpenseConcept>, IExpenseConceptRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ExpenseConceptRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public ExpenseConceptRepositoryEF(IEntityFrameworkDbContext<ExpenseConcept> databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets all the public properties non-readonly properties that are relevant to the current repository when making its
        ///     operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Type)
                }).ToArray();
            }
        }
    }
}