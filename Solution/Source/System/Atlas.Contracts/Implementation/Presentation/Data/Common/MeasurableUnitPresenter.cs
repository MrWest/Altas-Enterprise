using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public abstract class MeasurableUnitPresenter<TMeasurable> : EntityPresenterBase<TMeasurable, IMeasurableUnitManagerApplicationServices<TMeasurable>>, IMeasurableUnitPresenter<TMeasurable> 
        where TMeasurable : class ,IMeasurableUnit
    {
        private IPresenter _holder;

      
        public IPresenter Holder
        {
            get { return _holder; }
            set
            {
                _holder = value;
                //if(_holder!=null)
                //    Object = CreateServices().Items.FirstOrDefault();
            }
        }
        /// <summary>
        /// Gets or sets the measurement unit
        /// </summary>
        public IMeasurementUnitPresenter MeasurementUnit
        {
            get
            {

                return Object.MeasurementUnit != null ? ServiceLocator.Current.GetInstance<IMeasurementUnitProvider>().MeasurementUnits.SingleOrDefault(x => x.Id.ToString() == Object.MeasurementUnit.ToString()) : null;

            }
            set
            {
                var measurement = value as IMeasurementUnitPresenter;
                if (measurement != null)
                {

                    SetProperty(v => Object.MeasurementUnit = v, measurement.Id);
                    OnPropertyChanged(() => MeasurementUnit);
                }
            }
        }

        public decimal Amount
        {
            get { return Object.Amount; }
            set
            {
                SetProperty(v => Object.Amount = v, value);
                OnPropertyChanged(() => Amount);
            }
        }

        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override IMeasurableUnitManagerApplicationServices<TMeasurable> CreateServices()
        {
            IMeasurableUnitManagerApplicationServices<TMeasurable> services = base.CreateServices();
            services.Holder = Holder.Object as IEntity;

            return services;
        }

        public override void Notify()
        {
            base.Notify();
            var measurement = Object.MeasurementUnit;
            Object.MeasurementUnit = null;
            OnPropertyChanged(() => MeasurementUnit);
            Object.MeasurementUnit = measurement;
            OnPropertyChanged(() => MeasurementUnit);
        }
    }
}
