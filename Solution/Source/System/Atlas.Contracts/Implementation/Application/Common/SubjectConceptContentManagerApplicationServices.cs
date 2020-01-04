using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class SubjectConceptContentManagerApplicationServices<TEntity, TRepository, TDomain> :
        ItemManagerApplicationServicesBase<TEntity, TRepository, TDomain>,
        ISubjectConceptContentManagerApplicationServices<TEntity>
        where TEntity : class, ISubjectConceptContent
        where TRepository : class, ISubjectConceptContentRepository<TEntity>
        where TDomain : class, ISubjectConceptContentDomainServices<TEntity>
    {
        public ISubjectConcept SubjectConcept { get; set; }

        protected override TRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.SubjectConcept = SubjectConcept;
                return repo;
            }
        }

        protected override TDomain DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.SubjectConcept = SubjectConcept;
                return domain;
            }
        }


        public IEnumerable<ISubjectConceptContent> FindAutorByContains(string text)
        {
            if (text == null)
                return null;
            var repo = ServiceLocator.Current.GetInstance<ICommonRepository<ISubjectConceptDefinition>>();
            var list1 = repo.Where((x => !string.IsNullOrEmpty(x.Author) && x.Author.ToLower().Contains(text.ToLower()))).Take(5);

            var repo2 = ServiceLocator.Current.GetInstance<ICommonRepository<ISubjectConceptExample>>();
            var list2 = repo2.Where((x => !string.IsNullOrEmpty(x.Author) && x.Author.ToLower().Contains(text.ToLower()))).Take(5);

            var rslt1 = list1.Cast<ISubjectConceptContent>().ToList();
            var rslt2 = list2.Cast<ISubjectConceptContent>().ToList();
            var rsltfinal = new List<ISubjectConceptContent>();
            foreach (ISubjectConceptContent subjectConceptContent in rslt1)
            {
                rsltfinal.Add(subjectConceptContent);
            }

            foreach (ISubjectConceptContent subjectConceptContent in rslt2)
            {
                rsltfinal.Add(subjectConceptContent);
            }
            return rsltfinal.Take(5);

        }

        public IEnumerable<ISubjectConceptContent> FindSourceByContains(string text)
        {
            if (text == null)
                return null;
            var repo = ServiceLocator.Current.GetInstance<ICommonRepository<ISubjectConceptDefinition>>();
            var list1 = repo.Where((x => !string.IsNullOrEmpty(x.Source) && x.Source.ToLower().Contains(text.ToLower()))).Take(5);

            var repo2 = ServiceLocator.Current.GetInstance<ICommonRepository<ISubjectConceptExample>>();
            var list2 = repo.Where((x => !string.IsNullOrEmpty(x.Source) && x.Source.ToLower().Contains(text.ToLower()))).Take(5);

            var rslt1 = list1.Cast<ISubjectConceptContent>().ToList();
            var rslt2 = list2.Cast<ISubjectConceptContent>().ToList();
            var rsltfinal = new List<ISubjectConceptContent>();
            foreach (ISubjectConceptContent subjectConceptContent in rslt1)
            {
                rsltfinal.Add(subjectConceptContent);
            }

            foreach (ISubjectConceptContent subjectConceptContent in rslt2)
            {
                rsltfinal.Add(subjectConceptContent);
            }
            return rsltfinal.Take(5);

        }
    }
}
