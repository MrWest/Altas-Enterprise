using System.Collections.Generic;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface ICustomReportSettings: INomenclator
    {
        //ICustomReportSettings Parent { get; set; }
        IList<ICustomReportSettings> CustomReportSettingses { get; }
    }

    public interface IMainCustomReportSettings : ICustomReportSettings
    {
       
    }

    public interface IChildCustomReportSettings : ICustomReportSettings
    {
        ICustomReportSettings Parent { get; set; }
        string ParentId { get; set; }

    }
}