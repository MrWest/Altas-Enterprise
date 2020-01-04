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
    public class MeasurableUnitRepository<TMeasurable> : Db4ORepositoryBase<TMeasurable>, IMeasurableUnitRepository<TMeasurable>//, IRelatedRepository<TMeasurable, IEntity>
         where TMeasurable : class ,IMeasurableUnit
    {
        public MeasurableUnitRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        /////     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        /////     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        ///// </summary>
        //public override IEnumerable<TMeasurable> Entities
        //{
        //    get
        //    {
        //        var specification = new MeasurableOfSpecification<TMeasurable>(Holder);

        //        return Where(specification);
        //    }
        //}
        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get { return new[] {  GetName(x => x.MeasurementUnit), GetName(x => x.Amount) }; }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TMeasurable Add(TMeasurable measurable)
        {
            TMeasurable addedSection = base.Add(measurable);

          //  this.Relate(measurable, Holder, DatabaseContext);

            return measurable;
        }
        /// <summary>
        /// Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> does not have an <see cref="IBudget"/>.</exception>
        public override void Update(TMeasurable measurable)
        {
            if (measurable == null)
                throw new ArgumentNullException("TMeasurable");

            base.Update(measurable);
          //  this.Relate(measurable, Holder, DatabaseContext);

        }

      //  private IEntity _holder;
      //  public IEntity Holder { get { return _holder; } set { _holder = value; } }

        //public void Relate(TMeasurable current, IEntity other)
        //{
        //    if (current == null)
        //        throw new ArgumentNullException("TMeasurable");
        //    if (other == null)
        //        throw new ArgumentNullException("other in TMeasurable");
        //    current.Holder = other;
        //}

        //public void Unrelate(TMeasurable current, IEntity other)
        //{
        //    if (current == null)
        //        throw new ArgumentNullException("TMeasurable");
        //    if (other == null)
        //        throw new ArgumentNullException("other in TMeasurable");
        //    current.Holder = other;
        //}

        //public void SaveReference(TMeasurable current)
        //{
        //    if (current == null)
        //        throw new ArgumentNullException("TMeasurable");

        //    DatabaseContext.Store(current);
        //}

        //public void SaveReference(IEntity other)
        //{
        //   // throw new NotImplementedException();
        //}
    }


    public class MeasurableUnitRepositoryEF<TMeasurable, TClass> : EntityFrameworkRepositoryBase<TMeasurable,TClass>, IMeasurableUnitRepository<TMeasurable>//, IRelatedRepository<TMeasurable, IEntity>
        where TMeasurable : class, IMeasurableUnit
        where TClass: MeasurableUnit
    {
        public MeasurableUnitRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        
        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.MeasurementUnit), GetName(x => x.Amount) }; }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TMeasurable Add(TMeasurable measurable)
        {
            TMeasurable addedSection = base.Add(measurable);

            //  this.Relate(measurable, Holder, DatabaseContext);

            return measurable;
        }
        /// <summary>
        /// Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> does not have an <see cref="IBudget"/>.</exception>
        public override void Update(TMeasurable measurable)
        {
            if (measurable == null)
                throw new ArgumentNullException("TMeasurable");

            base.Update(measurable);
            //  this.Relate(measurable, Holder, DatabaseContext);

        }

        
    }
}
