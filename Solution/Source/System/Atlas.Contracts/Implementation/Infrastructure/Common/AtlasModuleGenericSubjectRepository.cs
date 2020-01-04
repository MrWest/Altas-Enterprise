using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public abstract class AtlasModuleGenericSubjectRepository<TEntity> : Db4ORepositoryBase<TEntity>, IAtlasModuleGenericSubjectRepository<TEntity> 
        where TEntity : class,IAtlasModuleGenericSubject
    {
        public AtlasModuleGenericSubjectRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Content)
                }).ToArray();
            }
        }

        public override void Delete(TEntity entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IReferenceDocumentRepository>();
            repo.Holder = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
    }

    public abstract class AtlasModuleGenericSubjectRepositoryEF<TEntity, TClass> : EntityFrameworkRepositoryBase<TEntity,TClass>, IAtlasModuleGenericSubjectRepository<TEntity>
        where TEntity : class, IAtlasModuleGenericSubject
        where TClass: AtlasModuleGenericSubject
    {
        public AtlasModuleGenericSubjectRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }



        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Content)
                }).ToArray();
            }
        }

        public override void Delete(TEntity entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IReferenceDocumentRepository>();
            repo.Holder = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
    }
}
