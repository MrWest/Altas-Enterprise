using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Default implementation of the contract <see cref="IInvestmentElementPresenter" />, representing the contract of
    ///     presenter view models used to impersonate investment elements in the UI.
    /// </summary>
    public class InvestmentElementPresenter :
        NomenclatorPresenterBase<IInvestmentElement, IInvestmentElementManagerApplicationServices>,
        IInvestmentElementPresenter
    {
        private IBudgetPresenter _budget;
        private IInvestmentComponentViewModel _childrenViewModel;
        private bool _isExpanded;


        /// <summary>
        ///     Initializes a new instance of a <see cref="InvestmentElementPresenter" /> given the investment element to
        ///     impersonate in the UI.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> that will be decorated and impersonated by the
        ///     <see cref="InvestmentElementPresenter" />
        ///     currently being initialized.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        public InvestmentElementPresenter(IInvestmentElement investmentElement)
            : base(investmentElement)
        {
        }

        /// <summary>
        ///     Gets the message that is displayed to the user when the current investment element presenter view model has
        ///     changed.
        /// </summary>
        protected override string SucessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedInvestmentElement; }
        }


        /// <summary>
        ///     Gets or sets the budget of the <see cref="IInvestmentElement" /> contained in the current
        ///     <see cref="IInvestmentElementPresenter" />.
        /// </summary>
        public IBudgetPresenter Budget
        {
            get
            {
                if (_budget == null)
                    throw new InvalidOperationException(Resources.InitializeBudgetBeforeUsingIt);

                return _budget;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _budget = value;
            }
        }

        /// <summary>
        ///     Gets or sets the parent <see cref="IInvestmentElementPresenter" /> of the current one.
        /// </summary>
        public IInvestmentElementPresenter Parent { get; set; }

        /// <summary>
        ///     Gets or sets the period  of the current <see cref="IInvestmentElementPresenter" />.
        /// </summary>
        public IInvestmentElementPeriodPresenter Period { get; set; }

        /// <summary>
        ///     Gets or sets the crud view model handling the investment element presenters being children of the current one.
        /// </summary>
        public IInvestmentComponentViewModel Elements
        {
            get
            {
                if (_childrenViewModel == null)
                {
                    _childrenViewModel = ServiceLocator.Current.GetInstance<IInvestmentComponentViewModel>();
                   // _childrenViewModel.Parent = this;
                    _childrenViewModel.Load();
                }

                return _childrenViewModel;
            }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [StringLengthValidator(1, 100, MessageTemplateResourceType = typeof (Resources), MessageTemplateResourceName = "InvestmentElementMustHaveAName")]
        public new string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;
                OnPropertyChanged(() => ShortName);
            }
        }

        /// <summary>
        ///     Gets or sets the depth the current <see cref="InvestmentElementPresenter" /> is with respect the root of the
        ///     tree formed by the root <see cref="InvestmentElementPresenter" /> containing directly or indirectly the current
        ///     one. Returns 0 if the current one is the root of the tree, 1 if it is a child of the tree's root, and so on.
        /// </summary>
        public int Depth
        {
            get { return Parent != null ? Parent.Depth + 1 : 0; }
        }

        /// <summary>
        ///     Gets the shortened version of the current <see cref="IInvestmentElementPresenter" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 6 ? Name.Substring(0, 6) + "..." : Name) : string.Empty; }
        }

        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="InvestmentElementPresenter" /> according to its depth.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string IconData
        {
            get
            {
                switch (Depth)
                {
                    case 0:
                        return
                            "F1 M 25,27L 46,19L 46,22.25L 28.5,29L 31.75,31.25L 51,23.75L 51,48.5L 31.75,57L 25,52L 25,27 Z ";
                    default:
                        return "F1 M 24,19L 40,19L 40,31L 52,31L 52,57L 24,57L 24,19 Z M 41,30L 41,19L 52,30L 41,30 Z ";
                }
            }
        }

        /// <summary>
        ///     Gets or sets whether the node rendering the current <see cref="InvestmentElementPresenter" /> data is expanded
        ///     or not.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded == value)
                    return;

                _isExpanded = value;
                OnPropertyChanged(() => IsExpanded);
            }
        }

        /// <summary>
        ///     Gets or sets the name of the current <see cref="ITreeNode" />.
        /// </summary>
        string ITreeNode.Name
        {
            get { return Name; }
            set { Name = value; }
        }

        /// <summary>
        ///     Gets the parent (if any) of the current <see cref="ITreeNode" />.
        /// </summary>
        ITreeNode ITreeNode.Parent
        {
            get { return Parent; }
        }

        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the children elements of the current
        ///     <see cref="ITreeNode" />.
        /// </summary>
        IEnumerable<ITreeNode> ITreeNode.Children
        {
            get { return Elements.Items; }
        }


        /// <summary>
        ///     Creates a new instance of the application services that this class will use.
        /// </summary>
        /// <returns>An instance of <see cref="IInvestmentElementManagerApplicationServices" />.</returns>
        protected override IInvestmentElementManagerApplicationServices CreateServices()
        {
            IInvestmentElementManagerApplicationServices services = base.CreateServices();
            services.Parent = Parent != null ? Parent.Object : null;

            return services;
        }

        
        public DateTime Start
        {
            get { return Period.Starts; }
            set
            {
               
                Period.Starts = value;

            }
        }

     
        public DateTime End
        {
            get { return Period.Ends; }
            set
            {
                Period.Ends = value;
            }
        }


        public ILifeline Value
        {
            get
            {
                return this;
            }
            set
            {
                this.Period.Starts = value.Start;
                this.Period.Ends = value.End;
                
            }
        }

        ITreeNode<ILifeline> ITreeNode<ILifeline>.Parent { get; set; }

         IEnumerable<ITreeNode<ILifeline>> ITreeNode<ILifeline>.Children
        {
            get { return Elements.Items; }
        }
         public int Percent
         {
             get { return GetPercent(); }
         }

         public int StartPercent
         {
             get { return GetStartPercent(); }
         }
         private int GetPercent()
         {
             if (Parent == null)
                 return 0;
             int fatherdays = Parent.Period.Days;
             int mydays = Period.Days;
             return fatherdays == 0 ? 0 : mydays * 100 / fatherdays;
         }

         private int GetStartPercent()
         {
             int fatherdays = Parent.Period.Days;
             int mydays = new Period(Parent.Period.Starts, Period.Starts).Days;
             return fatherdays == 0 ? 0 : mydays * 100 / fatherdays;
         }
         /// <summary>
         ///     Gets or sets the code of the underlying investment.
         /// </summary>
         public string Code
         {
             get { return Object.Code; }
             set { SetProperty(v => Object.Code = v, value); }
         }

         /// <summary>
         ///     Gets or sets the location of the underlying investment.
         /// </summary>
         public string Location
         {
             get { return Object.Location; }
             set { SetProperty(v => Object.Location = v, value); }
         }

         /// <summary>
         ///     Gets or sets the constructor of the underlying investment.
         /// </summary>
         public string Constructor
         {
             get { return Object.Constructor; }
             set { SetProperty(v => Object.Constructor = v, value); }
         }

         /// <summary>
         ///     Gets or sets the objective of the underlying investment.
         /// </summary>
         public string Objective
         {
             get { return Object.Objective; }
             set { SetProperty(v => Object.Objective = v, value); }
         }

         /// <summary>
         ///     Gets or sets the scope of the underlying investment.
         /// </summary>
         public string Scope
         {
             get { return Object.Scope; }
             set { SetProperty(v => Object.Scope = v, value); }
         }

         /// <summary>
         ///     Gets or sets the value of the current <see cref="ITreeNode{T}" />.
         /// </summary>
         //object ITreeNode.Value
         //{
         //    get { return Object; }
         //    set
         //    {
         //        if (value == null)
         //            throw new ArgumentNullException("value");

         //        Object = (IInvestmentElement) value;
         //    }
         //}

        
    }
}