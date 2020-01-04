using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class AtlasUserRepository : Db4ORepositoryBase<IAtlasUser>, IAtlasUserRepository
    {
        public AtlasUserRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Rol),
                    GetName(x => x.AllowedModules),GetName(x=>x.Password)
                }).ToArray();
            }
        }
    }


    public class AtlasUserRepositoryEF : EntityFrameworkRepositoryBase<IAtlasUser, AtlasUser>, IAtlasUserRepository
    {
        public AtlasUserRepositoryEF(IEntityFrameworkDbContext<AtlasUser> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Rol),
                    GetName(x => x.AllowedModules),GetName(x=>x.Password)
                }).ToArray();
            }
        }

        public override void Delete(IAtlasUser entity)
        {
            

            var moduleInfoRepo = ServiceLocator.Current.GetInstance<IAtlasModuleInfoRepository>();
            moduleInfoRepo.AtlasUser = entity;

            moduleInfoRepo.DeleteAll();

            base.Delete(entity);
        }

        protected override IAtlasUser Clone(IAtlasUser atlasUser)
        {
            if (atlasUser == null)
                throw new ArgumentNullException("investmentElement");

            IAtlasUser atlasUserClone = base.Clone(atlasUser);

         
         

            //atlasUserClone.Period = Clone(atlasUser.Period);
            //atlasUserClone.Period.Holder = atlasUserClone;
            //atlasUserClone.PeriodId = atlasUserClone.Period.Id;

            return atlasUserClone;
        }

        //private IPeriod Clone(IPeriod period)
        //{
        //    var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();

        //    periodClone.Starts = period.OriStart();
        //    periodClone.Ends = period.OriEnd();
        //    periodClone.PeriodKind = period.PeriodKind;

        //    periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

        //    return periodClone;
        //}

    }


    public class AtlasModuleInfoRepository : EntityFrameworkRepositoryBase<IAtlasModuleInfo, AtlasModuleInfo>, IAtlasModuleInfoRepository
    {
        public AtlasModuleInfoRepository(IEntityFrameworkDbContext<AtlasModuleInfo> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.InitializationMode), GetName(x => x.ModuleName), GetName(x => x.ModuleType),
                    GetName(x => x.Ref),GetName(x=>x.State),GetName(x=>x.AtlasUserId)
                }).ToArray();
            }
        }

        public IAtlasUser AtlasUser { get; set; }

        public override IEnumerable<IAtlasModuleInfo> Entities
        {
            get
            {
                var queryable = new AtlasModuleInfoOfQueryable(AtlasUser, DatabaseContext);
                return Where(queryable);
            }
        }

        
    }
}
