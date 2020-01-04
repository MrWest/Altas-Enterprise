using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentTypeRepository" /> used to manage the data operations in
    ///     Investment Type domain entities.
    /// </summary>
    public class InvestmentTypeRepository : CodedNomenclatorRepositoryBase<IInvestmentType>, IInvestmentTypeRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentTypeRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public InvestmentTypeRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}