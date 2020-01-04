using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    /// <summary>
    ///     Base class of the repository used to manage the data operations for coded nomenclators.
    /// </summary>
    /// <typeparam name="TNomenclator">The type of coded nomenclator to manane in the current repository.</typeparam>
    public abstract class CodedNomenclatorRepositoryBase<TNomenclator> :
        Db4ORepositoryBase<TNomenclator>
        where TNomenclator : class, ICodedNomenclator
    {
        /// <summary>
        ///     Initializes a new instance of a <see cref="CodedNomenclatorRepositoryBase{TNomenclator}" /> deriver given a
        ///     database context.
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext" /> is null.</exception>
        protected CodedNomenclatorRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets all the public properties non-readonly properties of the coded nomenclators that are relevant to the current
        ///     repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Description)
                }).ToArray();
            }
        }
    }

    /// <summary>
    ///     Base class of the repository used to manage the data operations for coded nomenclators.
    /// </summary>
    /// <typeparam name="TNomenclator">The type of coded nomenclator to manane in the current repository.</typeparam>
    public abstract class CodedNomenclatorRepositoryBaseEF<TNomenclator, TClass> :
        EntityFrameworkRepositoryBase<TNomenclator, TClass>
        where TNomenclator :class, ICodedNomenclator
         where TClass : NomenclatorBase
    {
        /// <summary>
        ///     Initializes a new instance of a <see cref="CodedNomenclatorRepositoryBase{TNomenclator}" /> deriver given a
        ///     database context.
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext" /> is null.</exception>
        protected CodedNomenclatorRepositoryBaseEF(IEntityFrameworkDbContext<TClass> databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets all the public properties non-readonly properties of the coded nomenclators that are relevant to the current
        ///     repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Description)
                }).ToArray();
            }
        }
    }
}