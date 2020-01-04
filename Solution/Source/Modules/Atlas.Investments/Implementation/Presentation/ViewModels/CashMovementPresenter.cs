using System;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class CashMovementPresenter<TCategory>:EntityPresenterBase<ICashMovement,ICashMovementManagerApplicationServices>, ICashMovementPresenter<TCategory>
        where TCategory:class ,ICashMovementCategory
    {
        private  ICashMovementCategoryPresenter<TCategory> _category { get; set; } 
        public ICashMovementCategoryPresenter<TCategory> Category { get { return _category; } set { _category = value; } }

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
                Category.Notify();
            }
        }

        /// <summary>
        /// Creates the application services used to make the updates made to the current
        /// <see cref="BudgetComponentItemPresenterBase{TItem,TComponent,TServices}"/>.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices"/>.</returns>
        protected override ICashMovementManagerApplicationServices CreateServices()
        {
            ICashMovementManagerApplicationServices services = base.CreateServices();


            services.Category = Category.Object;



            return services;
        }
    }
}
