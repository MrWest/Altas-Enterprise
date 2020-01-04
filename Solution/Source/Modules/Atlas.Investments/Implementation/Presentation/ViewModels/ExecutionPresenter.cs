using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// implements an <see cref="IExecution"/> for presentation
    /// </summary>
    public class ExecutionPresenter : EntityPresenterBase<IExecution,IExecutionManagerApplicationServices>, IExecutionPresenter
         //where TComponent :class, IBudgetComponent
    {
        private IExecutedActivityPresenter _activity;

        public IExecutedActivityPresenter ExecutedActivity
        {
            get { return _activity; } 
            set { _activity = value; }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetPresenter"/> decorating a domain budget.
        /// </summary>
        /// <param name="budget">The <see cref="IBudget"/> to present.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget"/> is null.</exception>
        public ExecutionPresenter(IExecution budget)
            : base(budget)
        {
        }

        public  String Description
        {
            get { return Object.Description; }
            set
            {
                SetProperty(v => Object.Description = v, value);
            }
        }

        public String ShortDescription
        {
            get { return  Description != null ? (Description.Length > 8 ? Description.Substring(0, 8) + "..." : Description) : string.Empty; }
            set
            {
                SetProperty(v => Object.Description = v, value);
            }
        }


        public DateTime Date
        {
            get { return Object.Date; }
            set
            {
                SetProperty(v => Object.Date = v, value);
            }
        }

        public decimal Amount
        {
            get { return Object.Amount; }
            set
            {
                SetProperty(v => Object.Amount = v, value);
                ExecutedActivity.DoNotify();
            }
        }
        public decimal Cost { get { return Amount; } }
        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override IExecutionManagerApplicationServices CreateServices()
        {
            IExecutionManagerApplicationServices services = base.CreateServices();
            services.ExecutedActivity = ExecutedActivity.Object;

            return services;
        }

        public bool IsCostCalculated { get; set; }
    }
}
