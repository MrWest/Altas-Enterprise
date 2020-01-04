using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// Implements CRUD operations over a casha movement
    /// </summary>
    /// <typeparam name="TItem">Cash entry category entity</typeparam>
    public class CashMovementCategoryViewModel<TItem> : CrudViewModelBase<TItem, ICashMovementCategoryPresenter<TItem>, ICashMovementCategoryManagerApplicationServices<TItem>>, ICashMovementCategoryViewModel<TItem>
    where TItem : class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        public ICashMovementCategoryPresenter<TItem> SuperiorCategory { get; set; }

        //public void DeleteLiquity(TItem liquity)
        //{
        //    CreateServices().Delete(liquity);
        //}

      

        /// <summary>
        ///     Adds a new budget component items presenter to the current view model.
        /// </summary>
        /// <param name="presenter">The budget component item to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="presenter" /> is null.</exception>
        public override void Add(ICashMovementCategoryPresenter<TItem> presenter)
        {
            base.Add(presenter);
            Load();

        }
        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            base.OnDeletedItem(sender, e);
            SuperiorCategory.TellYourFather();
            SuperiorCategory.IsExpanded = true;
        }
       

        /// <summary>
        ///     Makes some initializations to the added investment element and its components. This method is called when an
        ///     investment element
        ///     is added.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The information of the investment element addition.</param>
        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender,e);
            SuperiorCategory.TellYourFather();
            SuperiorCategory.IsExpanded = true;
        }

        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (ICashMovementCategoryPresenter<TItem> presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (TItem item in GetItems(services))
                {
                    ICashMovementCategoryPresenter<TItem> presenter = CreatePresenterFor(item);
                   
                    presenter.SuperiorCategory = SuperiorCategory;
                   
                    
                    presenter.PropertyChanged += OnPresenterPropertyChanged;

                  
                    Items.Add(presenter);
                 
                }
            });
        }
        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override ICashMovementCategoryManagerApplicationServices<TItem> CreateServices()
        {
            ICashMovementCategoryManagerApplicationServices<TItem> services = base.CreateServices();
            services.SuperiorCategory = SuperiorCategory.Object;
            
            return services;
        }

        /// <summary>
        ///     Creates a presenter view model for the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item to get decorated in a new presenter view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem" /> is null.</exception>
        /// <returns>A new instance of <typeparamref name="TPresenter" /> containing <paramref name="budgetComponentItem" />.</returns>
        protected override ICashMovementCategoryPresenter<TItem> CreatePresenterFor(TItem budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("ICashMovementCategoryPresenter");

            ICashMovementCategoryPresenter<TItem> presenter = base.CreatePresenterFor(budgetComponentItem);
            presenter.SuperiorCategory = SuperiorCategory;
            presenter.Object.SuperiorCategory = SuperiorCategory.Object;


            return presenter;
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(ICashMovementCategoryPresenter<TItem> presenter)
        {
            return Resources.SureToDeleteCashMovement.EasyFormat(presenter);
        }

        public override bool CanDelete(ICashMovementCategoryPresenter<TItem> presenter)
        {
            return true;
        }
    }
}
