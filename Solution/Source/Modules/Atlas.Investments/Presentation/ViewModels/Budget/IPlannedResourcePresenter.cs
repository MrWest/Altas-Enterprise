using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract for the presenter view models decorating and impersonating in the UI the planned resource of a certain
    /// budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated planned resource.</typeparam>
    public interface IPlannedResourcePresenter<TComponent> :
        IBudgetComponentItemPresenter<IPlannedResource>, IPlannedResourcePresenter
        where TComponent : class , IBudgetComponentItem
    {

        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        IBudgetComponentItemPresenter<TComponent> Component { get; set; }

       
    }


    /// <summary>
    /// Base contract for the presenter view models decorating and impersonating in the UI the planned resource of a certain
    /// budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated planned resource.</typeparam>
    public interface IPlannedResourcePresenter: IPeriodCalculator
    {
        /// <summary>
        /// Gets or sets the Norm for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        decimal Norm { get; set; }
        
        /// <summary>
        /// Gets or sets the <see cref="IWeightPresenter"/> instace for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        IWeightPresenter Weight { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IVolumePresenter"/> instace for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        IVolumePresenter Volume { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IVolumePresenter"/> instace for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        ResourceKind ResourceKind { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IVolumePresenter"/> instace for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        decimal WasteCoefficient { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IVolumePresenter"/> instace for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        IWageScalePresenter WageScale { get; set; }

        ///<summary>
        ///  Gets or sets the Men Number for the current <see cref="IPlannedResource"/>.
        /// </summary>
        int MenNumber { get; set; }

     //   ICommand DeleteMySelfCommand { get; }

    }


}
