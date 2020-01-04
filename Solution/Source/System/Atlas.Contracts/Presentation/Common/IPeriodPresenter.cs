using System;
using System.Notifications;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IPeriodPresenter:IPresenter<IPeriod>
    {
        IPresenter Holder { get; set; }
        DateTime Starts { get; set; }
        DateTime Ends { get; set; }
        int Days{get ;}

        DateTime OriStart();
        DateTime OriEnd();
        bool IsContained(IPeriod period);
        string ShortStarts { get; }
        string ShortEnds { get;  }
    }
}
