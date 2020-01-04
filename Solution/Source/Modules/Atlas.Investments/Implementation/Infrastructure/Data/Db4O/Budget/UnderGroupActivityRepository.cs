using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    public class UnderGroupActivityRepository: UnderGroupItemRepository<IUnderGroupActivity>, IUnderGroupActivityRepository,
        IRelatedRepository<IUnderGroupActivity, IPeriod>
    {
        public UnderGroupActivityRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
               {
                    GetName(x=>x.SubSpeciality)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        public override IUnderGroupActivity Add(IUnderGroupActivity entity)
        {
            IUnderGroupActivity item = base.Add(entity);

            entity.Period = item.Period;



            this.Relate(entity, entity.Period, DatabaseContext);

            //this.Relate(entity, UnderGroup, DatabaseContext);

            return entity;

        }

   

        protected override IUnderGroupActivity Clone(IUnderGroupActivity plannedActivity)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("IUnderGroupActivity");

            IUnderGroupActivity plannedActivityClone = base.Clone(plannedActivity);

            if (plannedActivity.Period == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

            plannedActivityClone.Period = Clone(plannedActivity.Period);
            plannedActivityClone.Period.Holder = plannedActivityClone;


            return plannedActivityClone;
        }


        private IPeriod Clone(IPeriod period)
        {
            var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();

            periodClone.Starts = period.Starts;
            periodClone.Ends = period.Ends;
            periodClone.PeriodKind = period.PeriodKind;

            periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

            return periodClone;
        }
        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(IUnderGroupActivity plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("period");

            plannedActivity.Period = period;
            period.Holder = plannedActivity;
        }

        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="period" />.
        /// </param>
        /// <param name="period">The <see cref="IPeriod" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="period" /> is null.
        /// </exception>
        public void Unrelate(IUnderGroupActivity plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("budget");

            //plannedActivity.Period = null;
            //period.Holder = null;
            var periodRepository = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            periodRepository.Holder = plannedActivity;

            periodRepository.DeleteAll();

        }

        public void SaveReference(IPeriod other)
        {

        }
    }

    public class UnderGroupActivityRepositoryEF : UnderGroupItemRepositoryEF<IUnderGroupActivity,UnderGroupActivity>, IUnderGroupActivityRepository,
          IRelatedRepository<IUnderGroupActivity, IPeriod>
    {
        public UnderGroupActivityRepositoryEF(IEntityFrameworkDbContext<UnderGroupActivity> databaseContext) : base(databaseContext)
        {
        }
        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
               {
                    GetName(x=>x.SubSpeciality)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        public override IUnderGroupActivity Add(IUnderGroupActivity entity)
        {
            this.Relate(entity, entity.Period);
            IUnderGroupActivity item = base.Add(entity);

            entity.Period = item.Period;



          

            //this.Relate(entity, UnderGroup, DatabaseContext);

            return entity;

        }



        protected override IUnderGroupActivity Clone(IUnderGroupActivity plannedActivity)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("IUnderGroupActivity");

            IUnderGroupActivity plannedActivityClone = base.Clone(plannedActivity);

            if (plannedActivity.Period == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

            plannedActivityClone.Period = Clone(plannedActivity.Period);
            plannedActivityClone.Period.Holder = plannedActivityClone;


            return plannedActivityClone;
        }


        private IPeriod Clone(IPeriod period)
        {
            var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();

            periodClone.Starts = period.Starts;
            periodClone.Ends = period.Ends;
            periodClone.PeriodKind = period.PeriodKind;

            periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

            return periodClone;
        }
        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(IUnderGroupActivity plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("period");

            plannedActivity.Period = period;
            period.Holder = plannedActivity;
        }

        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="period" />.
        /// </param>
        /// <param name="period">The <see cref="IPeriod" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="period" /> is null.
        /// </exception>
        public void Unrelate(IUnderGroupActivity plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("budget");

            //plannedActivity.Period = null;
            //period.Holder = null;
            var periodRepository = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            periodRepository.Holder = plannedActivity;

            periodRepository.DeleteAll();

        }

        public void SaveReference(IPeriod other)
        {
            DatabaseContext.Save();
        }
    }

}