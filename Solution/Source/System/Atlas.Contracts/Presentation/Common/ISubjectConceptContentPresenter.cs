using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface ISubjectConceptContentPresenter<TEntity>:IPresenter<TEntity>, ISubjectConceptContentPresenter
         where TEntity : class, ISubjectConceptContent
    {
        ISubjectConceptPresenter SubjectConcept { get; set; }
      
       

    }

    public interface ISubjectConceptContentPresenter
    {
        String Content { get; set; }
        String Source { get; set; }
        String Author { get; set; }
        String WebSite { get; set; }
        DateTime LastUpdate { get; set; }

        IEnumerable<string> FindAutorByContains(string text);
        IEnumerable<string> FindSourceByContains(string text);
    }
}
