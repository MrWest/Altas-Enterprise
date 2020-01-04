using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentPresenter" /> representing the presenter view model used to
    ///     decorate and impersonate an investment in the UI.
    /// </summary>
    public class InvestmentPresenter :
        InvestmentElementPresenterBase<IInvestment, IInvestmentManagerApplicationServices>,
        IInvestmentPresenter, IView
    {
        private IInvestmentDocumentViewModel _documents;
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentPresenter" /> given an investment.
        /// </summary>
        /// <param name="investmentElement">The investment to impersonate and decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        //public InvestmentPresenter(IInvestment investmentElement)
        //    : base(investmentElement)
        //{
        //}

        public override ICommand DeleteMySelfCommand
        {
            get
            {

                var viewmodel = ServiceLocator.Current.GetInstance<IInvestmentViewModel>();
               // viewmodel.Raised += OnInteractionRequested;
                //viewmodel?.Load();
                //var shit = viewmodel?.DeleteCommand.CanExecute(viewmodel?.Items.First());
                return viewmodel.DeleteCommand;
            }
        }

        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedInvestment; }
        }

        /// <summary>
        ///     Gets or sets the capacity of the underlying investment.
        /// </summary>
        public string Capacity
        {
            get { return Object.Capacity; }
            set { SetProperty(v => Object.Capacity = v, value); }
        }

        /// <summary>
        ///     Gets or sets the induced doings of the underlying investment.
        /// </summary>
        public string InducedDoings
        {
            get { return Object.InducedDoings; }
            set { SetProperty(v => Object.InducedDoings = v, value); }
        }
        /// <summary>
        ///     Gets or sets the author or emitter of the underlying investment element.
        /// </summary>
        public string AuthorOrEmitter
        {
            get { return Object.AuthorOrEmitter; }
            set { SetProperty(v => Object.AuthorOrEmitter = v, value); }
        }

        /// <summary>
        ///     Gets or sets the entity of the underlying investment element.
        /// </summary>
        public string Entity
        {
            get { return Object.Entity; }
            set { SetProperty(v => Object.Entity = v, value); }
        }

        /// <summary>
        ///     Gets or sets the related programs of the underlying investment.
        /// </summary>
        public string RelatedPrograms
        {
            get { return Object.RelatedPrograms; }
            set { SetProperty(v => Object.RelatedPrograms = v, value); }
        }

        /// <summary>
        /// Get or sets the Osde of the current <see cref="IInvestmentPresenter"/>.
        /// </summary>
        public IOsdePresenter Osde
        {

            get
            {

                return Object.Osde != null ? ServiceLocator.Current.GetInstance<IOsdeProvider>().Osdes.SingleOrDefault(x => x.Id.ToString() == Object.Osde.ToString()) : null;

            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Osde = v.Id, value);
                    OnPropertyChanged(() => Osde);
                }
            }
        }
        /// <summary>
        /// Get or sets the Oace of the current <see cref="IInvestmentPresenter"/>.
        /// </summary>
        public IOacePresenter Oace
        {

            get
            {

                return Object.Oace != null ? ServiceLocator.Current.GetInstance<IOaceProvider>().Oaces.SingleOrDefault(x => x.Id.ToString() == Object.Oace.ToString()) : null;

            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Oace = v.Id, value);
                    OnPropertyChanged(() => Oace);
                }
            }
        }
        /// <summary>
        /// Get or sets the Phase of the current <see cref="IInvestmentPresenter"/>.
        /// </summary>
        public IPhasePresenter Phase
        {

            get
            {

                return Object.Phase != null ? ServiceLocator.Current.GetInstance<IPhaseProvider>().Phases.SingleOrDefault(x => x.Id.ToString() == Object.Phase.ToString()) : null;

            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Phase = v.Id, value);
                    OnPropertyChanged(() => Phase);
                }
            }
        }
        //public IInvestmentTypePresenter InvestmentType
        //{
        //    get
        //    {

        //        return Object.InvestmentType != null ? ServiceLocator.Current.GetInstance<IInvestmentTypeProvider>().InvestmentTypes.SingleOrDefault(x => x.Id.ToString() == Object.InvestmentType.ToString()) : null;

        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            SetProperty(v => Object.InvestmentType = v.Id, value);
        //            OnPropertyChanged(() => InvestmentType);
        //        }
        //    }
        //}

        /// <summary>
        ///     Gets or sets the entity of the underlying investment element.
        /// </summary>
        public string InvestmentType
        {
            get { return Object.InvestmentType; }
            set { SetProperty(v => Object.InvestmentType = v, value); }
        }

        /// <summary>
        ///     Gets or sets the entity of the underlying investment element.
        /// </summary>
        public string Nature
        {
            get { return Object.Nature; }
            set { SetProperty(v => Object.Nature = v, value); }
        }
        /// <summary>
        ///     Gets or sets the entity of the underlying investment element.
        /// </summary>
        public string Impact
        {
            get { return Object.Impact; }
            set { SetProperty(v => Object.Impact = v, value); }
        }

        public IInvestmentDocumentViewModel Documents
        {
            get
            {
                if (_documents == null)
                {
                    _documents = ServiceLocator.Current.GetInstance<IInvestmentDocumentViewModel>();
                    //  Actioner(x => x.SuperiorCategory = Object);
                    _documents.Holder = this;
                    _documents.Load();

                    _documents.Raised += OnInteractionRequested;

                }
                return _documents;
            }
        }

        public bool Export(IDatabaseContext exportDatabaseContext)
        {
            if (exportDatabaseContext == null)
                return false;
           return CreateServices().Export(exportDatabaseContext,Object)!=null;
          
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

        public override string Kind
        {
            get { return "Investment"; }
        }

        public IEnumerable<string> FindInvestmentTypeByContains(string text)
        {
            var rslt = new List<string>();
            var list = CreateServices().FindInvestmentTypeByContains(text);
            if (list != null)
            {
                foreach (IInvestment entity in list)
                {
                    if (!rslt.Contains(entity.InvestmentType))
                        rslt.Add(entity.InvestmentType);
                }
            }

            return rslt;
        }

        public IEnumerable<string> FindNatureByContains(string text)
        {
            var rslt = new List<string>();
            var list = CreateServices().FindNatureByContains(text);
            if (list != null)
            {
                foreach (IInvestment entity in list)
                {
                    if (!rslt.Contains(entity.Nature))
                        rslt.Add(entity.Nature);
                }
            }

            return rslt;
        }

        public IEnumerable<string> FindImpactByContains(string text)
        {
            var rslt = new List<string>();
            var list = CreateServices().FindImpactByContains(text);
            if (list != null)
            {
                foreach (IInvestment entity in list)
                {
                    if (!rslt.Contains(entity.Impact))
                        rslt.Add(entity.Impact);
                }
            }

            return rslt;
        }
    }
}