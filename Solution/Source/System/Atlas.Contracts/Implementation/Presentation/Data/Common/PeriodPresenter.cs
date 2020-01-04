using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class PeriodPresenter : EntityPresenterBase<IPeriod, IPeriodManagerApplicationServices>, IPeriodPresenter
    {
        private IPresenter _holder;
       

        //public PeriodPresenter()
        //{
           
        //}
        //public PeriodPresenter(IPresenter holderEntity)
        //{
        //    if (holderEntity == null)
        //        throw new NullReferenceException("holderEntity");

        //    _holder = holderEntity;
        //     Object = CreateServices().GetPeriod();
        //}
        public IPresenter Holder
        {
            get { return _holder; }
            set
            {
                _holder = value;
               // Object = CreateServices().GetPeriod(_holder.Object as IEntity) ?? Object;
            }
        }


        public DateTime Starts
        {
            get
            {
                if (Holder != null && Holder.GetType().Implements<IPeriodCalculator>())
                    return ((IPeriodCalculator)Holder).StartDate();
                return Object.Starts;
            }
            set
            {
                SetProperty(v => Object.Starts = v, value);

              //  OnPropertyChanged(() => Starts);
                if (Holder != null && Holder.GetType().Implements<IPeriodCalculator>())
                {
                    ((IPeriodCalculator)Holder).StartCalculated = false;
                    OnPropertyChanged(() => Starts);
                    ((IPeriodCalculator)Holder).EndCalculated = false;
                    OnPropertyChanged(() => Ends);

                }
            }
        }

        public DateTime Ends
        {
            get
            {
                //get the finish date by asking to the period holder
                if (Holder != null && Holder.GetType().Implements<IPeriodCalculator>())
                    return ((IPeriodCalculator)Holder).FinishDate();
                return Object.Ends;
            }
            set
            {
                SetProperty(v => Object.Ends = v, value);
                if (Holder != null && Holder.GetType().Implements<IPeriodCalculator>())
                {
                    ((IPeriodCalculator)Holder).EndCalculated = false;
                   
                }
                   
                OnPropertyChanged(() => Ends);
            }
        }

        public int Days { get { return Object.Days; } }
        public DateTime OriStart()
        {
            return Object.Starts; 
        }

        public DateTime OriEnd()
        {
            return Object.Ends;
        }

        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override IPeriodManagerApplicationServices CreateServices()
        {
            IPeriodManagerApplicationServices services = base.CreateServices();
            services.Holder =  Holder.Object as IEntity;

            return services;
        }

        public override void Notify()
        {
            base.Notify();
            OnPropertyChanged(()=>Starts);
            OnPropertyChanged(() => Ends);
        }

        public bool IsContained(IPeriod period)
        {
            return Starts.CompareTo(period.Starts) <= 0;
        }

        public string ShortStarts => Starts.ToShortDateString();
        public string ShortEnds => Ends.ToShortDateString();
    }


}
