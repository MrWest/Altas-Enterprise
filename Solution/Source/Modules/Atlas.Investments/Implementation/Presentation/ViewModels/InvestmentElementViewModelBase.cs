using System;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
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
using Microsoft.Practices.Unity;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Base class of the crud view model with the responsibility of handling the investment element operations in the
    ///     presentation layers, and which also will be bound to the items controls listing, adding, etc the investment
    ///     elements in the system.
    /// </summary>
    public abstract class InvestmentElementViewModelBase<T, TPresenter, TServices> :
        NavigableViewModel<T, TPresenter, TServices>
        where T : class, IInvestmentElement
        where TPresenter : class, IInvestmentElementPresenter, IPresenter<T>
        where TServices : class , IItemManagerApplicationServices<T>
   
       
    {
        /// <summary>
        ///     Creates a new presenter view model decorating the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to wrap into an presenter view model.</param>
        /// <returns>
        ///     A new instance of <see cref="IInvestmentElementPresenter" /> decorating
        ///     <paramref name="investmentElement" />.
        /// </returns>
        protected override TPresenter CreatePresenterFor(T investmentElement)
        {
            //var shit = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;
            TPresenter presenter = base.CreatePresenterFor(investmentElement);

            //var g = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;

            var budget = ServiceLocator.Current.GetInstance<IBudgetPresenter>();
            budget.Object = investmentElement.Budget;

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

            var workcapitalcashflow = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlowPresenter>();
          
            // workcapitalcashflow.Object = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;
            workcapitalcashflow.WorkCapitalComponent = workCapital;
            workcapitalcashflow.Object = workCapital.Object.WorkCapitalCashFlow ?? ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapital.PlannedWorkCapitalCashFlow = workcapitalcashflow;
            //for new domain
             var cashentry = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashEntry>>();
            cashentry.Object = workcapitalcashflow.Object.CashEntries;
            cashentry.WorkCapitalCashFlowPresenter = workcapitalcashflow;
            workcapitalcashflow.CashEntries = cashentry;

            var cashoutgoing = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashOutgoing>>();
            cashoutgoing.Object = workcapitalcashflow.Object.CashOutgoings;
            cashoutgoing.WorkCapitalCashFlowPresenter = workcapitalcashflow;
            workcapitalcashflow.CashOutgoings = cashoutgoing;


            var workcapitalcashflow2 = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlowPresenter>();
            // workcapitalcashflow.Object = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;
            workcapitalcashflow2.WorkCapitalComponent = workCapital;
            workcapitalcashflow2.Object = workCapital.Object.ExecutedWorkCapitalCashFlow ?? ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapital.ExecutedWorkCapitalCashFlow = workcapitalcashflow2;

            var cashentry2 = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashEntry>>();
            cashentry2.Object = workcapitalcashflow2.Object.CashEntries;
            cashentry2.WorkCapitalCashFlowPresenter = workcapitalcashflow2;
            workcapitalcashflow2.CashEntries = cashentry2;

            var cashoutgoing2 = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashOutgoing>>();
            cashoutgoing2.Object = workcapitalcashflow2.Object.CashOutgoings;
            cashoutgoing2.WorkCapitalCashFlowPresenter = workcapitalcashflow2;
            workcapitalcashflow2.CashOutgoings = cashoutgoing2;
           
            
            budget.WorkCapitalComponent = workCapital;
            budget.Object.WorkCapitalComponent = workCapital.Object;
            
            budget.InvestmentElement = presenter;
            presenter.Budget = budget;

            var period = ServiceLocator.Current.GetInstance<IPeriodPresenter>();
            
            period.Object = investmentElement.Period;
           // period.Object.Holder = presenter.Object as IEntity;
            period.Holder = presenter;
            presenter.Period = period;
           
               // presenter.Period = ServiceLocator.Current.GetInstance<IPeriod>();
        

            
            return presenter;
        }

        public override bool CanDelete(TPresenter presenter)
        {
            return true;
        }
        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="investmentElement">The investment element presenter that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(TPresenter investmentElement)
        {
            return Resources.SureToDeleteInvestmentElement.EasyFormat(investmentElement);
        }

        /// <summary>
        ///     Gets the confirmation message that is show when there is added a new investment element.
        /// </summary>
        /// <param name="investmentElement">
        ///     The investment element presenter containing the added <see cref="IInvestmentElement" />.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> representing the message notifying that a new <see cref="IInvestmentElement" /> has been
        ///     added.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(TPresenter investmentElement)
        {
            return Resources.SuccessfullyAddedInvestmentElement.EasyFormat(investmentElement);
        }

        /// <summary>
        ///     Gets the confirmation message that is show when there is deleted a new investment element.
        /// </summary>
        /// <param name="investmentElement">
        ///     The investment element presenter containing the deleted <see cref="IInvestmentElement" />.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> representing the message notifying that a new <see cref="IInvestmentElement" /> has been
        ///     deleted.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(TPresenter investmentElement)
        {
            return Resources.SuccessfullyDeletedInvestmentElement.EasyFormat(investmentElement);
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

            ItemEventArgs<TPresenter> arguments;
            if (!CheckIsItemEventArgs(e, out arguments))
                return;

            //IInvestmentElementPresenter presenter = arguments.Item;

            //// Initialize the component of the budget component items component
            //IEquipmentComponentPresenter equipment = presenter.Budget.EquipmentComponent;
            //IConstructionComponentPresenter construction = presenter.Budget.ConstructionComponent;
            //IOtherExpensesComponentPresenter otherExpenses = presenter.Budget.OtherExpensesComponent;
            //IWorkCapitalComponentPresenter workCapital = presenter.Budget.WorkCapitalComponent;

           // presenter.Budget.EquipmentComponent.PlannedResources.Component = equipment;
            //presenter.Budget.EquipmentComponent.PlannedActivities.Component = equipment;
          //  presenter.Budget.EquipmentComponent.ExecutedResources.Component = equipment;
            //presenter.Budget.EquipmentComponent.ExecutedActivities.Component = equipment;

         //   presenter.Budget.ConstructionComponent.PlannedResources.Component = construction;
            //presenter.Budget.ConstructionComponent.PlannedActivities.Component = construction;
          //  presenter.Budget.ConstructionComponent.ExecutedResources.Component = construction;
            //presenter.Budget.ConstructionComponent.ExecutedActivities.Component = construction;

         //   presenter.Budget.OtherExpensesComponent.PlannedResources.Component = otherExpenses;
            //presenter.Budget.OtherExpensesComponent.PlannedActivities.Component = otherExpenses;
         //   presenter.Budget.OtherExpensesComponent.ExecutedResources.Component = otherExpenses;
            ////presenter.Budget.OtherExpensesComponent.ExecutedActivities.Component = otherExpenses;

         //   presenter.Budget.WorkCapitalComponent.PlannedResources.Component = workCapital;
            //presenter.Budget.WorkCapitalComponent.PlannedActivities.Component = workCapital;
          //  presenter.Budget.WorkCapitalComponent.ExecutedResources.Component = workCapital;
            //presenter.Budget.WorkCapitalComponent.ExecutedActivities.Component = workCapital;
        }

       
    }
}