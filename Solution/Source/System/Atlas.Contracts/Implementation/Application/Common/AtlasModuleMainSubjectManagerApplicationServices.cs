using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class AtlasModuleMainSubjectManagerApplicationServices : AtlasModuleGenericSubjectManagerApplicationServices<IAtlasModuleMainSubject,IAtlasModuleMainSubjectRepository,IAtlasModuleMainSubjectDomainServices> ,IAtlasModuleMainSubjectManagerApplicationServices
    {
        public string AssemblyName { get; set; }
        protected override IAtlasModuleMainSubjectRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.AssemblyName = AssemblyName;
                return repo;
            }
        }
        protected override IAtlasModuleMainSubjectDomainServices DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.AssemblyName = AssemblyName;
                return domain;
            }
        }

        public override IAtlasModuleMainSubject Export(IDatabaseContext databaseContext, IAtlasModuleMainSubject exportable)
        {
            if (databaseContext == null)
                return null;
            //  var defaultDatabaseContext = ServiceLocator.Current.GetInstance<IDatabaseContext>();


            var subject = SaveSubject(databaseContext, exportable);


            databaseContext.Save();
            return subject;
        }

        private IAtlasModuleMainSubject SaveSubject(IDatabaseContext databaseContext, IAtlasModuleMainSubject exportable)
        {

         

            var newSubject = Repository.GetClone(exportable);
            databaseContext.Add(newSubject);

            ContentFill(databaseContext, exportable, newSubject);

            // recursive Fill
            var subjectRepo = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectRepository>();
            subjectRepo.OwnerSubject = exportable;

            var subjectService = ServiceLocator.Current.GetInstance<IAtlasModuleSubjectManagerApplicationServices>();
            subjectService.OwnerSubject = newSubject;

            foreach (IAtlasModuleSubject atlasModuleSubject in subjectRepo.Entities)
            {
                subjectService.Export(databaseContext, atlasModuleSubject);
            }

            return newSubject;

        }
    }
}
