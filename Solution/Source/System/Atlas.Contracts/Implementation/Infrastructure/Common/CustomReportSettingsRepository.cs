using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    
    public abstract class CustomReportSettingsRepository<TSettings>: Db4ORepositoryBase<TSettings>,ICustomReportSettingsRepository<TSettings>
         where TSettings : class, ICustomReportSettings
    {
        public CustomReportSettingsRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.Id), GetName(x => x.Name), GetName(x => x.Description) }; }
        }


    }

    public abstract class ChildCustomReportSettingsRepository<TSettings> : CustomReportSettingsRepository<TSettings>, IRelatedRepository<TSettings, ICustomReportSettings>, IChildCustomReportSettingsRepository<TSettings>
        where TSettings : class, IChildCustomReportSettings
    {
        public ChildCustomReportSettingsRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public ICustomReportSettings Parent { get; set; }

        public override IEnumerable<TSettings> Entities
        {
            get
            {
                var specification = new ChildCustomReportSettingsofSpecification<TSettings>(Parent);

                return Where(specification);
            }
        }


        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TSettings Add(TSettings budgetComponentItem)
        {
            TSettings addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, Parent, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(TSettings budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, Parent, DatabaseContext);

        }



        public void Relate(TSettings current, ICustomReportSettings other)
        {
            if (current == null)
                throw new ArgumentNullException("TSettings");
            if (other == null)
                throw new ArgumentNullException("ICustomReportSettings");

            current.Parent = other;
        }

        public void Unrelate(TSettings current, ICustomReportSettings other)
        {
            if (current == null)
                throw new ArgumentNullException("TSettings");
            if (other == null)
                throw new ArgumentNullException("ICustomReportSettings");

            current.Parent = null;
        }

        public void SaveReference(TSettings current)
        {

            if (current == null)
                throw new ArgumentNullException("TSettings");

            DatabaseContext.Store(current);
        }

        public void SaveReference(ICustomReportSettings other)
        {

            if (other == null)
                throw new ArgumentNullException("ICustomReportSettings");

            DatabaseContext.Store(other);
        }
    }

    public abstract class MainCustomReportSettingsRepository<TSettings> : CustomReportSettingsRepository<TSettings>, IMainCustomReportSettingsRepository<TSettings>
      where TSettings : class, IMainCustomReportSettings
    {
        public MainCustomReportSettingsRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

      
    }

    public abstract class CustomReportSettingsRepositoryEF<TSettings, TClass> : EntityFrameworkRepositoryBase<TSettings, TClass>, ICustomReportSettingsRepository<TSettings>
        where TSettings : class, ICustomReportSettings
         where TClass : CustomReportSettings
    {
        public CustomReportSettingsRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.Id), GetName(x => x.Name), GetName(x => x.Description) }; }
        }


    }

    public abstract class ChildCustomReportSettingsRepositoryEF<TSettings, TClass> : CustomReportSettingsRepositoryEF<TSettings,TClass>, IRelatedRepository<TSettings, ICustomReportSettings>, IChildCustomReportSettingsRepository<TSettings>
        where TSettings : class, IChildCustomReportSettings
         where TClass : ChildCustomReportSettings
    {
        public ChildCustomReportSettingsRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.ParentId) }; }
        }

        public ICustomReportSettings Parent { get; set; }

        public override IEnumerable<TSettings> Entities
        {
            get
            {
                var specification = new ChildCustomReportSettingsOfQueryable<TClass>(Parent, DatabaseContext);

                return Where(specification);
            }
        }


        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TSettings Add(TSettings budgetComponentItem)
        {

            this.Relate(budgetComponentItem, Parent);
            TSettings addedBudgetComponentITem = base.Add(budgetComponentItem);


            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(TSettings budgetComponentItem)
        {
            this.Relate(budgetComponentItem, Parent);
            base.Update(budgetComponentItem);

          

        }



        public void Relate(TSettings current, ICustomReportSettings other)
        {
            if (current == null)
                throw new ArgumentNullException("TSettings");
            if (other == null)
                throw new ArgumentNullException("ICustomReportSettings");

            current.Parent = other;
            current.ParentId = other.Id;

        }

        public void Unrelate(TSettings current, ICustomReportSettings other)
        {
            if (current == null)
                throw new ArgumentNullException("TSettings");
            if (other == null)
                throw new ArgumentNullException("ICustomReportSettings");

            current.Parent = null;
            current.ParentId = null;
        }

        public void SaveReference(TSettings current)
        {

            if (current == null)
                throw new ArgumentNullException("TSettings");

            DatabaseContext.Save();
        }

        public void SaveReference(ICustomReportSettings other)
        {

            if (other == null)
                throw new ArgumentNullException("ICustomReportSettings");

            DatabaseContext.Save();
        }
    }

    public abstract class MainCustomReportSettingsRepositoryEF<TSettings, TClass> : CustomReportSettingsRepositoryEF<TSettings, TClass>, IMainCustomReportSettingsRepository<TSettings>
      where TSettings : class, IMainCustomReportSettings
        where TClass : MainCustomReportSettings
    {
        public MainCustomReportSettingsRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }


    }

}