using System.Collections.ObjectModel;

namespace CompanyName.Atlas.Contracts.Presentation.Reporting
{
    public interface ISeriesData
    {
         string SeriesDisplayName { get; set; }

        string SeriesDescription { get; set; }

       ObservableCollection<IDataClass> Items { get; set; }
    }
}
