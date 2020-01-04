using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class PeriodRepositorye : Db4ORepositoryBase<IPeriod>, IRelatedRepository<IPeriod, IEntity>, IPeriodRepository
       
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PeriodRepository"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        /// The <see cref="IDb4ODatabaseContext"/> representing the Db4O database context used to carry on with the row data
        /// operations.
        /// </param>
        public PeriodRepositorye(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<IPeriod> Entities
        {
            get
            {
                var specification = new PeriodOfSpecification(Holder);

                return Where(specification);
            }
        }
        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override  string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.Id), GetName(x => x.Starts), GetName(x => x.Ends), GetName(x => x.PeriodKind) }; }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IPeriod Add(IPeriod period)
        {
            IPeriod addedSection = base.Add(period);

            this.Relate(period, Holder, DatabaseContext);

            return period;
        }
        /// <summary>
        /// Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> does not have an <see cref="IBudget"/>.</exception>
        public override void Update(IPeriod period)
        {
            if (period == null)
                throw new ArgumentNullException("Period");

            this.Relate(period, Holder, DatabaseContext);
            base.Update(period);

            
        }

        private IEntity _holder;
        public IEntity Holder { get { return _holder; } set { _holder = value; } }


        public void Relate(IPeriod current, IEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("Period");
            if (other == null)
                throw new ArgumentNullException("other in PeriodRepository");
            current.Holder = other;
        }

        public void Unrelate(IPeriod current, IEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("Period");
            if (other == null)
                throw new ArgumentNullException("other in PeriodRepository");
            current.Holder = null;
        }

        public void SaveReference(IPeriod period)
        {
            if (period == null)
                throw new ArgumentNullException("period");

            DatabaseContext.Store(period);
        }

        public void SaveReference(IEntity other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Store(other);
        }
    }

    public class PeriodRepositoryEF : EntityFrameworkRepositoryBase<IPeriod, Period>, IRelatedRepository<IPeriod, IEntity>, IPeriodRepository

    {
        /// <summary>
        /// Initializes a new instance of <see cref="PeriodRepository"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        /// The <see cref="IDb4ODatabaseContext"/> representing the Db4O database context used to carry on with the row data
        /// operations.
        /// </param>
        public PeriodRepositoryEF(IEntityFrameworkDbContext<Period> databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<IPeriod> Entities
        {
            get
            {
                var specification = new PeriodOfQueryable(Holder, DatabaseContext);

                return Where(specification);
            }
        }
        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.Id), GetName(x => x.Starts), GetName(x => x.Ends), GetName(x => x.PeriodKind), GetName(x => x.HolderId) }; }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IPeriod Add(IPeriod period)
        {
            this.Relate(period, Holder);
            IPeriod addedSection = base.Add(period);

            this.Relate(period, Holder);

            return period;
        }
        /// <summary>
        /// Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> does not have an <see cref="IBudget"/>.</exception>
        public override void Update(IPeriod period)
        {
            if (period == null)
                throw new ArgumentNullException("Period");

            this.Relate(period, Holder);
            base.Update(period);


        }

        private IEntity _holder;
        public IEntity Holder { get { return _holder; } set { _holder = value; } }


        public void Relate(IPeriod current, IEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("Period");
            if (other == null)
                throw new ArgumentNullException("other in PeriodRepository");
            current.Holder = other;
            current.HolderId = other.Id;
        }

        public void Unrelate(IPeriod current, IEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("Period");
            if (other == null)
                throw new ArgumentNullException("other in PeriodRepository");
            current.Holder = null;
            current.HolderId = null;
        }

        public void SaveReference(IPeriod period)
        {
            if (period == null)
                throw new ArgumentNullException("period");

            DatabaseContext.Save();
        }

        public void SaveReference(IEntity other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Save();
        }
    }

}
