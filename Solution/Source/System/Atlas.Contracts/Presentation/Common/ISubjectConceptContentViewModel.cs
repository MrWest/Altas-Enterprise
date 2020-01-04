using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface ISubjectConceptContentViewModel<TEntity,TPresenter>:ICrudViewModel<TEntity,TPresenter>
          where TEntity : class, ISubjectConceptContent
        where TPresenter : class, ISubjectConceptContentPresenter<TEntity>
    {
        ISubjectConceptPresenter SubjectConcept { get; set; }
    }
}
