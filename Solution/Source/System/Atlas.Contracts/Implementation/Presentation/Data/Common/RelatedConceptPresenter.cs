using System;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Db4objects.Db4o.Internal.Reflect;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class RelatedConceptPresenter:EntityPresenterBase<IRelatedConcept, IRelatedConceptManagerApplicationServices>, IRelatedConceptPresenter
    {
        private ISubjectConceptManagerApplicationServices _service;
        private ISubjectConceptPresenter _presenter;

        public ISubjectConceptPresenter OwnerSubjectConcept { get; set; }

        protected override IRelatedConceptManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OwnerSubjectConcept = OwnerSubjectConcept.Object;
            return service;
        }

        public string SubjectConcept
        {
            get { return this.ToString(); }
            set
            {
                SetProperty(v => Object.SubjectConcept = v, value);
                _name = null;
                OnPropertyChanged(()=>SubjectConcept);
            }
        }

        public  int Number => OwnerSubjectConcept.RelatedConcepts.Items.IndexOf(this) + 1;

        private string _name = null;
        public override string  ToString()
        {
                if(_service==null)
                 _service = ServiceLocator.Current.GetInstance<ISubjectConceptManagerApplicationServices>();
                if(_name == null)
                 _name = _service.Find(Object.SubjectConcept)?.Name;
            return _name;

        }

        public ISubjectConceptPresenter SubjectConceptPresenter
        {
            get
            {
                if (_service == null)
                    _service = ServiceLocator.Current.GetInstance<ISubjectConceptManagerApplicationServices>();
                if (_presenter==null)
                    _presenter = ServiceLocator.Current.GetInstance<ISubjectConceptPresenter>();
                _presenter.Object =_service.Find(Object.SubjectConcept);

                return _presenter;
            }
        }
    }
}