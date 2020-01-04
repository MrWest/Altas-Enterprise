using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IMeasurableUnitPresenter<TMeasurable>:IPresenter<TMeasurable>
        where TMeasurable : class ,IMeasurableUnit
    {
        IPresenter Holder { get; set; }

        /// <summary>
        /// Gets or sets the measurement unit id
        /// </summary>
        IMeasurementUnitPresenter MeasurementUnit { get; set; }
        decimal Amount { get; set; }
        
    }
}
