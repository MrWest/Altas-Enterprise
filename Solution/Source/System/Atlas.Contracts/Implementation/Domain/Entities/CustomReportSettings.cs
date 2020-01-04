using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public abstract class CustomReportSettings : NomenclatorBase, ICustomReportSettings
    {

        private IList<ICustomReportSettings> _customReportSettingses;

       public CustomReportSettings()
        {
            _customReportSettingses = new List<ICustomReportSettings>();
        }
        public IList<ICustomReportSettings> CustomReportSettingses
        {
            get { return _customReportSettingses; }
        }

       
    }

    public  class ChildCustomReportSettings : CustomReportSettings, IChildCustomReportSettings
    {

       
        public ICustomReportSettings Parent { get; set; }
        public string ParentId { get; set; }
    }

    public  class MainCustomReportSettings : CustomReportSettings, IMainCustomReportSettings
    {

       
    }

}