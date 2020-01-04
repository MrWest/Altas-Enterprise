using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract describing the interface of a period 
    /// </summary>
    public interface IInvestmentElementPeriodPresenter : IPresenter<IInvestmentElementPeriod>
    {
        DateTimeScale PeriodKind { get; set; }

        DateTime Starts { get; set; }


        DateTime Ends { get; set; }
       
        IList<IPeriod> Periods { get; }

        int Days { get; }



        /// <summary>
        /// Gets or sets the presenter view model containing the investment element to which belong the period decorated by the current
        /// period presenter.
        /// </summary>
        IInvestmentElementPresenter InvestmentElement { get; set; }
        
       

    }
}
