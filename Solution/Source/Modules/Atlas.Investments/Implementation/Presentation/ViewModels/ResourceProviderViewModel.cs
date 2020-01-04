using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class ResourceProviderViewModel:CrudViewModelBase<IResourceProvider, IResourceProviderPresenter, IResourceProviderManagerApplicationServices>, IResourceProviderViewModel
    {
        private static IResourceProviderProvider _currencyProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IResourceProviderPresenter> Providers
        {
            get
            {
                if (_currencyProvider == null)
                    _currencyProvider = ServiceLocator.Current.GetInstance<IResourceProviderProvider>();

                return _currencyProvider.Providers;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IResourceProviderPresenter> IResourceProviderProvider.Providers
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