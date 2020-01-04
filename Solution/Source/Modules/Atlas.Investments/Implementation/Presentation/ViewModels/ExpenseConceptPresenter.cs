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
    public class ExpenseConceptPresenter : CodedNomenclatorPresenterBase<IExpenseConcept, IExpenseConceptManagerApplicationServices>, IExpenseConceptPresenter, IView
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ExpenseConceptPresenter" /> given an Expense Concept.
        /// </summary>
        /// <param name="expenseConcept">The expense concept to decorate and impersonate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="expenseConcept" /> is null.</exception>
        public ExpenseConceptPresenter(IExpenseConcept expenseConcept)
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

        /// <summary>
        ///     Gets or sets the type of the current <see cref="IExpenseConcept" />.
        /// </summary>
        public ExpenseConceptType Type
        {
            get { return Object.Type; }
            set
            {
                SetProperty(v => Object.Type = v, value);
                OnPropertyChanged(()=>Type);
            }
        }
        
        /// <summary>
        ///     Gets the different values of the type <see cref="IExpenseConcept" />.
        /// </summary>
        public Array Types
        {
            get { return Enum.GetValues(typeof(ExpenseConceptType)); }
        }

     //   public IEntity OwnerEntity { get; set; }
        public IList<ISubExpenseConceptPresenter> Items { get { return SubExpenseConcepts.Items; }}

        private ISubExpenseConceptViewModel _subExpenseConcepts;

        public ISubExpenseConceptViewModel SubExpenseConcepts
        {
            get
            {
                if (_subExpenseConcepts == null)
                {
                    _subExpenseConcepts = ServiceLocator.Current.GetInstance<ISubExpenseConceptViewModel>();
                    _subExpenseConcepts.ExpenseConcept = this;
                    _subExpenseConcepts.Load();
                    _subExpenseConcepts.Raised += OnInteractionRequested;
                }
                return _subExpenseConcepts;
            }
        }

      

        public bool HasItems { get { return SubExpenseConcepts.Items.Count > 0; } }

        public object SelectedItem
        {
            get { return SubExpenseConcepts.SelectedItem; }
            set { SubExpenseConcepts.SelectedItem = (ISubExpenseConceptPresenter) value; }
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