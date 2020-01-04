using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class AtlasMainModuleAccessRepository: AtlasGenericModuleAccessRepository<IAtlasMainModuleAccess>, IAtlasMainModuleAccessRepository
    {
        public AtlasMainModuleAccessRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

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
      
        
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IAtlasMainModuleAccess> Entities
        {
            get
            {

                ISpecification<IAtlasMainModuleAccess> specification = new AtlasMainModuleAccessOfSpecifcation(AssemblyName);
             //   var shit = base.Entities;
                return Where(specification);
            }
        }

       
        public string AssemblyName { get; set; }

        public override void Delete(IAtlasMainModuleAccess entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IAtlasModuleAccessRepository>();
            repo.OwnerModuleAccess = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
    }
}
