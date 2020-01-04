using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class CashMovementCategoryPresenter<TItem> : NomenclatorPresenterBase<TItem, ICashMovementCategoryManagerApplicationServices<TItem>>, ICashMovementCategoryPresenter<TItem>, IView
     where TItem : class ,ICashMovementCategory
    {
        private ICashMovementCategoryViewModel<TItem> _subCategories;
        private ICashMovementViewModel<TItem> _movements;
        private IList<ICashMovement> _movementsList; 
        private bool _isExpanded;
        //private ICashMovementViewModel<TItem> _subCategories;
        /// <summary>
        /// Superior Category
        /// </summary>
        public ICashMovementCategoryPresenter<TItem> SuperiorCategory { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetPresenter"/> decorating a domain budget but without such budget.
        /// </summary>
        public CashMovementCategoryPresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetPresenter"/> decorating a domain budget.
        /// </summary>
        /// <param name="budget">The <see cref="IBudget"/> to present.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget"/> is null.</exception>
        public CashMovementCategoryPresenter(TItem budget)
            : base(budget)
        {
        }


        public override void Notify()
        {
            base.Notify();

            SuperiorCategory.Notify();
        }

        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public ICashMovementCategoryViewModel<TItem> SubCategories
        {
            get
            {
                if (_subCategories == null)
                {
                    _subCategories = ServiceLocator.Current.GetInstance<ICashMovementCategoryViewModel<TItem>>();
                  //  Actioner(x => x.SuperiorCategory = Object);
                    _subCategories.SuperiorCategory = this;
                    _subCategories.Load();

                    _subCategories.Raised += OnInteractionRequested;

                }
                return _subCategories;
            }
            set { _subCategories = value; }
        }

        public IList<ICashMovement> MovementsList {
            get
            {
                if (_movementsList != null && _movements!=null && _movementsList.Count == _movements.Items.Count
                    && SubCategories.Items.Count == 0 && Movements.Items.Count==0)
                    return _movementsList;

                if (wassetted)
                    return _movementsList;

                 _movementsList = new List<ICashMovement>();

                if (SubCategories.Items.Count > 0)
                    foreach (var cashMovementCategoryPresenter in SubCategories.Items)
                    {
                        foreach (var cashMovementPresenter in cashMovementCategoryPresenter.MovementsList)
                        {

                            _movementsList.Add(cashMovementPresenter);
                        }
                    }
                else
                {
                    Movements.Load();
                    foreach (var cashMovementPresenter in Movements.Items)
                    {

                        _movementsList.Add(cashMovementPresenter.Object);
                    }
               
                }
                    
                return _movementsList;
            }
            set
            {
                if (value != null)
                {
                    var cashmovement = ServiceLocator.Current.GetInstance<ICashMovementPresenter<TItem>>();
                    cashmovement.Object = value[0] as ICashMovement;
                    var item = Movements.Items.FirstOrDefault(x => x.Date == cashmovement.Date);
                    if (item != null)
                        item.Amount = cashmovement.Object.Amount;
                    else
                    Movements.Add(cashmovement);

                    OnPropertyChanged(() => MovementsList);
                }
              
            } }

        public ICommand AddCommand { get
        {
            if (SubCategories != null)
                        return SubCategories.AddCommand;
                    return null;
                } 
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (SuperiorCategory != null)
                    return SuperiorCategory.SubCategories.DeleteCommand;
                return null;
            }
        }

        public ICashMovementCategoryPresenter<TItem> MySelf
        {
            get { return this; }
        }

        public IList<ICashMovement> AllCashMovements
        {
            get
            {
                if(SubCategories.Items.Count==0)
                    return MovementsList;
                IList<ICashMovement> movementsList = new List<ICashMovement>();
                return AppendMyChildCashMovements(ref movementsList);
            }
        }

        private IList<ICashMovement> AppendMyChildCashMovements(ref IList<ICashMovement> list)
        {

            foreach (var subcategories in SubCategories.Items)
            {
                foreach (var movement in subcategories.AllCashMovements)
                {
                    list.Add(movement);
                }
               
               
            }

            return list;
        }
        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public ICashMovementViewModel<TItem> Movements
        {
            get
            {
                if (_movements == null)
                {
                    _movements = ServiceLocator.Current.GetInstance<ICashMovementViewModel<TItem>>();
                    //  Actioner(x => x.SuperiorCategory = Object);
                    _movements.Category = this;
                    _movements.Load();

                    //_subCategories.Raised += OnInteractionRequested;

                }
                return _movements;
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged(() => IsExpanded);
                OnPropertyChanged(() => IconData);
                TellYourFather();
            }
        }

       
        public virtual void TellYourFather()
        {
            OnPropertyChanged(() => MovementsList);
            if (SuperiorCategory!=null)
            SuperiorCategory.TellYourFather();
        }

        public void TellYourSelf()
        {
            OnPropertyChanged(() => MovementsList);
            OnPropertyChanged(() => IconData);
        }

        private bool wassetted;
        public void SetMovementsList(IList<ICashMovement> list)
        {
            if ( list != null)
            {
                if (_movementsList == null)
                    _movementsList = new List<ICashMovement>();
                _movementsList.Clear();
                foreach (ICashMovement cashMovement in list)
                {
                    _movementsList.Add(cashMovement);
                }
                wassetted = true;
            }
           
        }

        /// <summary>
        /// Creates the application services used to make the updates made to the current
        /// <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/>.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices"/>.</returns>
        protected override ICashMovementCategoryManagerApplicationServices<TItem> CreateServices()
        {
            ICashMovementCategoryManagerApplicationServices<TItem> services = base.CreateServices();


            services.SuperiorCategory = SuperiorCategory.Object;



            return services;
        }
        private void Actioner(Func<ICashMovementCategory, object> method)
        {
            Actioner((services =>
            {
                method(services);
                return null;
            }));
        }

        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 12 ? Name.Substring(0, 12) + "..." : Name) : string.Empty; }
            set { SetProperty(v => Object.Name = v, value); }
        }

        public bool HasSons { get { return SubCategories.Items.Count > 0; }}


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public override string Name
        {
            get { return Object.Name; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
            }
        }

        public decimal TotalMovements
        {
            get { return Movements.Items.Sum(x => x.Amount); }
        }

        public virtual bool ShowLiquity { get; set; }

        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="InvestmentElementPresenterBase{T,TServices}" /> according to its depth.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public string IconData
        {
            get
            {
                if (HasSons)
                {
                    if (IsExpanded)
                    return "F1 M 0,12 0,8 20,8 20,12 Z ";

                    return "F1 M 0,12 0,8 8,8 8,0 12,0 12,8 20,8 20,12 12,12 12,20 8,20 8,12 Z ";
                }
                  
              return "";
            }
            
        }

        /// <summary>
        /// work out whit selection
        /// </summary>
        public bool IsSelected { get; set; }

        public bool IsMouseOver { get; set; }

        public void InDeepList<TItem>(ref IList<ICashMovementCategoryPresenter<TItem>> list)
            where TItem:class, ICashMovementCategory
        
        {

            foreach (ICashMovementCategoryPresenter<TItem> cashmovement in SubCategories.Items)
            {
                list.Add(cashmovement);
                if(cashmovement.IsExpanded)
                cashmovement.InDeepList( ref list);
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


        public virtual int Level
        {
            get { return SuperiorCategory.Level + 1; }
            
        }

        public void SetCashMovement(decimal value, IPeriod period)
        {
           CreateServices().SetCashMovement(value,period,Object);
        }
    }
}
