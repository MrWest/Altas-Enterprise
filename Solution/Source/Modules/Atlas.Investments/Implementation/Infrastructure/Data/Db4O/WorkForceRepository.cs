using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkForceRepository" /> used to manage the data operations in WorkForce
    ///     domain entities.
    /// </summary>
    public class WorkForceRepository : Db4ORepositoryBase<IWorkForce>, IWorkForceRepository
    {
        private readonly IWageScaleRepository _wageScaleRepository = ServiceLocator.Current.GetInstance<IWageScaleRepository>();


        /// <summary>
        ///     Initializes a new instance of <see cref="WorkForceRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public WorkForceRepository(IDb4ODatabaseContext databaseContext)
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
                    GetName(x => x.Name), GetName(x => x.Code), GetName(x => x.WageScale)
                }).ToArray();
            }
        }


        ///// <summary>
        /////     Adds a new Work Force to the data source.
        ///// </summary>
        ///// <param name="workForce">The <see cref="IWorkForce" /> to add.</param>
        ///// <exception cref="ArgumentNullException"><paramref name="workForce" /> is null.</exception>
        ///// <returns>The added <see cref="IWorkForce" />.</returns>
        //public override IWorkForce Add(IWorkForce workForce)
        //{
        //    IWorkForce dbWorkForce = base.Add(workForce);

        //    if (workForce.WageScale != null)
        //        this.Relate(workForce, workForce.WageScale, DatabaseContext);

        //    return dbWorkForce;
        //}

        ///// <summary>
        /////     Updates the changes made to the given Work Force in its corresponding item in the currently opened transaction.
        ///// </summary>
        ///// <param name="workForce">
        /////     The item to use its changes and apply them to its current transaction corresponding one.
        ///// </param>
        ///// <exception cref="ArgumentNullException"><paramref name="workForce" /> is null.</exception>
        //public override void Update(IWorkForce workForce)
        //{
        //    base.Update(workForce);

        //    if (workForce.WageScale != null)
        //        this.Relate(workForce, workForce.WageScale, DatabaseContext);
        //}

        /// <summary>
        ///     Gets the method that is used to set the reference, from Work Force to a Wage Scale.
        /// </summary>
        /// <param name="workForce">The <see cref="IWorkForce" /> to relate with <paramref name="wageScale" />.</param>
        /// <param name="wageScale">The entity of the other side of the relationship.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="workForce" /> or <paramref name="wageScale" /> is null.
        /// </exception>
        public void Relate(IWorkForce workForce, IWageScale wageScale)
        {
            if (workForce == null)
                throw new ArgumentNullException("workForce");
            if (wageScale == null)
                throw new ArgumentNullException("wageScale");

            workForce.WageScale = wageScale;
        }

        /// <summary>
        ///     Gets the method that is used to break the reference, from Work Force to a Wage Scale.
        /// </summary>
        /// <param name="workForce">The <see cref="IWorkForce" /> to relate with <paramref name="wageScale" />.</param>
        /// <param name="wageScale">The entity of the other side of the relationship.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="workForce" /> is null.
        /// </exception>
        public void Unrelate(IWorkForce workForce, IWageScale wageScale)
        {
            if (workForce == null)
                throw new ArgumentNullException("workForce");
            if (wageScale == null)
                throw new ArgumentNullException("wageScale");

            workForce.WageScale = null;
        }

        /// <summary>
        ///     Saves the references of given Work Force.
        /// </summary>
        /// <param name="workForce">The <see cref="IWorkForce" /> to save its references.</param>
        public void SaveReference(IWorkForce workForce)
        {
            if (workForce == null)
                throw new ArgumentNullException("workForce");

            DatabaseContext.Store(workForce);
        }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        /// <param name="other">Not used.</param>
        public void SaveReference(IWageScale other)
        {
        }


        /// <summary>
        ///     Clones the given Work Force.
        /// </summary>
        /// <param name="workForce">The <see cref="IWorkForce" /> to clone.</param>
        /// <returns>A clone of <paramref name="workForce" /> with the same value of it in the relevant properties.</returns>
        protected override IWorkForce Clone(IWorkForce workForce)
        {
            IWorkForce clone = base.Clone(workForce);

            //if (workForce.WageScale != null)
            //    clone.WageScale = _wageScaleRepository.Find(workForce.WageScale.Id);

            return clone;
        }
    }
}