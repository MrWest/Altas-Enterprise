using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class AtlasModuleSubjectRepository : AtlasModuleGenericSubjectRepository<IAtlasModuleSubject>, IRelatedRepository<IAtlasModuleSubject, IAtlasModuleGenericSubject>, IAtlasModuleSubjectRepository
    {
        public AtlasModuleSubjectRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public override IEnumerable<IAtlasModuleSubject> Entities
        {
            get
            {
                var specification = new AtlasModuleSubjectOfSpecifcation(OwnerSubject);
                return Where(specification);
            }
        }

        public IAtlasModuleGenericSubject OwnerSubject { get; set; }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IAtlasModuleSubject Add(IAtlasModuleSubject budgetComponentItem)
        {
            IAtlasModuleSubject addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, OwnerSubject, DatabaseContext);
           
            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IAtlasModuleSubject budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, OwnerSubject, DatabaseContext);
           
        }
        public override void Delete(IAtlasModuleSubject entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectRepository>();
            repo.OwnerSubject = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
        public void Relate(IAtlasModuleSubject current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            if (current.OwnerSubject != null && current.OwnerSubject.Id != other.Id)
                current.OwnerSubject = other;
            else
            {
                current.OwnerSubject = other;
                //  IList<TItem> itemCollection = other.SubCategories;
                if (other.Subjects.All(x => !Equals(x.Id, current.Id)))
                    other.Subjects.Add(current);
            }

        }

        public void Unrelate(IAtlasModuleSubject current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            if (current.OwnerSubject != null && current.OwnerSubject.Id != other.Id)
                current.OwnerSubject = null;
            else
            {
                current.OwnerSubject = null;

            }
        }

        public void SaveReference(IAtlasModuleSubject current)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IAtlasModuleGenericSubject other)
        {
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            DatabaseContext.Store(other);
        }
    }
    public class AtlasModuleSubjectRepositoryEF : AtlasModuleGenericSubjectRepositoryEF<IAtlasModuleSubject, AtlasModuleSubject>, IRelatedRepository<IAtlasModuleSubject, IAtlasModuleGenericSubject>, IAtlasModuleSubjectRepository
    {
        public AtlasModuleSubjectRepositoryEF(IEntityFrameworkDbContext<AtlasModuleSubject> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.OwnerSubjectId)
                }).ToArray();
            }
        }

        public override IEnumerable<IAtlasModuleSubject> Entities
        {
            get
            {
                var queryable = new AtlasModuleSubjectOfQueryable(OwnerSubject,DatabaseContext);
                return Where(queryable);
            }
        }

        public IAtlasModuleGenericSubject OwnerSubject { get; set; }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IAtlasModuleSubject Add(IAtlasModuleSubject budgetComponentItem)
        {
            Relate(budgetComponentItem, OwnerSubject);
            IAtlasModuleSubject addedBudgetComponentITem = base.Add(budgetComponentItem);

           

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IAtlasModuleSubject budgetComponentItem)
        {
            Relate(budgetComponentItem, OwnerSubject );
            base.Update(budgetComponentItem);

            

        }
        public override void Delete(IAtlasModuleSubject entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectRepository>();
            repo.OwnerSubject = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
        public void Relate(IAtlasModuleSubject current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            if (current.OwnerSubject != null && current.OwnerSubject.Id != other.Id)
                current.OwnerSubject = other;
            else
            {
                current.OwnerSubject = other;
                current.OwnerSubjectId = other.Id;
                //  IList<TItem> itemCollection = other.SubCategories;
                if (other.Subjects.All(x => !Equals(x.Id, current.Id)))
                    other.Subjects.Add(current);
            }

        }

        public void Unrelate(IAtlasModuleSubject current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

         

            current.OwnerSubject = null;
            current.OwnerSubjectId = null;
        }

        public void SaveReference(IAtlasModuleSubject current)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");

            DatabaseContext.Save();
        }

        public void SaveReference(IAtlasModuleGenericSubject other)
        {
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            DatabaseContext.Save();
        }
    }

}
