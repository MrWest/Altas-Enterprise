using System;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class UnitConverterViewModel<TEntity>: CrudViewModelBase<IUnitConverter, IUnitConverterPresenter<TEntity>,IUnitConverterManagerApplicationServices>,IUnitConverterViewModel<TEntity>
        where TEntity : class ,IConvertibleEntity
    {
        private IConvertibleEntityPresenter<TEntity> _convertibleEntityPresenter;
        public IConvertibleEntityPresenter<TEntity> ConvertibleEntityPresenter { 
            get{return _convertibleEntityPresenter;}
            set { _convertibleEntityPresenter = value; } }


        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override IUnitConverterManagerApplicationServices CreateServices()
        {
            IUnitConverterManagerApplicationServices services = base.CreateServices();
            services.ConversionForEntity = ConvertibleEntityPresenter.Object;

            return services;
        }

        
        /// <summary>
        ///     Creates a presenter view model for the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item to get decorated in a new presenter view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem" /> is null.</exception>
        /// <returns>A new instance of <typeparamref name="TPresenter" /> containing <paramref name="budgetComponentItem" />.</returns>
        protected override IUnitConverterPresenter<TEntity> CreatePresenterFor(IUnitConverter budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("IUnitConverterPresenter");

            IUnitConverterPresenter<TEntity> presenter = base.CreatePresenterFor(budgetComponentItem);
            presenter.ConversionForEntity = ConvertibleEntityPresenter;
            presenter.Object.ConversionForEntity = ConvertibleEntityPresenter.Object;


            return presenter;
        }
    }


}
