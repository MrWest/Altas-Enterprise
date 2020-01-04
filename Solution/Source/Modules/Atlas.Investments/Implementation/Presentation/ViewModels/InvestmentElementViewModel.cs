using System;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// Default implementation of the contract <see cref="IInvestmentElementViewModel"/>, representing the contract defined
    /// for the crud view model with the responsibility of handling the investment element operations in the presentation
    /// layers, and which also will be bound to the items controls listing, adding, etc the investment elements in the
    /// system.
    /// </summary>
    public class InvestmentElementViewModel :
        CrudViewModelBase<IInvestmentElement, IInvestmentElementPresenter, IInvestmentElementManagerApplicationServices>,
        IInvestmentElementViewModel
    {
        /// <summary>
        /// Gets or sets the <see cref="IInvestmentElementPresenter"/> containing the <see cref="IInvestmentElement"/>
        /// being the parent of the ones managed in the current <see cref="IInvestmentElementViewModel"/>.
        /// </summary>
        public IInvestmentElementPresenter Parent { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IInvestmentElementPeriodPresenter"/> containing the <see cref="IPeriod"/>
        /// for this element.
        /// </summary>
      //  public IPeriodPresenter Period { get; set; }
        /// <summary>
        /// Creates a new instance of the application services that this class will use.
        /// </summary>
        /// <returns>An instance of <see cref="IInvestmentElementManagerApplicationServices"/>.</returns>
        protected override IInvestmentElementManagerApplicationServices CreateServices()
        {
            IInvestmentElementManagerApplicationServices services = base.CreateServices();
            services.Parent = Parent != null ? Parent.Object : null;

            return services;
        }

        /// <summary>
        /// Creates a new presenter view model decorating the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to wrap into an presenter view model.</param>
        /// <returns>A new instance of <see cref="IInvestmentElementPresenter"/> decorating <paramref name="investmentElement"/>.</returns>
        protected override IInvestmentElementPresenter CreatePresenterFor(IInvestmentElement investmentElement)
        {
            IInvestmentElementPresenter presenter = base.CreatePresenterFor(investmentElement);
            presenter.Parent = Parent;

            IBudgetPresenter budget = ServiceLocator.Current.GetInstance<IBudgetPresenter>();
            budget.Object = investmentElement.Budget;
            budget.InvestmentElement = presenter;
            presenter.Budget = budget;

            var equipment = ServiceLocator.Current.GetInstance<IEquipmentComponentPresenter>();
            equipment.Object = budget.Object.EquipmentComponent;
            equipment.Budget = budget;
            budget.EquipmentComponent = equipment;

            var construction = ServiceLocator.Current.GetInstance<IConstructionComponentPresenter>();
            construction.Object = budget.Object.ConstructionComponent;
            construction.Budget = budget;
            budget.ConstructionComponent = construction;

            var otherExpenses = ServiceLocator.Current.GetInstance<IOtherExpensesComponentPresenter>();
            otherExpenses.Object = budget.Object.OtherExpensesComponent;
            otherExpenses.Budget = budget;
            budget.OtherExpensesComponent = otherExpenses;

            var workCapital = ServiceLocator.Current.GetInstance<IWorkCapitalComponentPresenter>();
            workCapital.Object = budget.Object.WorkCapitalComponent;
            workCapital.Budget = budget;
            budget.WorkCapitalComponent = workCapital;

            // Besides

            var period = ServiceLocator.Current.GetInstance<IInvestmentElementPeriodPresenter>();
            period.Object = investmentElement.Period;
            period.InvestmentElement = presenter;
            presenter.Period = period;

            return presenter;
        }

        /// <summary>
        /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        /// with the operation.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElementPresenter"/> that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IInvestmentElementPresenter investmentElement)
        {
            return Resources.SureToDeleteInvestmentElement.EasyFormat(investmentElement);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is added a new investment element.
        /// </summary>
        /// <param name="investmentElement">
        /// The <see cref="IInvestmentElementPresenter"/> containing the added <see cref="IInvestmentElement"/>.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new <see cref="IInvestmentElement"/> has been added.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IInvestmentElementPresenter investmentElement)
        {
            return Resources.SuccessfullyAddedInvestmentElement.EasyFormat(investmentElement);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is deleted a new investment element.
        /// </summary>
        /// <param name="investmentElement">
        /// The <see cref="IInvestmentElementPresenter"/> containing the deleted <see cref="IInvestmentElement"/>.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new <see cref="IInvestmentElement"/> has been deleted.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IInvestmentElementPresenter investmentElement)
        {
            return Resources.SuccessfullyDeletedInvestmentElement.EasyFormat(investmentElement);
        }

        /// <summary>
        /// Makes some initializations to the added investment element and its components. This method is called when an investment element
        /// is added.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The information of the investment element addition.</param>
        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);

            ItemEventArgs<IInvestmentElementPresenter> arguments;
            if (!CheckIsItemEventArgs(e, out arguments))
                return;

            IInvestmentElementPresenter presenter = arguments.Item;

            // Initialize the component of the budget component items component
            IEquipmentComponent equipment = presenter.Object.Budget.EquipmentComponent;
            IConstructionComponent construction = presenter.Object.Budget.ConstructionComponent;
            IOtherExpensesComponent otherExpenses = presenter.Object.Budget.OtherExpensesComponent;
            IWorkCapitalComponent workCapital = presenter.Object.Budget.WorkCapitalComponent;
            
            presenter.Budget.EquipmentComponent.PlannedResources.Component = equipment;
            presenter.Budget.EquipmentComponent.PlannedActivities.Component = equipment;
            presenter.Budget.EquipmentComponent.ExecutedResources.Component = equipment;
            presenter.Budget.EquipmentComponent.ExecutedActivities.Component = equipment;

            presenter.Budget.ConstructionComponent.PlannedResources.Component = construction;
            presenter.Budget.ConstructionComponent.PlannedActivities.Component = construction;
            presenter.Budget.ConstructionComponent.ExecutedResources.Component = construction;
            presenter.Budget.ConstructionComponent.ExecutedActivities.Component = construction;

            presenter.Budget.OtherExpensesComponent.PlannedResources.Component = otherExpenses;
            presenter.Budget.OtherExpensesComponent.PlannedActivities.Component = otherExpenses;
            presenter.Budget.OtherExpensesComponent.ExecutedResources.Component = otherExpenses;
            presenter.Budget.OtherExpensesComponent.ExecutedActivities.Component = otherExpenses;

            presenter.Budget.WorkCapitalComponent.PlannedResources.Component = workCapital;
            presenter.Budget.WorkCapitalComponent.PlannedActivities.Component = workCapital;
            presenter.Budget.WorkCapitalComponent.ExecutedResources.Component = workCapital;
            presenter.Budget.WorkCapitalComponent.ExecutedActivities.Component = workCapital;
        }
    }
}
