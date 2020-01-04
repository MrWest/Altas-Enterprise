using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class ResourceSupplierViewModel:CrudViewModelBase<IResourceSupplier, IResourceSupplierPresenter, IResourceSupplierManagerApplicationServices>, IResourceSupplierViewModel
    {
        private static IResourceSupplierProvider _currencyProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IResourceSupplierPresenter> Suppliers
        {
            get
            {
                if (_currencyProvider == null)
                    _currencyProvider = ServiceLocator.Current.GetInstance<IResourceSupplierProvider>();

                return _currencyProvider.Suppliers;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IResourceSupplierPresenter> IResourceSupplierProvider.Suppliers
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