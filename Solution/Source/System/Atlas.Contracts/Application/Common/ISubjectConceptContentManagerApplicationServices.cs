using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    public interface ISubjectConceptContentManagerApplicationServices<TEntity>:IItemManagerApplicationServices<TEntity>
          where TEntity : class, ISubjectConceptContent
    {
        ISubjectConcept SubjectConcept { get; set; }
        IEnumerable<ISubjectConceptContent> FindAutorByContains(string text);
        IEnumerable<ISubjectConceptContent> FindSourceByContains(string text);
    }
}
