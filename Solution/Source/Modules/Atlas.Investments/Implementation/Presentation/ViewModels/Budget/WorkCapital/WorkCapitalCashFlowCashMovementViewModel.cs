using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    public class WorkCapitalCashFlowCashMovementCategoryViewModel<TItem> : CrudViewModelBase<TItem, IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem>, ICashFlowCashMovementCategoryManagerApplicationServices<TItem>>, IWorkCapitalCashFlowCashMovementCategoryViewModel<TItem> 
        where TItem : class ,ICashMovementCategory
    {
        public IWorkCapitalCashFlowPresenter WorkCapitalCashFlow { get; set; }

        /// <summary>
        ///     Adds a new budget component items presenter to the current view model.
        /// </summary>
        /// <param name="presenter">The budget component item to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="presenter" /> is null.</exception>
        public override void Add(IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem> presenter)
        {
            base.Add(presenter);
            Load();

        }
        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            base.OnDeletedItem(sender, e);
            WorkCapitalCashFlow.TellYourFather();
           // SuperiorCategory.IsExpanded = true;
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
            base.OnAddedItem(sender, e);
            WorkCapitalCashFlow.TellYourFather();
           // WorkCapitalCashFlow.IsExpanded = true;
        }

        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem> presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (TItem item in GetItems(services))
                {
                    IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem> presenter = CreatePresenterFor(item);

                    presenter.WorkCapitalCashFlowPresenter = WorkCapitalCashFlow;


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
        protected override ICashFlowCashMovementCategoryManagerApplicationServices<TItem> CreateServices()
        {
            ICashFlowCashMovementCategoryManagerApplicationServices<TItem> services = base.CreateServices();
            services.WorkCapitalCashFlow = WorkCapitalCashFlow.Object;

            return services;
        }

        /// <summary>
        ///     Creates a presenter view model for the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item to get decorated in a new presenter view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem" /> is null.</exception>
        /// <returns>A new instance of <typeparamref name="TPresenter" /> containing <paramref name="budgetComponentItem" />.</returns>
        protected override IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem> CreatePresenterFor(TItem budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("ICashMovementCategoryPresenter");

            IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem> presenter = base.CreatePresenterFor(budgetComponentItem);
            presenter.WorkCapitalCashFlowPresenter = WorkCapitalCashFlow;
            presenter.Object.SuperiorCategory = WorkCapitalCashFlow.Object;


            return presenter;
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem> presenter)
        {
            return Resources.SureToDeleteCashMovement.EasyFormat(presenter);
        }
    }
}
