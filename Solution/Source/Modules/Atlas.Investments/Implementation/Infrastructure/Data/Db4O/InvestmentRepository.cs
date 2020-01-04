using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentRepository" /> representing the repository used to handle the
    ///     data operations of investments.
    /// </summary>
    public class InvestmentRepository : InvestmentElementRepositoryBase<IInvestment>, IInvestmentRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentRepository" /> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        ///     The <see cref="IDb4ODatabaseContext" /> representing the Db4O database context used to carry on with the row data
        ///     operations.
        /// </param>
        public InvestmentRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets the properties of the investment to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.AuthorOrEmitter), GetName(x => x.RelatedPrograms), GetName(x => x.Entity), GetName(x => x.Capacity),
                    GetName(x => x.InducedDoings), GetName(x => x.Osde), GetName(x => x.Oace), GetName(x => x.Phase), GetName(x => x.InvestmentType)
                    , GetName(x => x.Impact), GetName(x => x.Nature)
                }).ToArray();
            }
        }


        /// <summary>
        ///     Deletes the given investment component from the repository.
        /// </summary>
        /// <param name="investment">The <see cref="IInvestmentComponent" /> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investment" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investment" /> has a null budget.</exception>
        public override void Delete(IInvestment investment)
        {
            if (investment == null)
                throw new ArgumentNullException("investment");

            this.Unrelate(investment, investment.Budget, DatabaseContext);

            var investmentComponentRepository = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
            investmentComponentRepository.InvestmentElement = investment;
            investmentComponentRepository.DeleteAll();

            var investmentDocumentRepository = ServiceLocator.Current.GetInstance<IInvestmentDocumentRepository>();
            investmentDocumentRepository.Holder = investment;
            investmentDocumentRepository.DeleteAll();

            var dbInvestment = DatabaseContext.Find<IInvestment>(investment.Id);
            DatabaseContext.Delete(dbInvestment);
            DatabaseContext.Delete((object)dbInvestment.Elements);
        }
    }

    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentRepository" /> representing the repository used to handle the
    ///     data operations of investments.
    /// </summary>
    public class InvestmentRepositoryEF : InvestmentElementRepositoryBaseEF<IInvestment, Investment>, IInvestmentRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentRepository" /> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        ///     The <see cref="IDb4ODatabaseContext" /> representing the Db4O database context used to carry on with the row data
        ///     operations.
        /// </param>
        public InvestmentRepositoryEF(IEntityFrameworkDbContext<Investment> databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets the properties of the investment to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.AuthorOrEmitter), GetName(x => x.RelatedPrograms), GetName(x => x.Entity), GetName(x => x.Capacity),
                    GetName(x => x.InducedDoings), GetName(x => x.Osde), GetName(x => x.Oace), GetName(x => x.Phase), GetName(x => x.InvestmentType)
                    , GetName(x => x.Impact), GetName(x => x.Nature)
                }).ToArray();
            }
        }


        ///// <summary>
        /////     Deletes the given investment component from the repository.
        ///// </summary>
        ///// <param name="investment">The <see cref="IInvestmentComponent" /> to delete.</param>
        ///// <exception cref="ArgumentNullException"><paramref name="investment" /> is null.</exception>
        ///// <exception cref="ArgumentException"><paramref name="investment" /> has a null budget.</exception>
        //public override void Delete(IInvestment investment)
        //{
        //    if (investment == null)
        //        throw new ArgumentNullException("investment");

        //    this.Unrelate(investment, investment.Budget, DatabaseContext);

        //    var investmentComponentRepository = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
        //    investmentComponentRepository.InvestmentElement = investment;
        //    investmentComponentRepository.DeleteAll();

        //    var investmentDocumentRepository = ServiceLocator.Current.GetInstance<IInvestmentDocumentRepository>();
        //    investmentDocumentRepository.Holder = investment;
        //    investmentDocumentRepository.DeleteAll();

        //    var dbInvestment = DatabaseContext.Find<IInvestment>(investment.Id);
        //    DatabaseContext.Delete(dbInvestment);
        //    DatabaseContext.Delete((object)dbInvestment.Elements);
        //}
    }

}