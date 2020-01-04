using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class SubjectConceptPresenter: NomenclatorPresenterBase<ISubjectConcept, ISubjectConceptManagerApplicationServices>, ISubjectConceptPresenter, IView
    {
        private ISubjectConceptDefinitionViewModel _conceptDefinitions;
        private ISubjectConceptExampleViewModel _conceptExamples;
        private IRelatedConceptViewModel _relatedConcepts;

        public IAtlasModuleGenericSubjectPresenter ModuleSubject { get; set; }

        protected override ISubjectConceptManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.ModuleSubject = (ModuleSubject as IPresenter)?.Object as IAtlasModuleGenericSubject;
            return service;
        }

        public string Code
        {
            get { return Object.Code; }
            set
            {
                SetProperty(v => Object.Code = v, value);
                OnPropertyChanged(() => Code);
            }
        }



        public ISubjectConceptDefinitionViewModel ConceptDefinitions
        {
            get
            {
                if (_conceptDefinitions == null)
                {
                    _conceptDefinitions = ServiceLocator.Current.GetInstance<ISubjectConceptDefinitionViewModel>();

                    _conceptDefinitions.SubjectConcept = this;
                    _conceptDefinitions.Load();

                    _conceptDefinitions.Raised += OnInteractionRequested;

                }
                return _conceptDefinitions;
            }
        }

        public ISubjectConceptExampleViewModel ConceptExamples
        {
            get
            {
                if (_conceptExamples == null)
                {
                    _conceptExamples = ServiceLocator.Current.GetInstance<ISubjectConceptExampleViewModel>();

                    _conceptExamples.SubjectConcept = this;
                    _conceptExamples.Load();

                    _conceptExamples.Raised += OnInteractionRequested;

                }
                return _conceptExamples;
            }
        }
        public IRelatedConceptViewModel RelatedConcepts
        {
            get
            {
                if (_relatedConcepts == null)
                {
                    _relatedConcepts = ServiceLocator.Current.GetInstance<IRelatedConceptViewModel>();

                    _relatedConcepts.OwnerSubjectConcept = this;
                    _relatedConcepts.Load();

                    _relatedConcepts.Raised += OnInteractionRequested;

                }
                return _relatedConcepts;
            }
        }
        public object DataContext { get; set; }
        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }
    }
}
