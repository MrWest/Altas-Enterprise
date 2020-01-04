using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class SubjectConceptContentDomainServices<TEntity>:DomainServicesBase<TEntity>, ISubjectConceptContentDomainServices<TEntity>
        where TEntity : class, ISubjectConceptContent
    {
        public ISubjectConcept SubjectConcept { get; set; }

        public override TEntity Create()
        {
            var conceptContet = base.Create();
            conceptContet.SubjectConcept = SubjectConcept;
            conceptContet.LastUpdate = DateTime.Now;
            return conceptContet;

        }
    }
}
