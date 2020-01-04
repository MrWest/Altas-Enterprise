using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    /// <summary>
    /// Specifies the a time scale for a period.
    /// </summary>
    public enum PeriodKind { Anual, Mensual, Semanal, Diario };
    /// <summary>
    /// Represents a time interval, and brings som facilities.
    /// </summary>
    public interface IPeriod: INomenclator
    {
        IEntity Holder { get; set; }
        DateTimeScale PeriodKind { get; set; }

        [Column(TypeName = "datetime2")]
        DateTime Starts { get; set; }
        [Column(TypeName = "datetime2")]
        DateTime Ends { get; set; }
       
        int Days { get; }

        /// <summary>
        /// To obtains a collection of period, defined by the kind of time scale from the current (<see cref="InvestmentElementPeriodPresenter"/>
        /// </summary>
        IList<IPeriod> Periods { get ; }

        bool IsContained(IPeriod period);

        DateTime OriStart();
        DateTime OriEnd();

        #region
        // add-ons
        String ShortEnds  { get; }
        String ShortStarts { get; }

        #endregion

        string HolderId { get; set; }


    }
}
