using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class AtlasModuleMainSubjectRepository : AtlasModuleGenericSubjectRepository<IAtlasModuleMainSubject>, IAtlasModuleMainSubjectRepository
    {
        public AtlasModuleMainSubjectRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public string AssemblyName { get; set; }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.AssemblyName)
                }).ToArray();
            }
        }
        public override IEnumerable<IAtlasModuleMainSubject> Entities
        {
            get
            {
                 var specification = new AtlasModuleMainSubjectOfSpecifcation(AssemblyName);
                 return Where(specification);
            }
        }
        public override void Delete(IAtlasModuleMainSubject entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectRepository>();
            repo.OwnerSubject = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
    }

    public class AtlasModuleMainSubjectRepositoryEF : AtlasModuleGenericSubjectRepositoryEF<IAtlasModuleMainSubject, AtlasModuleMainSubject>, IAtlasModuleMainSubjectRepository
    {
        public AtlasModuleMainSubjectRepositoryEF(IEntityFrameworkDbContext<AtlasModuleMainSubject> databaseContext) : base(databaseContext)
        {
        }

        public string AssemblyName { get; set; }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.AssemblyName)
                }).ToArray();
            }
        }
        public override IEnumerable<IAtlasModuleMainSubject> Entities
        {
            get
            {
                var specification = new AtlasModuleMainSubjectOfQueryable(AssemblyName, DatabaseContext);
                return Where(specification);
            }
        }
        public override void Delete(IAtlasModuleMainSubject entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectRepository>();
            repo.OwnerSubject = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
    }
}
