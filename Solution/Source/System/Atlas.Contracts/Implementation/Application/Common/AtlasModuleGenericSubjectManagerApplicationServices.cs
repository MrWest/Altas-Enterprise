using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public abstract class AtlasModuleGenericSubjectManagerApplicationServices<TEntity, TRepository, TService> : ItemManagerApplicationServicesBase<TEntity, TRepository, TService>, IAtlasModuleGenericSubjectManagerApplicationServices<TEntity>
        where TEntity : class,IAtlasModuleGenericSubject
        where TRepository : class,IAtlasModuleGenericSubjectRepository<TEntity>
        where TService : class,IAtlasModuleGenericSubjectDomainServices<TEntity>
    {
        public abstract TEntity Export(IDatabaseContext databaseContext, TEntity exportable);




        protected static void ContentFill(IDatabaseContext databaseContext, TEntity exportable, TEntity newSubject)
        {
            //export Content

            var conceptRepo = ServiceLocator.Current.GetInstance<ISubjectConceptRepository>();
            conceptRepo.ModuleSubject = exportable;

            foreach (ISubjectConcept subjectConcept in conceptRepo.Entities)
            {
                var newSubjectConcept = conceptRepo.GetClone(subjectConcept);
                newSubjectConcept.ModuleSubject = newSubject;
                databaseContext.Add(newSubjectConcept);

                //try to unmiss this
                var conceptService = ServiceLocator.Current.GetInstance<ISubjectConceptManagerApplicationServices>();


                if (!Equals(subjectConcept, null))
                {
                    ISpecification<IRelatedConcept> specification = new Specification<IRelatedConcept>(x => conceptService.Find(x.SubjectConcept) != null &&
                    conceptService.Find(x.SubjectConcept).Code == newSubjectConcept.Code
                    && conceptService.Find(x.SubjectConcept).Name == newSubjectConcept.Name);
                    var result = databaseContext.Where(specification);
                    if (result.Any())
                    {

                        foreach (IRelatedConcept relatedConcept in result)
                        {
                            relatedConcept.SubjectConcept = newSubjectConcept.Id;
                            databaseContext.Update(relatedConcept);
                        }
                    }
                }


                var definitionRepo = ServiceLocator.Current.GetInstance<ISubjectConceptDefinitionRepository>();
                definitionRepo.SubjectConcept = subjectConcept;

                foreach (ISubjectConceptDefinition conceptDefinition in definitionRepo.Entities)
                {
                    var newconceptDefinition = definitionRepo.GetClone(conceptDefinition);
                    newconceptDefinition.SubjectConcept = newSubjectConcept;

                    databaseContext.Add(newconceptDefinition);
                }

                var exampleRepo = ServiceLocator.Current.GetInstance<ISubjectConceptExampleRepository>();
                exampleRepo.SubjectConcept = subjectConcept;

                foreach (ISubjectConceptExample example in exampleRepo.Entities)
                {
                    var newexample = exampleRepo.GetClone(example);
                    newexample.SubjectConcept = newSubjectConcept;

                    databaseContext.Add(newexample);
                }

                var relatedRepo = ServiceLocator.Current.GetInstance<IRelatedConceptRepository>();
                relatedRepo.OwnerSubjectConcept = subjectConcept;

                foreach (IRelatedConcept relatedConcept in relatedRepo.Entities)
                {
                    var newrelatedConcept = relatedRepo.GetClone(relatedConcept);
                    newrelatedConcept.OwnerSubjectConcept = newSubjectConcept;

                    //try to unmiss this
                    conceptService = ServiceLocator.Current.GetInstance<ISubjectConceptManagerApplicationServices>();
                    var realConcept = conceptService.Find(newrelatedConcept.SubjectConcept);

                    if (!Equals(realConcept, null))
                    {
                        ISpecification<ISubjectConcept> specification = new Specification<ISubjectConcept>(x => x.Code == realConcept.Code && x.Name == realConcept.Name);
                        var result = databaseContext.Where(specification);
                        if (result.Count() > 0)
                        {
                            newrelatedConcept.SubjectConcept = result.First().Id;
                        }
                    }

                    databaseContext.Add(newrelatedConcept);
                }
            }

            // reference documents
            var refDocRepo = ServiceLocator.Current.GetInstance<IReferenceDocumentRepository>();
            refDocRepo.Holder = exportable;

            foreach (IReferenceDocument refDocument in refDocRepo.Entities)
            {
                var newRefDocument = refDocRepo.GetClone(refDocument);
                newRefDocument.Holder = newSubject;
                databaseContext.Add(newRefDocument);
            }
        }

    }
}
