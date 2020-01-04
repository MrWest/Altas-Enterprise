using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Reporting;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Reporting
{
    public class SeriesData:ISeriesData
    {
        public string SeriesDisplayName { get; set; }

        public string SeriesDescription { get; set; }

        public SeriesData()
        {
            Items = new ObservableCollection<IDataClass>();
        }

        public ObservableCollection<IDataClass> Items { get; set; }
    }
}
