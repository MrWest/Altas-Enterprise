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
    public abstract class SubjectConceptContentPresenter<TEntity, TService> : EntityPresenterBase<TEntity, TService>,
        ISubjectConceptContentPresenter<TEntity>
        where TEntity : class, ISubjectConceptContent
        where TService : class, ISubjectConceptContentManagerApplicationServices<TEntity>
    {
        public ISubjectConceptPresenter SubjectConcept { get; set; }

        protected override TService CreateServices()
        {
            var service = base.CreateServices();
            service.SubjectConcept = SubjectConcept.Object;
            return service;
        }

        public String Content
        {
            get { return Object.Content; }
            set
            {
                SetProperty(v => Object.Content = v, value);
                OnPropertyChanged(() => Content);
                OnPropertyChanged(() => ContentNumbered);
                OnPropertyChanged(() => ShortContent);
                LastUpdate = DateTime.Now;
                OnPropertyChanged(() => LastUpdate);
               
            }
        }

        public object ContentNumbered
        {
            get
            {

                return Number.ToString() +"- "+ Object.Content;
            }
        }

        public virtual object ShortContent
        {
            get { return Content != null ? (Content.ToString().Length > 90 ? Content.ToString().Substring(0, 90) + "..." : Content) : string.Empty; }

        }
        public String Source
        {
            get { return Object.Source; }
            set
            {
                SetProperty(v => Object.Source = v, value);
                OnPropertyChanged(() => Source);
                LastUpdate = DateTime.Now;
                OnPropertyChanged(() => LastUpdate);
            }

        }

        public String Author
        {
            get { return Object.Author; }
            set
            {
                SetProperty(v => Object.Author = v, value);
                OnPropertyChanged(() => Author);
                LastUpdate = DateTime.Now;
                OnPropertyChanged(() => LastUpdate);
            }
        }

        public String WebSite
        {
            get { return Object.WebSite; }
            set
            {
                SetProperty(v => Object.WebSite = v, value);
                OnPropertyChanged(() => WebSite);
                LastUpdate = DateTime.Now;
                OnPropertyChanged(() => LastUpdate);
            }
        }

        public DateTime LastUpdate
        {
            get { return Object.LastUpdate; }
            set
            {
                SetProperty(v => Object.LastUpdate = v, value);
                OnPropertyChanged(() => LastUpdate);
            }
        }

        public virtual int Number
        {
            get { return 0; }
        }

        public IEnumerable<string> FindAutorByContains(string text)
        {
            var rslt = new List<string>();
            var list = CreateServices().FindAutorByContains(text);
            if (list != null)
            {
                foreach (ISubjectConceptContent entity in list)
                {
                    if (!rslt.Contains(entity.Author))
                        rslt.Add(entity.Author);
                }
            }

            return rslt;
        }

        public IEnumerable<string> FindSourceByContains(string text)
        {
            var rslt = new List<string>();
            var list = CreateServices().FindSourceByContains(text);
            if (list != null)
            {
                foreach (ISubjectConceptContent entity in list)
                {
                    if(!rslt.Contains(entity.Source))
                    rslt.Add(entity.Source);
                }
            }

            return rslt;
        }
    }
}
