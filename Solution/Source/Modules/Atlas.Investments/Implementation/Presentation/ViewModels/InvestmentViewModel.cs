using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentViewModel" /> being the crud view model handling the crud
    ///     operations in the presentation layer for investment entities.
    /// </summary>
    public class InvestmentViewModel :
        InvestmentElementViewModelBase<IInvestment, IInvestmentPresenter, IInvestmentManagerApplicationServices>,
        IInvestmentViewModel, IInvestmentProvider
    {
        private static IInvestmentProvider _InvestmentProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IInvestmentPresenter> Investments
        {
            get
            {
                if (_InvestmentProvider == null)
                    _InvestmentProvider = ServiceLocator.Current.GetInstance<IInvestmentProvider>();

                return _InvestmentProvider.Investments;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IInvestmentPresenter> IInvestmentProvider.Investments
        {
            get
            {
                Load();
                return Items;
            }
        }
    }
}