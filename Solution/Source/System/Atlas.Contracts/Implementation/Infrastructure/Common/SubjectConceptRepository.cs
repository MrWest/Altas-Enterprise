using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
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
    public class SubjectConceptRepository : CodedNomenclatorRepositoryBase<ISubjectConcept>, IRelatedRepository<ISubjectConcept, IAtlasModuleGenericSubject>, ISubjectConceptRepository
    {
        public SubjectConceptRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.Code), GetName(x => x.Name)
                }).ToArray();
            }
        }
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ISubjectConcept> Entities
        {
            get
            {

                ISpecification<ISubjectConcept> specification = new SubjectConceptOfSpecifcation(ModuleSubject);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override ISubjectConcept Add(ISubjectConcept budgetComponentItem)
        {
            ISubjectConcept addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, ModuleSubject , DatabaseContext);
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(ISubjectConcept budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, ModuleSubject, DatabaseContext);

        }

        public override void Delete(ISubjectConcept entity)
        {
            var repo = ServiceLocator.Current.GetInstance<ISubjectConceptDefinitionRepository>();
            repo.SubjectConcept = entity;
            repo.DeleteAll();
            var repo2 = ServiceLocator.Current.GetInstance<ISubjectConceptExampleRepository>();
            repo2.SubjectConcept = entity;
            repo2.DeleteAll();
            var repo3 = ServiceLocator.Current.GetInstance<IRelatedConceptRepository>();
            repo3.OwnerSubjectConcept = entity;
            repo3.DeleteAll();
            base.Delete(entity);
        }

        public void Relate(ISubjectConcept current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ModuleSubject = other;
           
           
        }

        public void Unrelate(ISubjectConcept current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ModuleSubject = null;
        }

        public void SaveReference(ISubjectConcept current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IAtlasModuleGenericSubject other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Store(other);
        }

        public IAtlasModuleGenericSubject ModuleSubject { get; set; }
    }


    public class SubjectConceptRepositoryEF : CodedNomenclatorRepositoryBaseEF<ISubjectConcept, SubjectConcept>, IRelatedRepository<ISubjectConcept, IAtlasModuleGenericSubject>, ISubjectConceptRepository
    {
        public SubjectConceptRepositoryEF(IEntityFrameworkDbContext<SubjectConcept> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.Code), GetName(x => x.Name),  GetName(x => x.ModuleSubjectId)
                }).ToArray();
            }
        }
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ISubjectConcept> Entities
        {
            get
            {

                var queryable = new SubjectConceptOfQueryable(ModuleSubject, DatabaseContext);

                return Where(queryable);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override ISubjectConcept Add(ISubjectConcept budgetComponentItem)
        {
            this.Relate(budgetComponentItem, ModuleSubject);
            ISubjectConcept addedBudgetComponentITem = base.Add(budgetComponentItem);


            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(ISubjectConcept budgetComponentItem)
        {
            this.Relate(budgetComponentItem, ModuleSubject);
            base.Update(budgetComponentItem);



        }

        public override void Delete(ISubjectConcept entity)
        {  
            var repo = ServiceLocator.Current.GetInstance<ISubjectConceptDefinitionRepository>();
            repo.SubjectConcept = entity;
            repo.DeleteAll();
            var repo2 = ServiceLocator.Current.GetInstance<ISubjectConceptExampleRepository>();
            repo2.SubjectConcept = entity;
            repo2.DeleteAll();
            var repo3 = ServiceLocator.Current.GetInstance<IRelatedConceptRepository>();
            repo3.OwnerSubjectConcept = entity;
            repo3.DeleteAll();
            base.Delete(entity);
        }

        public void Relate(ISubjectConcept current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ModuleSubject = other;
            current.ModuleSubjectId = other.Id;




        }

        public void Unrelate(ISubjectConcept current, IAtlasModuleGenericSubject other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ModuleSubject = null;
            current.ModuleSubjectId = null;
        }

        public void SaveReference(ISubjectConcept current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(IAtlasModuleGenericSubject other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Save();
        }

        public IAtlasModuleGenericSubject ModuleSubject { get; set; }
    }
}
