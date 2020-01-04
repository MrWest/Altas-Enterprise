using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class ActivityExecutorViewModel: CrudViewModelBase<IActivityExecutor, IActivityExecutorPresenter, IActivityExecutorManagerApplicationServices>, IActivityExecutorViewModel
    {
        private static IActivityExecutorProvider _currencyProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IActivityExecutorPresenter> Executors
        {
            get
            {
                if (_currencyProvider == null)
                    _currencyProvider = ServiceLocator.Current.GetInstance<IActivityExecutorProvider>();

                return _currencyProvider.Executors;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IActivityExecutorPresenter> IActivityExecutorProvider.Executors
        {
            get
            {
                if (!IsLoaded)
                    Load();
                return Items;
            }
        }
    }
}