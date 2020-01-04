using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    /// Implements the functionalities for a (<see cref="IInvestmentElementPeriodPresenter"/>
    /// </summary>
    public class InvestmentElementPeriodPresenter:  EntityPresenterBase<IInvestmentElementPeriod, IInvestmentElementPeriodManagerApplicationServices<IInvestmentElementPeriod>>, IInvestmentElementPeriodPresenter
    {

        /// <summary>
        /// InvestmentElementPeriodPresenter contructor.
        /// </summary>
        public InvestmentElementPeriodPresenter(IInvestmentElementPeriod period)
            : base(period)
        {
            Object = period;
            Update = true;

        }
        /// <summary>
        /// Gets or sets the unique identifier of the current entity.
        /// </summary>
        //public override  object Id
        //{
        //    get { return Object.Id; }
        //    set { SetProperty(v => Object.Id = v, value); }
        //}

        //public override IInvestmentElementPeriod Object { get; set; }
        


        /// <summary>
        /// Gets or sets the presenter view model containing the investment element to which belong the period decorated by the current
        /// period presenter.
        /// </summary>
        public IInvestmentElementPresenter InvestmentElement { get; set; }
        public DateTimeScale PeriodKind
        {
            get { return Object.PeriodKind; }
            set { Object.PeriodKind = value; }
        }


        public DateTime Starts
        {
            get
            {
                return Object.Starts;
            }
            set
            {
                var r = Id;
                var g = Object.Id;
                SetProperty(v => Object.Starts = v, value);
                if (Object.Starts.CompareTo(Ends) > 0)
                    Object.Ends = Starts;
            }
        }

        public DateTime Ends
        {
            get
            {
                return Object.Ends;
            }
            set
            {
                SetProperty(v => Object.Ends = v, value);
                if (Object.Ends.CompareTo(Starts) < 0)
                    Object.Starts = Ends;

            }
        }

        public int Days { get { return Object.Days; }}

     
        public String ShortEnds { get { return Ends.ToShortDateString(); } }
        public String ShortStarts { get { return Starts.ToShortDateString(); } }
        public DateTimeScale BestChoice { get { return BestShut(); } }




        private DateTimeScale BestShut()
        {
            var shut = DateTimeScale.Yearly;

            if (Starts.CompareTo(Ends) <= 0)
            {
                int days = Days;

                if (days <= 100)
                    shut = DateTimeScale.Daily;

                if (days > 200)
                    shut = DateTimeScale.Weekly;

                if (days > 400)
                    shut = DateTimeScale.Monthly;

                if (days > 800)
                    shut = DateTimeScale.Yearly;

            }
            return shut;
        }
        /// <summary>
        /// To obtains a collection of period, defined by the kind of time scale from the current (<see cref="InvestmentElementPeriodPresenter"/>
        /// </summary>
        public IList<IPeriod> Periods { get { return Object.Periods; } }


      

      
    }
}
