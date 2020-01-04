using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public abstract class SubSpecialityHolderPresenter<THolder,TComponent,TService>: NavigableNomenclator<THolder, TService>, ISubSpecialityHolderPresenter<THolder,TComponent>
        where TComponent : class, IBudgetComponent
        where THolder:class , ISubSpecialityHolder
        where TService:class, ISubSpecialityHolderManagerApplicationServices<THolder>
    {
        private ISubSpecialityManagerApplicationServices _service;
        private object _isVisible;

        public ISubSpeciality SubSpeciality
        {
            get
            {
                if (_service == null)
                   _service = ServiceLocator.Current.GetInstance<ISubSpecialityManagerApplicationServices>();

                return _service.Find(Object.SubSpeciality);
            }

           
        }

        public override string Name
        {
            get { return SubSpeciality?.Name; }
            set
            {
               OnPropertyChanged(()=>Name) ;
            }
        }

        public object Code
        {
            get
            {
                if(!Equals(SubSpeciality,null))
                 return SubSpeciality.Code;
                if (!Equals(SubExpenseConcept, null))
                    return SubExpenseConcept.Code;
                if (!Equals(Category, null))
                    return Category.Code;
                return null;
            }
        }
        protected override TService CreateServices()
        {
            var service = base.CreateServices();
            service.BudgetComponent = BudgetComponent.Object;
            return service;
        }

        public IBudgetComponentPresenter<TComponent> BudgetComponent { get; set; }
        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="InvestmentElementPresenterBase{T,TServices}" /> according to its depth.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override string IconData
        {
            get
            {
                if (HasItems)
                {
                    if (IsExpanded)
                        return "F1 M 0,12 0,8 20,8 20,12 Z ";

                    return "F1 M 0,12 0,8 8,8 8,0 12,0 12,8 20,8 20,12 12,12 12,20 8,20 8,12 Z ";
                }

                return "";
            }

        }

        public string SubSpecialityObject
        {
            get
            {
                return Name;
            }
            set
            {
                SetProperty(v => Object.SubSpeciality = v, value);
                OnPropertyChanged(() => SubSpeciality);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => Code);
                RefreshCommand();
              


            }
        }


        /// <summary>
        /// Get or sets the Category of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public ICategoryPresenter Category
        {

            get
            {
                return Object.Category != null
                    ? ServiceLocator.Current.GetInstance<ICategoryProvider>()
                        .Categories.SingleOrDefault(x => x.Id.ToString() == Object.Category.ToString())
                    : null;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Category = v.Id, value);
                    OnPropertyChanged(() => Category);
                    RefreshCommand();
                }
            }
        }


        //public static DependencyProperty SubLevelProperty = DependencyProperty.Register("SubExpenseConcept", typeof(object), typeof(BudgetComponentItemBase), new PropertyMetadata(OnSubLevelChanged));

        //private static void OnSubLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var sublevel = e.NewValue;
        //}

        protected ISubExpenseConceptPresenter _subExpenseConcept;

        /// <summary>
        /// Get or sets the Expense Concept of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual ISubExpenseConceptPresenter SubExpenseConcept
        {
            get
            {
                return Object.SubExpenseConcept != null
                    ? ServiceLocator.Current.GetInstance<IExpenseConceptProvider>()
                        .ExpenseConcepts.SingleOrDefault(x => x.SubExpenseConcepts.Any(s=> s.Object.Id == Object.SubExpenseConcept))?.SubExpenseConcepts.SingleOrDefault(x=> x.Object.Id == Object.SubExpenseConcept)
                    : null;
                //if (_subExpenseConcept == null)
                //{
                //    _subExpenseConcept = ServiceLocator.Current.GetInstance<ISubExpenseConceptPresenter>();
                //    var provider =
                //        ServiceLocator.Current
                //            .GetInstance<ISubExpenseConceptManagerApplicationServices>();

                //    var entity = provider.Find(Object.SubExpenseConcept);
                //    //entity = ServiceLocator.Current.GetInstance<ISubExpenseConcept>();
                //    //entity.Name = "ANTIMETA";
                //    if (entity != null)
                //    {
                //        var expenseconcept = ServiceLocator.Current.GetInstance<IExpenseConceptProvider>();
                //        bool dobreak = false;
                //        foreach (IExpenseConceptPresenter expenseconceptExpenseConcept in expenseconcept.ExpenseConcepts)
                //        {
                //            foreach (ISubExpenseConceptPresenter subExpenseConceptPresenter in expenseconceptExpenseConcept.SubExpenseConcepts)
                //            {
                //                if (subExpenseConceptPresenter.Object.Id == entity.Id)
                //                {
                //                    _subExpenseConcept = subExpenseConceptPresenter;
                //                    dobreak = true;
                //                    break;

                //                }

                //            }
                //            if(dobreak)
                //                break;
                //        }
                //    }

                //}
                ////  return Object.SubExpenseConcept != null ? ServiceLocator.Current.GetInstance<IExpenseConceptProvider>().ExpenseConcepts.FirstOrDefault(x => true) : null;
                //return _subExpenseConcept;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.SubExpenseConcept = v.Object.Id, value);
                    //_subExpenseConcept = value;
                    OnPropertyChanged(() => SubExpenseConcept);
                    RefreshCommand();
                }

            }

        }

        protected abstract void RefreshCommand();

        public virtual DateTime StartDate()
        {
            throw new NotImplementedException();
        }

        public virtual DateTime FinishDate()
        {
            throw new NotImplementedException();
        }

        private bool _startCalculated = false;

        public bool StartCalculated
        {
            get { return _startCalculated; }
            set
            {
                bool change = _startCalculated;
                _startCalculated = value;
                if (!value)
                {
                    // OnPropertyChanged(() => Period);
                    if (BudgetComponent != null)
                        BudgetComponent.StartCalculated = _startCalculated;
                    
                }
            }
        }
        private bool _endCalculated = false;

        public bool EndCalculated
        {
            get { return _endCalculated; }
            set
            {
                bool change = _endCalculated;
                _endCalculated = value;
                if (!value)
                {
                    // OnPropertyChanged(() => Period);
                    if (BudgetComponent != null)
                        BudgetComponent.EndCalculated = _endCalculated;
                    
                }
            }
        }

        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }

        public virtual decimal Cost
        {
            get
            {
              //  decimal cost = 0;
             //   if (Currency != null)
                   
                //if (Currency!=null)
                return Math.Round(Items.Items.Cast<ICosttable>().Sum(x=>x.Cost), 2);
            }
        }

        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "SubSpeciality"; }
        }

        public override int Depth { get { return 2; } }

        public override void Notify()
        {
          
                    OnPropertyChanged(() => SubSpeciality);
                    OnPropertyChanged(() => Name);
                    OnPropertyChanged(() => Code);
            
        }

        public DateTime Start { get { return StartDate(); } set
        {
            ;
        } }
        public DateTime End { get { return FinishDate(); } set
        {
            ;
        } }

        public Thickness TimeLineThickness
        {
            get
            {
                return BudgetComponent.Budget.InvestmentElement.TimeLineThickness;
            }
            set
            {
                ;// BudgetComponent.Budget.InvestmentElement.TimeLineThickness = value;
            }
        }

        public override INavigable Parent
        {
            get { return BudgetComponent.Budget.InvestmentElement; }
        }

        public override Thickness DeepThickness
        {
            get
            {
                //if (Parent != null)
                //    return new Thickness(Parent.DeepThickness.Left + 13, 0, 0, 0);

                return new Thickness(0, 0, 0, 0);
            }

        }

        public Brush BackgroundColorBrush { get; set; }
    }
}