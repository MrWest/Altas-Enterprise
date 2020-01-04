using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="ICategoryRepository" /> used to manage the data operations in Category domain
    ///     entities.
    /// </summary>
    public class CategoryRepository : CodedNomenclatorRepositoryBase<ICategory>, ICategoryRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="CategoryRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public CategoryRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }

    /// <summary>
    ///     Implementation of the contract <see cref="ICategoryRepository" /> used to manage the data operations in Category domain
    ///     entities.
    /// </summary>
    public class CategoryRepositoryEF : CodedNomenclatorRepositoryBaseEF<ICategory, Category>, ICategoryRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="CategoryRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public CategoryRepositoryEF(IEntityFrameworkDbContext<Category> databaseContext)
            : base(databaseContext)
        {
        }
    }
}