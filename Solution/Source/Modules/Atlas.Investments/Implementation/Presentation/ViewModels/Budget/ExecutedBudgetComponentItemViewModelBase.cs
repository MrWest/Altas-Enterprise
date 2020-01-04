using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Commands;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    ///     Implementation of the contract <see cref="IExecutedBudgetComponentItemViewModel" />, representing a crud view model
    ///     managing executed budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the executed budget component items managed.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view model used to decorate and impersonate the items.</typeparam>
    /// <typeparam name="TComponent">
    ///     The budget component to which belong the executed items managed in the current crud view model.
    /// </typeparam>
    /// <typeparam name="TServices">The application services used to communicate with the system core.</typeparam>
    //public abstract class ExecutedBudgetComponentItemViewModelBase< TPresenter, TComponent, TServices> :
    //    BudgetComponentItemViewModelBase<IExecutedActivity, TPresenter,  TServices>,
    //    IExecutedBudgetComponentItemViewModel<TPresenter, TComponent>
    //    //where TItem : class, IExecutedBudgetComponentItem
    //    where TPresenter : class, IBudgetComponentItemPresenter<IExecutedActivity>
    //    where TComponent : class, IBudgetComponent
    //    where TServices : class ,IExecutedBudgetComponentItemManagerApplicationServices<TComponent>
    //{
    //    private IBudgetComponentPresenter<TComponent> _component;
    //    public IBudgetComponentPresenter<TComponent> Component
    //    {
    //        get
    //        {
    //            if (_component == null)
    //                throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

    //            return _component;
    //        }
    //        set
    //        {
    //            if (value == null)
    //                throw new ArgumentNullException("value");

    //            _component = value;
    //        }
    //    }
    //    /// <summary>
    //    ///     Initializes a new instance of
    //    ///     <see cref="ExecutedBudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
    //    /// </summary>
    //    protected ExecutedBudgetComponentItemViewModelBase()
    //    {
    //        ExecutePlannedItemsCommand = new DelegateCommand<IList>(ExecutePlannedItemsCommand_Executed, ExecutePlannedItemsCommand_CanExecute);
    //    }


    //    /// <summary>
    //    ///     Gets the command that allows to executed specified planned items.
    //    /// </summary>
    //    public ICommand ExecutePlannedItemsCommand { get; private set; }


    //    private bool ExecutePlannedItemsCommand_CanExecute(IList list)
    //    {
    //        IPlannedActivity[] plannedItems;
    //        return TryGetPlannedItems(list, out plannedItems) && ExecuteUsingServices(services => services.CanBeExecute(plannedItems));
    //    }

    //    private void ExecutePlannedItemsCommand_Executed(IList list)
    //    {
    //        IPlannedActivity[] plannedItems;
    //        if (!TryGetPlannedItems(list, out plannedItems))
    //            return;

    //        // Command to execute them
    //        IEnumerable<IExecutedActivity> executedItems = ExecuteUsingServices(services => services.BeExecuted(plannedItems)).ToArray();

    //        // Then, create for each of the executed item a presenter and add them to the current crud view model
    //        foreach (var executedItem in executedItems)
    //        {
    //            TPresenter presenter = CreatePresenterFor(executedItem);
    //            presenter.PropertyChanged += OnPresenterPropertyChanged;
    //            Items.Add(presenter);
    //        }

    //        // And notify the user
    //        Notify(Resources.ExecutedThisManyItems.EasyFormat(executedItems.Count()), Resources.ExecutionSummary);
    //    }


    //    private static bool TryGetPlannedItems(IList arg, out IPlannedActivity[] plannedItems)
    //    {
    //        plannedItems =
    //            (from presenter in (arg ?? new object[0]).OfType<IPresenter>()
    //                select presenter.Object)
    //                .OfType<IPlannedActivity>()
    //                .ToArray();

    //        return plannedItems.Any() && arg != null && plannedItems.Count() == arg.Count;
    //    }
    //}
}