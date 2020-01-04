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

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
   public class CashMovementViewModel<TCategory>:CrudViewModelBase<ICashMovement,ICashMovementPresenter<TCategory>,ICashMovementManagerApplicationServices>, ICashMovementViewModel<TCategory>
       where TCategory:class ,ICashMovementCategory
   {
       private ICashMovementCategoryPresenter<TCategory> _category { get; set; }
     public  ICashMovementCategoryPresenter<TCategory> Category { get { return _category; } set { _category = value; } }
       public void DeleteAll()
       {
           CreateServices().DeleteAll();
       }

       /// <summary>
     ///     Adds a new budget component items presenter to the current view model.
     /// </summary>
     /// <param name="presenter">The budget component item to add.</param>
     /// <exception cref="ArgumentNullException"><paramref name="presenter" /> is null.</exception>
     public override void Add(ICashMovementPresenter<TCategory> presenter)
     {
         base.Add(presenter);
         Load();

     }

     /// <summary>
     /// Loads the items from the data source.
     /// </summary>
     public override void Load()
     {
         foreach (ICashMovementPresenter<TCategory> presenter in Items)
             presenter.PropertyChanged -= OnPresenterPropertyChanged;

         Items.Clear();

         ExecuteUsingServices(services =>
         {
             foreach (ICashMovement item in GetItems(services))
             {
                 ICashMovementPresenter<TCategory> presenter = CreatePresenterFor(item);
                 if (Category != null)
                 {
                     presenter.Category = Category;
                 }

                 presenter.PropertyChanged += OnPresenterPropertyChanged;

                 Items.Add(presenter);
             }
         });
     }

     // </summary>
     ////     Gets the application services used to send the data operations originated in the current
     ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
     /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
     protected override ICashMovementManagerApplicationServices CreateServices()
     {
         ICashMovementManagerApplicationServices services = base.CreateServices();
         services.Category = Category.Object;

         return services;
     }

     /// <summary>
     ///     Creates a presenter view model for the given budget component item.
     /// </summary>
     /// <param name="budgetComponentItem">The budget component item to get decorated in a new presenter view model.</param>
     /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem" /> is null.</exception>
     /// <returns>A new instance of <typeparamref name="TPresenter" /> containing <paramref name="budgetComponentItem" />.</returns>
     protected override ICashMovementPresenter<TCategory> CreatePresenterFor(ICashMovement budgetComponentItem)
     {
         if (budgetComponentItem == null)
             throw new ArgumentNullException("ICashMovementCategoryPresenter");

         ICashMovementPresenter<TCategory> presenter = base.CreatePresenterFor(budgetComponentItem);
         presenter.Category = Category;
         presenter.Object.CashMovementCategory = Category.Object;


         return presenter;
     }
    }
}
