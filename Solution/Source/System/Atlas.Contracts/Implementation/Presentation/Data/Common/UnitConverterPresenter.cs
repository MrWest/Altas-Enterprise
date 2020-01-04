using System.Linq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public  class UnitConverterPresenter<TEntity>: EntityPresenterBase<IUnitConverter,IUnitConverterManagerApplicationServices>,IUnitConverterPresenter<TEntity>
     where TEntity : class ,IConvertibleEntity
    {
        private IConvertibleEntityPresenter<TEntity> _conversionForEntity;
        public IConvertibleEntityPresenter<TEntity> ConversionForEntity
        {
            get { return _conversionForEntity; }
            set { _conversionForEntity = value; }
        }


       public  IConvertibleEntityPresenter<TEntity> ConversionUnit
        {
            get { return Object.ConversionUnit != null ? ServiceLocator.Current.GetInstance<IConvertibleEntityProvider<TEntity>>().ConvertibleEntities.SingleOrDefault(x => x.Id.ToString() == Object.ConversionUnit.ToString()) : null; }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.ConversionUnit = v.Object.Id, value);
                    OnPropertyChanged(() => ConversionUnit);
                }
               
            }
        }
     

        public decimal Factor
        {
            get { return Object.Factor; }
            set
            {
                SetProperty(v => Object.Factor = v, value);
                OnPropertyChanged(() => Factor);
            }
        }

        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override IUnitConverterManagerApplicationServices CreateServices()
        {
            IUnitConverterManagerApplicationServices services = base.CreateServices();
            services.ConversionForEntity = ConversionForEntity.Object;

            return services;
        }
    }
}
