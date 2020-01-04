using System;
using System.Collections.Generic;
using System.Windows;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IExpenseConceptPresenter" /> being the presenter view model used to
    ///     decorated and impersonate Expense Concept domain entities in the UI.
    /// </summary>
    public class SpecialityPresenter : CodedNomenclatorPresenterBase<ISpeciality, ISpecialityManagerApplicationServices>, ISpecialityPresenter, IView
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ExpenseConceptPresenter" /> given an Expense Concept.
        /// </summary>
        /// <param name="expenseConcept">The expense concept to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="expenseConcept" /> is null.</exception>
        public SpecialityPresenter(ISpeciality expenseConcept)
            : base(expenseConcept)
        {
            
        }


        /// <summary>
        ///     Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected override string SuccessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedExpenseConcept; }
        }


        ///// <summary>
        /////     Gets or sets the code of the current <see cref="IExpenseConcept" />.
        ///// </summary>
        //public string Code
        //{
        //    get { return Object.Code; }
        //    set { SetProperty(v => Object.Code = v, value); }
        //}

        /// <summary>
        ///     Gets or sets the name of the current <see cref="IExpenseConcept" />.
        /// </summary>
        //public string Name
        //{
        //    get { return Object.Name; }
        //    set { SetProperty(v => Object.Name = v, value); }
        //}

      
     //   public IEntity OwnerEntity { get; set; }
        public IList<ISubSpecialityPresenter> Items { get { return SubSpecialities.Items; } }

        private ISubSpecialityViewModel _subSpecialities;

        public ISubSpecialityViewModel SubSpecialities
        {
            get
            {
                if (_subSpecialities == null)
                {
                    _subSpecialities = ServiceLocator.Current.GetInstance<ISubSpecialityViewModel>();
                    _subSpecialities.Speciality = this;
                    _subSpecialities.Load();
                    _subSpecialities.Raised += OnInteractionRequested;
                }
                return _subSpecialities;
            }
        }



        public bool HasItems { get { return SubSpecialities.Items.Count > 0; } }

        public object SelectedItem
        {
            get { return SubSpecialities.SelectedItem; }
            set { SubSpecialities.SelectedItem = (ISubSpecialityPresenter)value; }
        }

        //public ISubExpenseConceptPresenter SelectedItem
        //{
        //    get { return SubExpenseConcepts.SelectedItem; }
        //    set { SubExpenseConcepts.SelectedItem = value; }
        //}

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
        /// <summary>
        /// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        /// between this view and the new datacontext are wired up.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetupInteractionWithDataContext(e, OnInteractionRequested);
        }

    }
}