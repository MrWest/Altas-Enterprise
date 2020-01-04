using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// describes crud operations for a <see cref="IExecution"/> entity
    /// </summary>
    public class ExecutionViewModel : CrudViewModelBase<IExecution, IExecutionPresenter,IExecutionManagerApplicationServices>, IExecutionViewModel
     ////where TComponent :class, IBudgetComponent
    {
        private IExecutedActivityPresenter _activity;

        public IExecutedActivityPresenter ExecutedActivity
        {
            get { return _activity; } 
            set { _activity = value; }
        }

        ///// <summary>
        /////     Adds a new budget component items presenter to the current view model.
        ///// </summary>
        ///// <param name="presenter">The budget component item to add.</param>
        ///// <exception cref="ArgumentNullException"><paramref name="presenter" /> is null.</exception>
        //public override void Add(IExecutionPresenter<TComponent> presenter)
        //{
        //    base.Add(presenter);
        //    Load();

        //}



        // <summary>
        //     Makes some initializations to the added investment element and its components. This method is called when an
        //     investment element
        //     is added.
        // </summary>
        // <param name="sender">The object sending the event invoking this method.</param>
        // <param name="e">The information of the investment element addition.</param>
        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
            ExecutedActivity.Notify();
        }

        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            base.OnDeletedItem(sender, e);
            ExecutedActivity.Notify();
        }

        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (IExecutionPresenter presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (IExecution item in GetItems(services))
                {
                    IExecutionPresenter presenter = CreatePresenterFor(item);

                    presenter.ExecutedActivity = ExecutedActivity;

                    presenter.PropertyChanged += OnPresenterPropertyChanged;
                    
                    Items.Add(presenter);
                }
            });
        }
        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponenTComponentViewModelBase{TComponent,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override IExecutionManagerApplicationServices CreateServices()
        {
            IExecutionManagerApplicationServices services = base.CreateServices();
            services.ExecutedActivity = ExecutedActivity.Object;

            return services;
        }

        /// <summary>
        ///     Creates a presenter view model for the given budget component item.
        /// </summary>
        /// <param name="execution">The budget component item to get decorated in a new presenter view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execution" /> is null.</exception>
        /// <returns>A new instance of <typeparamref name="TPresenter" /> containing <paramref name="execution" />.</returns>
        protected override IExecutionPresenter CreatePresenterFor(IExecution execution)
        {
            if (execution == null)
                throw new ArgumentNullException("IExecutionPresenter");

            IExecutionPresenter presenter = base.CreatePresenterFor(execution);
            presenter.ExecutedActivity = ExecutedActivity;
            presenter.Object.ExecutedActivity = ExecutedActivity.Object;


            return presenter;
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IExecutionPresenter presenter)
        {
            return Resources.SureToDeleteExecution.EasyFormat(presenter);
        }

       
    }
}
