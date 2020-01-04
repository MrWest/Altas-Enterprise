using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class SubjectConceptContentViewModel<TEntity,TPresenter,TService>:CrudViewModelBase<TEntity,TPresenter,TService>, ISubjectConceptContentViewModel<TEntity,TPresenter>
         where TEntity : class, ISubjectConceptContent
         where TPresenter : class, ISubjectConceptContentPresenter<TEntity>
        where TService : class, ISubjectConceptContentManagerApplicationServices<TEntity>
    {
        public ISubjectConceptPresenter SubjectConcept { get; set; }

        protected override TPresenter CreatePresenterFor(TEntity item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.SubjectConcept = SubjectConcept;
            return presenter;
        }

        protected override TService CreateServices()
        {
            var service = base.CreateServices();
            service.SubjectConcept = SubjectConcept.Object;
            return service;
        }
    }
}
