using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class AtlasModuleSubjectManagerApplicationServices : AtlasModuleGenericSubjectManagerApplicationServices<IAtlasModuleSubject, IAtlasModuleSubjectRepository, IAtlasModuleSubjectDomainServices>, IAtlasModuleSubjectManagerApplicationServices
    {
        public IAtlasModuleGenericSubject OwnerSubject { get; set; }

        protected override IAtlasModuleSubjectRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.OwnerSubject = OwnerSubject;
                return repo;
            }
        }
        protected override IAtlasModuleSubjectDomainServices DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.OwnerSubject = OwnerSubject;
                return domain;
            }
        }

        public override IAtlasModuleSubject Export(IDatabaseContext databaseContext, IAtlasModuleSubject exportable)
        {
            if (databaseContext == null)
                return null;
            //  var defaultDatabaseContext = ServiceLocator.Current.GetInstance<IDatabaseContext>();


            var subject = SaveSubject(databaseContext, exportable);


            databaseContext.Save();
            return subject;

           

        }

        private IAtlasModuleSubject SaveSubject(IDatabaseContext databaseContext, IAtlasModuleSubject exportable)
        {
           

            var newSubject = Repository.GetClone(exportable);
            newSubject.OwnerSubject = OwnerSubject;

            databaseContext.Add(newSubject);

            ContentFill(databaseContext, exportable, newSubject);

            // recursive Fill


            var subjectService = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectManagerApplicationServices>();
            subjectService.OwnerSubject = newSubject;

            var subjectRepo = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectRepository>();
            subjectRepo.OwnerSubject = exportable;

            foreach (IAtlasModuleSubject atlasModuleSubject in subjectRepo.Entities)
            {
                subjectService.Export(databaseContext, atlasModuleSubject);
            }

            return newSubject;
        }
    }
}
